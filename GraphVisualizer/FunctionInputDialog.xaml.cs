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
using Algebra;

namespace GraphVisualizer
{
    /// <summary>
    /// FunctionInputDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class FunctionInputDialog : Window
    {
        /// <summary>
        /// 関数の文字列を取得・設定します。
        /// </summary>
        public string FunctionText { get; set; }

        public static readonly RoutedCommand CalculatorButtonCommand = new RoutedCommand();
        public static readonly RoutedCommand OKCommand = new RoutedCommand();

        public FunctionInputDialog()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            functionPreviewText.Text = FunctionText;
            functionPreviewText.Focus();
            functionPreviewText.CaretIndex = FunctionText.Length;
        }

        #region Command Implementations
        #region CalculatorButtonCommand
        void CalculatorButtonCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void CalculatorButtonCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            string s = (string)e.Parameter;
            int caret = functionPreviewText.CaretIndex;
            #region switch-syntax branches
            switch (s)
            {
                case "0":
                    FunctionText = FunctionText.Insert(caret, "0");
                    caret++;
                    break;
                case "1":
                    FunctionText = FunctionText.Insert(caret, "1");
                    caret++;
                    break;
                case "2":
                    FunctionText = FunctionText.Insert(caret, "2");
                    caret++;
                    break;
                case "3":
                    FunctionText = FunctionText.Insert(caret, "3");
                    caret++;
                    break;
                case "4":
                    FunctionText = FunctionText.Insert(caret, "4");
                    caret++;
                    break;
                case "5":
                    FunctionText = FunctionText.Insert(caret, "5");
                    caret++;
                    break;
                case "6":
                    FunctionText = FunctionText.Insert(caret, "6");
                    caret++;
                    break;
                case "7":
                    FunctionText = FunctionText.Insert(caret, "7");
                    caret++;
                    break;
                case "8":
                    FunctionText = FunctionText.Insert(caret, "8");
                    caret++;
                    break;
                case "9":
                    FunctionText = FunctionText.Insert(caret, "9");
                    caret++;
                    break;
                case ".":
                    FunctionText = FunctionText.Insert(caret, ".");
                    caret++;
                    break;
                case "sin":
                    FunctionText = FunctionText.Insert(caret, "sin(");
                    caret += 4;
                    break;
                case "cos":
                    FunctionText = FunctionText.Insert(caret, "cos(");
                    caret += 4;
                    break;
                case "tan":
                    FunctionText = FunctionText.Insert(caret, "tan(");
                    caret += 4;
                    break;
                case "ln":
                    FunctionText = FunctionText.Insert(caret, "log(");
                    caret += 4;
                    break;
                case "(":
                    FunctionText = FunctionText.Insert(caret, "(");
                    caret++;
                    break;
                case ")":
                    FunctionText = FunctionText.Insert(caret, ")");
                    caret++;
                    break;
                case "+":
                    FunctionText = FunctionText.Insert(caret, "+");
                    caret++;
                    break;
                case "-":
                    FunctionText = FunctionText.Insert(caret, "-");
                    caret++;
                    break;
                case "*":
                    FunctionText = FunctionText.Insert(caret, "*");
                    caret++;
                    break;
                case "/":
                    FunctionText = FunctionText.Insert(caret, "/");
                    caret++;
                    break;
                case "^":
                    FunctionText = FunctionText.Insert(caret, "^");
                    caret++;
                    break;
                case "e":
                    FunctionText = FunctionText.Insert(caret, "e");
                    caret++;
                    break;
                case "pi":
                    FunctionText = FunctionText.Insert(caret, "PI");
                    caret += 2;
                    break;
                case "bs":
                    if (FunctionText.Length > 0 && caret > 0)
                    {
                        FunctionText = FunctionText.Remove(caret - 1, 1);
                        caret--;
                    }
                    break;
            }
            #endregion
            UpdateFunctionText();
            functionPreviewText.Focus();
            functionPreviewText.CaretIndex = caret;
        }
        #endregion

        #region OKCommand
        void OKCommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        void OKCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                // 解析に失敗するということは認識不能。
                new Calculator(FunctionText);
            }
            catch(Exception ex)
            {
                MessageBox.Show(
                    "数式にエラーがあります。"
                    + Environment.NewLine
                    + ex.Message,
                    "数式エラー",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            DialogResult = true;
        }
        #endregion
        #endregion

        void UpdateFunctionText()
        {
            functionPreviewText.Text = FunctionText;
        }

        private void functionPreviewText_TextChanged(object sender, TextChangedEventArgs e)
        {
            FunctionText = functionPreviewText.Text;
        }

        private void functionPreviewText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OKCommand.Execute(null, null);
            }
        }
    }
}
