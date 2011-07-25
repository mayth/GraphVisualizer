using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace GraphVisualizer
{
    class PositiveDoubleValueValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null)
                return new ValidationResult(false, "値がnullです。");

            double d;
            if (!double.TryParse(value as string, out d))
                return new ValidationResult(false, "実数型に変換できませんでした。範囲を超えているか、数字以外の文字が入力されています。");

            if (d < 0)
                return new ValidationResult(false, "負の数が入力されています。");

            return new ValidationResult(true, null);
        }
    }
}
