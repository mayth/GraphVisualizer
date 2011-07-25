using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace NamePlateCreatorEx
{
    [ValueConversion(typeof(double), typeof(string))]
    class SliderToColorValueConverter : IValueConverter
    {
        const double Factor = 255.0 / 10.0;

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            double d = (double)value;
            return d * Factor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string s = (string)value;
            double d;
            if (!double.TryParse(s, out d))
                return 0;
            return d / Factor;
        }
    }
}
