using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace SmokeMusic.Modules.Player.Converters
{
    public class DurationToTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ts = (TimeSpan)value;
            return string.Format("{0}:{1}", ts.Minutes.ToString("00"), ts.Seconds.ToString("00"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
