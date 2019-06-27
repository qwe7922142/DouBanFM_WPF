using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Common.Helpers
{
    public sealed class ConvertHelper
    {
        /// <summary>
        /// 强制转换为整形,此方法不会扔转换异常
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int ToInt32(object input)
        {
            if (input == null) return default(Int32);
            int result;
            if (!int.TryParse(input.ToString(), out result))
            {
                result = default(Int32);
            }
            return result;
        }
    }
}
