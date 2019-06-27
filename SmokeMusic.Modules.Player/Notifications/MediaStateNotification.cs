using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Player.Notifications
{
    public class MediaStateNotification : Notification
    {
        public Enums.MediaState MediaState { get; set; }
    }
}
