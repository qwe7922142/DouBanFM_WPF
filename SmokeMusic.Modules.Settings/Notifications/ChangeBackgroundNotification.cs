using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Settings.Notifications
{
    public class ChangeBackgroundNotification : Notification
    {
        public string Background { get; set; }
    }
}
