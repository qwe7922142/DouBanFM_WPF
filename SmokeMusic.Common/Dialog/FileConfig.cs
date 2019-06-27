using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Common.Dialog
{
    /// <summary>
    /// 文件对话框信息类
    /// </summary>
    public class FileConfig
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件扩展名
        /// 示例: txt files(*.txt)|*.txt|All files(*.*)|*.*
        /// </summary>
        public string Filter { get; set; }
        /// <summary>
        /// 显示的标题
        /// </summary>
        public string Title { get; set; }
    }
}
