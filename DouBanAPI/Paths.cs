using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DouBanAPI
{
    /// <summary>
    /// 路径集合
    /// </summary>
    public sealed class Paths
    {
        static Paths()
        {
            if (!Directory.Exists(DataFolder))
            {
                Directory.CreateDirectory(DataFolder);
            }
        }
        /// <summary>
        /// 保存数据的文件夹
        /// </summary>
        public static readonly string DataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"DouBanMusic");
        /// <summary>
        /// Cookies文件路径
        /// </summary>
        public static readonly string CookiesFile = Path.Combine(DataFolder, "Cookies.dat");
        /// <summary>
        /// 用户信息文件路径
        /// </summary>
        public static readonly string UserInfoFile = Path.Combine(DataFolder, "UserInfo.dat");
    }
}
