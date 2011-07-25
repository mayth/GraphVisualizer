using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace NamePlateCreatorEx
{
    [ValueConversion(typeof(FontData), typeof(string))]
    class FontDataConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            FontData font = value as FontData;
            if (font == null)
                return string.Empty;

            return string.Format("{0}, {1:N1}pt, {2}", font.FontFamily.Source, font.FontSize, font.FontWeight.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
