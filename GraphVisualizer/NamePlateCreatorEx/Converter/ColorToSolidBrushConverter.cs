using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace NamePlateCreatorEx
{
    [ValueConversion(typeof(Color), typeof(SolidColorBrush))]
    class ColorToSolidBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is Color))
                return Brushes.White;
            return new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            SolidColorBrush brush = value as SolidColorBrush;
            if (brush == null)
                return Colors.White;
            return brush.Color;
        }
    }
}
