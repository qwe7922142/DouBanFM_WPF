using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using Hardcodet.Wpf.TaskbarNotification;

namespace SmokeMusic.Modules.Notify.Triggers
{
    public class DisposeTrigger : TriggerAction<TaskbarIcon>
    {
        protected override void Invoke(object parameter)
        {
            this.AssociatedObject.Dispose();
        }
    }
}
