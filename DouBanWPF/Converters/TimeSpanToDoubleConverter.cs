using System;
using System.Globalization;
using System.Windows.Data;

namespace DouBanWPF.Converters
{

    [ValueConversion(typeof(TimeSpan?), typeof(double))]
    internal class TimeSpanToDoubleConverter : IValueConverter
    {
        /// <summary>Converts a value. </summary>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            TimeSpan? timeSpan = value as TimeSpan?;
            if (timeSpan == null)
            {
                return 0;
            }
            return timeSpan.Value.TotalSeconds;
        }

        /// <summary>Converts a value. </summary>
        /// <returns>A converted value. If the method returns null, the valid null value is used.</returns>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double value2 = double.Parse(value.ToString());
            return TimeSpan.FromSeconds(value2);
        }
    }
}
