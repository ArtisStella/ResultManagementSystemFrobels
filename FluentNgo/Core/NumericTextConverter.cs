using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace FluentNgo.Core
{
    public class NumericTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string stringValue)) return value;

            var numericValue = new string(stringValue.Where(char.IsDigit).ToArray());

            return int.TryParse(numericValue, out var result) ? result : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int intValue)) return value;
            return intValue.ToString();
        }
    }
}
