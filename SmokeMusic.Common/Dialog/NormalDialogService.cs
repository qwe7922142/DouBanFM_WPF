using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace SmokeMusic.Common.Dialog
{
    public class NormalDialogService : DialogBase
    {
        public override void Alert(string message, string title, Action callback)
        {
            if (string.IsNullOrWhiteSpace(title)) title = "提示";
            MessageBox.Show(Application.Current.MainWindow, message, title);
            if (callback != null)
            {
                callback.Invoke();
            }
        }

        public override void Confirm(string message, string title, Action<bool> callback)
        {
            if (string.IsNullOrWhiteSpace(title)) title = "确认操作";
            var result = MessageBox.Show(Application.Current.MainWindow, message, title, MessageBoxButton.YesNo);
            if (callback != null)
            {
                callback.Invoke(result == MessageBoxResult.Yes);
            }
        }

        public void SaveFile(FileConfig config, Action<Stream> callback)
        {
            
        }
    }
}
