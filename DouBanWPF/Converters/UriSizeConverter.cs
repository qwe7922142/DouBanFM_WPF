using System;
using System.Globalization;
using System.Windows.Data;

namespace DouBanWPF.Converters
{
   
    public class UriSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return 250;
            }
            string uri = value.ToString();
            if (uri.Contains("large"))
            {
                return 250;
            }
            if (uri.Contains("medium"))
            {
                return 250;
            }
            else
            {
                return 250;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
