using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace SmokeMusic.Modules.Player.Triggers
{
    public class PlayerOrPauseAction : TriggerAction<MediaElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
        }
        protected override void Invoke(object parameter)
        {
            var e = parameter as InteractionRequestedEventArgs;
            if (e == null) return;
            var notifition = e.Context as Notifications.MediaStateNotification;
            if (notifition == null) return;
            switch (notifition.MediaState)
            {
                case Enums.MediaState.Play:
                    this.AssociatedObject.Play();
                    break;
                case Enums.MediaState.Pause:
                    this.AssociatedObject.Pause();
                    break;
            }
        }
    }
}
