using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using Hardcodet.Wpf.TaskbarNotification;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace SmokeMusic.Modules.Notify.Triggers
{
    /// <summary>
    /// 通知任务栏图标显示错误信息
    /// </summary>
    public class ShowErrorTrigger :TriggerAction<TaskbarIcon>
    {
        protected override void Invoke(object parameter)
        {
            var e = parameter as InteractionRequestedEventArgs;
            if (e == null) return;
            var errorInfo = e.Context as Notifications.ErrorInfo;
            if (errorInfo == null) return;
            this.AssociatedObject.ShowBalloonTip("错误", errorInfo.Content.ToString(), BalloonIcon.Error);
            if (e.Callback != null)
            {
                e.Callback.Invoke();
            }
        }
    }
}
