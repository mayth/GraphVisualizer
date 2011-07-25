using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace NamePlateCreatorEx
{
    class EnumBooleanConverter : IValueConverter
    {
        // Enum to bool
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = parameter as string;
            if (string.IsNullOrWhiteSpace(param))
                return System.Windows.DependencyProperty.UnsetValue;
            if (!Enum.IsDefined(value.GetType(), param))
                return System.Windows.DependencyProperty.UnsetValue;
            object v = Enum.Parse(value.GetType(), param);
            return value.Equals(v);
        }

        // bool to Enum
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = parameter as string;
            if (string.IsNullOrWhiteSpace(param))
                return System.Windows.DependencyProperty.UnsetValue;
            return Enum.Parse(targetType, param);
        }
    }
}
