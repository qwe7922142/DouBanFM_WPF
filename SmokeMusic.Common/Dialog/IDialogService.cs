using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SmokeMusic.Common.Dialog
{
    /// <summary>
    /// 对话框服务接口
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// 弹出一个警告框
        /// </summary>
        /// <param name="message">显示的内容</param>
        /// <param name="title">显示的标题</param>
        /// <param name="callback">关闭对话框后的回调方法</param>
        void Alert(string message, string title, Action callback);
        /// <summary>
        /// 弹出一个选择确认或取消的提示框
        /// </summary>
        /// <param name="message">显示的内容</param>
        /// <param name="title">显示的标题</param>
        /// <param name="callback">关闭对话框后的回调方法</param>
        void Confirm(string message, string title, Action<Boolean> callback);
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="config">文件对话框的配置</param>
        /// <param name="callback">选择文件后才触发的回调方法</param>
        void SaveFile(FileConfig config, Action<Stream> callback);
    }
}
