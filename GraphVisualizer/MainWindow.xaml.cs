using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Win32;

namespace GraphVisualizer
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        ValueSteps steps = new ValueSteps();
        public string CurrentFunctionText { get; set; }
        GraphDrawer gd = null;
        ObservableCollection<Parameter> parameters = new ObservableCollection<Parameter>();

        public static readonly RoutedCommand MoveGraphCommand = new RoutedCommand();
        public static readonly RoutedCommand AddConstantCommand = new RoutedCommand();
        public static readonly RoutedCommand RemoveConstantCommand = new RoutedCommand();
        public static readonly RoutedCommand StepConstantCommand = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();

            CurrentFunctionText = "x^2";
            displayFunctionText.Text = CurrentFunctionText.Replace("PI", "π");
            steps.GraphMoveStep = 10.0;
            steps.ValueStep = 1.0;
        }

        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            gd = new GraphDrawer(new Size(graphBorder.ActualWidth, graphBorder.ActualHeight));
            gd.Calculator = new Algebra.Calculator(CurrentFunctionText);
            parameters = new ObservableCollection<Parameter>();

            parameterList.ItemsSource = parameters;
            graphDrawConfigPanel.DataContext = gd;
            constantStepPanel.DataContext = steps;
            graphMoveStepPanel.DataContext = steps;

            UpdateCalculationAndCanvas();
        }

        private void funcChangeButton_Click(object sender, RoutedEventArgs e)
        {
            FunctionInputDialog fid = new FunctionInputDialog();
            fid.FunctionText = CurrentFunctionText.Replace("π", "PI");
            if (fid.ShowDialog() == true)
            {
                CurrentFunctionText = fid.FunctionText;
                UpdateFunctionText();
            }
        }

        #region Command Implements
        void MoveGraphCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string direction = (string)e.Parameter;
            switch (direction)
            {
                case "Left":
                    gd.OffsetX -= steps.GraphMoveStep;
                    break;
                case "Right":
                    gd.OffsetX += steps.GraphMoveStep;
                    break;
                case "Up":
                    gd.OffsetY -= steps.GraphMoveStep;
                    break;
                case "Down":
                    gd.OffsetY += steps.GraphMoveStep;
                    break;
            }
            // 描画範囲が変化しているので再計算が必要
            UpdateCalculationAndCanvas();
        }

        #region AddConstantCommand
        void AddConstantCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            string s = e.Parameter as string;

            e.CanExecute = true;

            if (string.IsNullOrWhiteSpace(s))
            {
                e.CanExecute = false;
                return;
            }

            if (s.Length > 1)
            {
                e.CanExecute = false;
                return;
            }

            if (!System.Text.RegularExpressions.Regex.IsMatch(s, @"[a-zA-Z]"))
            {
                e.CanExecute = false;
                return;
            }

            char c = char.Parse(s);
            if (parameters.Any(p => p.Character == c))
            {
                e.CanExecute = false;
                return;
            }

            if (c == 'x')
            {
                e.CanExecute = false;
                return;
            }
        }

        void AddConstantCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            parameters.Add(new Parameter(char.Parse((string)e.Parameter)));
        }
        #endregion

        #region RemoveConstantCommand
        void RemoveConstantCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;

            ListView listView = e.Parameter as ListView;
            
            if (listView == null)
            {
                e.CanExecute = false;
                return;
            }

            if (listView.SelectedIndex == -1)
                e.CanExecute = false;
        }

        void RemoveConstantCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            ListView listView = e.Parameter as ListView;
            if (listView == null)
                return;
            if (listView.SelectedIndex == -1)
                return;

            Parameter param = listView.SelectedItem as Parameter;
            parameters.Remove(param);
            UpdateFunctionText();
        }
        #endregion

        #region StepConstantCommand
        void StepConstantCommandCanExecuted(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
            if (parameterList == null || parameterList.SelectedIndex == -1)
                e.CanExecute = false;
        }

        void StepConstantCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string direction = e.Parameter as string;
            if (string.IsNullOrWhiteSpace(direction))
                return;
            
            // 値を変化させる方向の決定
            double step;
            if (direction == "Down")
                step = -steps.ValueStep;
            else if (direction == "Up")
                step = steps.ValueStep;
            else
                return;

            // 対象パラメータ取得
            Parameter param = parameterList.SelectedItem as Parameter;
            if (param == null)
                return;
            param.Value += step;
            // リンクが有効なら、その他のリンク定数も増減
            if (param.IsLinked)
                foreach (Parameter p in parameters.Where(x => x.IsLinked && x != param))
                    p.Value += step;
        }
        #endregion
        #endregion

        void UpdateFunctionText()
        {
            displayFunctionText.Text = CurrentFunctionText.Replace("PI", "π");
            gd.Calculator.SetExpression(CurrentFunctionText);
            if (!gd.Calculator.CheckParameter(parameters.Select(x => x.Character).ToList()))
            {
                foreach (char c in gd.Calculator.GetCharacters())
                    if (!parameters.Any(x => x.Character == c))
                        parameters.Add(new Parameter(c));
            }
            UpdateCalculationAndCanvas();
        }

        /// <summary>
        /// グラフを再描画します。
        /// </summary>
        void UpdateCanvas()
        {
            graphDG.ClipGeometry = new RectangleGeometry(new Rect(gd.CanvasSize));
            if (gd != null)
                using (DrawingContext dc = graphDG.Open())
                    gd.Draw(dc, false);
        }

        /// <summary>
        /// 計算結果を更新し、その後グラフを再描画します。
        /// </summary>
        void UpdateCalculationAndCanvas()
        {
            gd.Calculator.Parameter = CreateParameterDictionary(parameters);
            if (gd != null)
                using (DrawingContext dc = graphDG.Open())
                    gd.Draw(dc, true);
        }

        private void drawConfigurationBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCanvas();
        }

        private void drawConfigurationBoxWithUpdate_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCalculationAndCanvas();
        }

        private void valueBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCalculationAndCanvas();
        }

        /// <summary>
        /// <see cref="Parameter"/>クラスのコレクションから文字と値の辞書を作成します。
        /// </summary>
        /// <param name="parameters"><see cref="Parameter"/>クラスのコレクション</param>
        /// <returns>文字と値の辞書</returns>
        static Dictionary<char, double> CreateParameterDictionary(ICollection<Parameter> parameters)
        {
            Dictionary<char, double> result = new Dictionary<char, double>(parameters.Count);
            if (parameters != null)
                foreach (Parameter p in parameters)
                    result.Add(p.Character, p.Value);
            return result;
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            UpdateCanvas();
        }

        private void graphColorSettingButton_Click(object sender, RoutedEventArgs e)
        {
            GraphColorSettingDialog gcsd = new GraphColorSettingDialog();
            DrawingPenSettings dps = new DrawingPenSettings();
            dps.AxisLineBrush = gd.AxisLineBrush;
            dps.AxisLineThickness = gd.AxisLineThickness;
            dps.GraphBrush = gd.GraphBrush;
            dps.GraphThickness = gd.GraphThickness;
            dps.GridLineBrush = gd.GridLineBrush;
            dps.GridLineThickness = gd.GridLineThickness;
            dps.SubGridLineBrush = gd.SubGridLineBrush;
            dps.SubGridLineThickness = gd.SubGridLineThickness;
            gcsd.PenSettings = dps;
            if (gcsd.ShowDialog() == true)
            {
                gd.AxisLineBrush = gcsd.PenSettings.AxisLineBrush;
                gd.AxisLineThickness = gcsd.PenSettings.AxisLineThickness;
                gd.GraphBrush = gcsd.PenSettings.GraphBrush;
                gd.GraphThickness = gcsd.PenSettings.GraphThickness;
                gd.GridLineBrush = gcsd.PenSettings.GridLineBrush;
                gd.GridLineThickness = gcsd.PenSettings.GridLineThickness;
                gd.SubGridLineBrush = gcsd.PenSettings.SubGridLineBrush;
                gd.SubGridLineThickness = gcsd.PenSettings.SubGridLineThickness;
                UpdateCanvas();
            }
        }

        private void saveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            // http://blogs.yahoo.co.jp/elku_simple/21110650.html
            DrawingVisual dv = new DrawingVisual();
            using (DrawingContext dc = dv.RenderOpen())
                if (gd != null)
                    gd.Draw(dc, false);
            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)gd.CanvasSize.Width, (int)gd.CanvasSize.Height, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(dv);

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PNGファイル|*.png";
            if (sfd.ShowDialog() == true)
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                using (var stream = sfd.OpenFile())
                    encoder.Save(stream);
            }
        }
    }
}
