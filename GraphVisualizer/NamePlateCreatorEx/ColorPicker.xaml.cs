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
using System.Reflection;
using System.Globalization;

namespace NamePlateCreatorEx
{
    /// <summary>
    /// ColorPicker.xaml の相互作用ロジック
    /// </summary>
    public partial class ColorPicker : Window
    {
        static List<ColorNamePair> colorsList;
        public Color SelectedColor { get; set; }

        public ColorPicker()
        {
            InitializeComponent();

            SelectedColor = Colors.White;
        }

        static ColorPicker()
        {
            colorsList = new List<ColorNamePair>();
            Type t = typeof(Colors);
            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                ColorNamePair c = new ColorNamePair(property.Name, (Color)property.GetValue(null, null));
                colorsList.Add(c);
            }
            ColorNamePair transparent = colorsList.Find(c => c.Name == "Transparent");
            colorsList.Remove(transparent);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DefaultColorBox.ItemsSource = colorsList;
            DefaultColorBox.SelectedItem = SelectedColor;
            colorABox.Text = SelectedColor.A.ToString();
            ColorInHexTextBox.Text = ColorToHexString(SelectedColor);
            selectedColorRectangle.Fill = new SolidColorBrush(SelectedColor);
            selectedColorWithAlphaRectangle.Fill = new SolidColorBrush(Color.FromArgb(SelectedColor.A, SelectedColor.R, SelectedColor.G, SelectedColor.B));
        }

        #region デフォルト色ピッカー
        private void Rectangle_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            Color rectColor = ((SolidColorBrush)rect.Fill).Color;
            ColorNamePair cnPair = colorsList.Find(x => x.Color == rectColor);
            Color alphaColor = Color.FromArgb(byte.Parse(colorABox.Text), cnPair.Color.R, cnPair.Color.G, cnPair.Color.B);

            previewColorRectangle.Fill = new SolidColorBrush(cnPair.Color);
            previewColorWithAlphaRectangle.Fill = new SolidColorBrush(alphaColor);
            previewColorNameText.Text = (string)rect.Tag;
            previewColorRText.Text = alphaColor.R.ToString();
            previewColorGText.Text = alphaColor.G.ToString();
            previewColorBText.Text = alphaColor.B.ToString();
        }

        private void DefaultColorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DefaultColorBox.SelectedItem == null)
                return;

            ColorNamePair selectedPair = (ColorNamePair)DefaultColorBox.SelectedItem;
            Color alphaColor = Color.FromArgb(byte.Parse(colorABox.Text), selectedPair.Color.R, selectedPair.Color.G, selectedPair.Color.B);

            selectedColorRectangle.Fill = new SolidColorBrush(selectedPair.Color);
            selectedColorWithAlphaRectangle.Fill = new SolidColorBrush(alphaColor);
            selectedColorNameText.Text = selectedPair.Name;
            selectedColorRBox.Text = alphaColor.R.ToString();
            selectedColorGBox.Text = alphaColor.G.ToString();
            selectedColorBBox.Text = alphaColor.B.ToString();

            SelectedColor = alphaColor;
        }

        private void DefaultColorBox_MouseLeave(object sender, MouseEventArgs e)
        {
            previewColorRectangle.Fill = new SolidColorBrush(Colors.White);
            previewColorWithAlphaRectangle.Fill = new SolidColorBrush(Colors.White);
            previewColorNameText.Text = string.Empty;
            previewColorRText.Text = string.Empty;
            previewColorGText.Text = string.Empty;
            previewColorBText.Text = string.Empty;
        }
        #endregion

        #region 選択色RGBボックス
        private void selectedColorRBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSelectedColor(Color.FromArgb(byte.Parse(colorABox.Text), byte.Parse(selectedColorRBox.Text), SelectedColor.G, SelectedColor.B));
        }

        private void selectedColorGBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSelectedColor(Color.FromArgb(byte.Parse(colorABox.Text), SelectedColor.R, byte.Parse(selectedColorGBox.Text), SelectedColor.B));
        }

        private void selectedColorBBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateSelectedColor(Color.FromArgb(byte.Parse(colorABox.Text), SelectedColor.R, SelectedColor.G, byte.Parse(selectedColorBBox.Text)));
        }
        #endregion

        private void colorABox_TextChanged(object sender, TextChangedEventArgs e)
        {
            byte alpha = byte.Parse(colorABox.Text);
            UpdateSelectedColor(Color.FromArgb(alpha, SelectedColor.R, SelectedColor.G, SelectedColor.B));

            // Update Preview
            if (previewColorRectangle.Fill == null)
                return;
            Color currentPreviewColor = ((SolidColorBrush)previewColorRectangle.Fill).Color;
            previewColorWithAlphaRectangle.Fill = new SolidColorBrush(Color.FromArgb(alpha, currentPreviewColor.R, currentPreviewColor.G, currentPreviewColor.B));
        }

        private void ColorInHexTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateSelectedColorFromHex();
        }

        private void ColorInHexTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                UpdateSelectedColorFromHex();
        }

        private void UpdateSelectedColorFromHex()
        {
            Color color = HexStringToColor(ColorInHexTextBox.Text);
            UpdateSelectedColor(color);

            colorABox.Text = color.A.ToString();
            selectedColorRBox.Text = color.R.ToString();
            selectedColorGBox.Text = color.G.ToString();
            selectedColorBBox.Text = color.B.ToString();
        }

        void UpdateSelectedColor(Color color)
        {
            Color colorWithoutAlpha = Color.FromArgb(255, color.R, color.G, color.B);
            ColorNamePair selectedPair = colorsList.Find(c => c.Color == colorWithoutAlpha);
            string selectedColorName = selectedPair == null ? "Unknown" : selectedPair.Name;

            selectedColorRectangle.Fill = new SolidColorBrush(colorWithoutAlpha);
            selectedColorWithAlphaRectangle.Fill = new SolidColorBrush(color);
            selectedColorNameText.Text = selectedColorName;
            ColorInHexTextBox.Text = ColorToHexString(color);

            SelectedColor = color;
        }

        static string ColorToHexString(Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", color.A, color.R, color.G, color.B);
        }

        static Color HexStringToColor(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return Colors.White;
            string match = System.Text.RegularExpressions.Regex.Match(s, "[0-9A-Fa-f]{8}").Value;
            if (string.IsNullOrWhiteSpace(match))
                return Colors.White;
            match = "#" + match;

            byte a, r, g, b;
            if (!byte.TryParse(match.Substring(1, 2), NumberStyles.HexNumber, null, out a))
                a = 255;
            if (!byte.TryParse(match.Substring(3, 2), NumberStyles.HexNumber, null, out r))
                r = 255;
            if (!byte.TryParse(match.Substring(5, 2), NumberStyles.HexNumber, null, out g))
                g = 255;
            if (!byte.TryParse(match.Substring(7, 2), NumberStyles.HexNumber, null, out b))
                b = 255;

            return Color.FromArgb(a, r, g, b);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
