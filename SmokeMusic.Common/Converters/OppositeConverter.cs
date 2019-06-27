using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SmokeMusic.Common.Converters
{
    public class OppositeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return true;
            bool result = false;
            Boolean.TryParse(value.ToString(), out result);
            return !result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return false;
            bool result = true;
            Boolean.TryParse(value.ToString(), out result);
            return !result;
        }
    }
}
