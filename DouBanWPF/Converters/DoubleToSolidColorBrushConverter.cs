using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace DouBanWPF.Converters
{
    public class DoubleBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = value.ToString().Substring(2);
            SolidColorBrush dd = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#"+ str));
            return dd;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
