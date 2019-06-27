using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace SmokeMusic.Common.Dialog
{
    /// <summary>
    /// 对话框通用基类
    /// </summary>
    public abstract class DialogBase : IDialogService
    {
        public abstract void Alert(string message, string title, Action callback);

        public abstract void Confirm(string message, string title, Action<bool> callback);


        public virtual void SaveFile(FileConfig config, Action<Stream> callback)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (config == null) config = this.CreateDefaultConfig();
            dialog.FileName = config.FileName;
            dialog.Filter = config.Filter;
            dialog.Title = config.Title;
            if (dialog.ShowDialog(Application.Current.MainWindow) == true)
            {
                config.FileName = dialog.FileName;
                if (callback != null)
                {
                    var stream = dialog.OpenFile();
                    callback.Invoke(stream);
                }
            }
        }
        FileConfig CreateDefaultConfig()
        {
            var config = new FileConfig();
            config.Title = "请选择保存的路径";
            return config;
        }
    }
}
