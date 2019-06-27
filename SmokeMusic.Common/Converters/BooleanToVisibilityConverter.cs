using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace SmokeMusic.Common.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility result = Visibility.Collapsed;
            if (value == null)
            {
                return result;
            }
            bool b ;
            if (!Boolean.TryParse(value.ToString(), out b))
            {
                b = false;
            }
            result = b ? Visibility.Visible : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
