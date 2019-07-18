using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DouBanAPI
{
    /// <summary>
    /// 代表URL地址中的参数
    /// </summary>
    public class Parameters : Dictionary<string, string>
    {
        /// <summary>
        /// 是否添加空参数
        /// </summary>
        public bool AddEmptyParameter { get; set; }

        /// <summary>
        /// 生成 <see cref="Parameters"/> class 的新实例。
        /// </summary>
        /// <param name="addEmptyParameter">是否添加空参数</param>
        public Parameters(bool addEmptyParameter = false)
            : base()
        {
            AddEmptyParameter = addEmptyParameter;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var p in this)
            {
                if (AddEmptyParameter || !string.IsNullOrEmpty(p.Value))
                {
                    if (sb.Length != 0) sb.Append("&");
                    sb.Append(Uri.EscapeDataString(p.Key));
                    sb.Append("=");
                    sb.Append(Uri.EscapeDataString(p.Value == null ? string.Empty : p.Value));
                }
            }
            return sb.ToString();
        }
    }
}
