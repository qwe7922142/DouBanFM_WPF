using System;
using System.Globalization;
using System.Windows.Data;

namespace DouBanWPF.Converters
{

    [ValueConversion(typeof(TimeSpan?), typeof(string))]
    internal class TimeSpanToStringConverter : IValueConverter
    {
        private static TimeSpanToStringConverter converter = new TimeSpanToStringConverter();
        /// <summary>
        /// 由TimeSpan值转换为字符串，用于时间显示
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            TimeSpan timespan = (TimeSpan)value;
            return (timespan.Hours > 0 ? timespan.Hours.ToString() + ":" : "") + (timespan.Minutes < 10 ? "0" : "") + timespan.Minutes + ":" + (timespan.Seconds < 10 ? "0" : "") + timespan.Seconds;
        }
        /// <summary>
        /// 由时间字符串转换回TimeSpan值
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string value2 = "00:" + (string)value;
            return TimeSpan.Parse((string)value2);
        }
        /// <summary>
        /// 从时间到字符串的静态转换方法
        /// </summary>
        /// <param name="value">时间</param>
        /// <returns>格式化后的字符串</returns>
        public static string QuickConvert(TimeSpan value)
        {
            return (string)converter.Convert(value, typeof(string), null, null);
        }
        /// <summary>
        /// 从字符串到时间的静态转换方法
        /// </summary>
        /// <param name="value">格式化后的字符串</param>
        /// <returns>时间</returns>
        public static TimeSpan QuickConvertBack(string value)
        {
            return (TimeSpan)converter.ConvertBack(value, typeof(TimeSpan), null, null);
        }
    }
}
