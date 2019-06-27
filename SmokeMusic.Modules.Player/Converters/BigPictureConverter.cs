using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace SmokeMusic.Modules.Player.Converters
{
    /// <summary>
    /// 将中等大小的图片路径更改为大图片的途径
    /// </summary>
    public class BigPictureConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return null;
            return value.ToString().Replace("mpic", "lpic");
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
