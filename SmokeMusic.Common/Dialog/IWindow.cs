using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Common.Dialog
{
    /// <summary>
    /// IWindow接口
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// 标题
        /// </summary>
        string Title { get; set; }
        /// <summary>
        /// 打开窗口
        /// </summary>
        void Show();
        /// <summary>
        /// 关闭窗口
        /// </summary>
        void Close();
        /// <summary>
        /// 激活窗口
        /// </summary>
        bool Activate();
        /// <summary>
        /// 数据上下文
        /// </summary>
        object DataContext { get; set; }
    }
}
