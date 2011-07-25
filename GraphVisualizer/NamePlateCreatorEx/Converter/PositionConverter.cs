using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace NamePlateCreatorEx
{
    [ValueConversion(typeof(Point), typeof(string))]
    class PositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            return ((Point)value).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            try
            {
                return Point.Parse((string)value);
            }
            catch (FormatException)
            {
                return new Point(0, 0);
            }
        }
    }
}
