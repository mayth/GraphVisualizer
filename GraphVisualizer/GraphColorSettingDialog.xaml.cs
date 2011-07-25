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
using System.Windows.Shapes;

namespace GraphVisualizer
{
    /// <summary>
    /// GraphColorSettingDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class GraphColorSettingDialog : Window
    {
        public DrawingPenSettings PenSettings { get; set; }

        public static readonly RoutedCommand ColorChangeCommand = new RoutedCommand();

        public GraphColorSettingDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (PenSettings == null)
                PenSettings = new DrawingPenSettings();
            mainPanel.DataContext = PenSettings;
        }

        void ColorChangeCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string target = e.Parameter as string;
            if (string.IsNullOrEmpty(target))
                return;

            NamePlateCreatorEx.ColorPicker cp = new NamePlateCreatorEx.ColorPicker();
            switch (target)
            {
                case "Graph":
                    cp.SelectedColor = ((SolidColorBrush)PenSettings.GraphBrush).Color;
                    break;
                case "Axis":
                    cp.SelectedColor = ((SolidColorBrush)PenSettings.AxisLineBrush).Color;
                    break;
                case "GridLine":
                    cp.SelectedColor = ((SolidColorBrush)PenSettings.GridLineBrush).Color;
                    break;
                case "SubGridLine":
                    cp.SelectedColor = ((SolidColorBrush)PenSettings.SubGridLineBrush).Color;
                    break;
                default:
                    return;
            }

            if (cp.ShowDialog() == true)
            {
                switch (target)
                {
                    case "Graph":
                        PenSettings.GraphBrush = new SolidColorBrush(cp.SelectedColor);
                        break;
                    case "Axis":
                        PenSettings.AxisLineBrush = new SolidColorBrush(cp.SelectedColor);
                        break;
                    case "GridLine":
                        PenSettings.GridLineBrush = new SolidColorBrush(cp.SelectedColor);
                        break;
                    case "SubGridLine":
                        PenSettings.SubGridLineBrush = new SolidColorBrush(cp.SelectedColor);
                        break;
                }
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
