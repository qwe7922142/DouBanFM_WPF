using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace SmokeMusic.Modules.Settings.Triggers
{
    public class ChangeBackgroundTrigger : TriggerAction<Window>
    {
        Storyboard _backgroundStoryboard;
        Storyboard BackgroundStoryboard 
        {
            get
            {
                if (_backgroundStoryboard == null)
                {
                    _backgroundStoryboard = (Storyboard)this.AssociatedObject.FindResource("BackgroundColorStoryboard");
                }
                return _backgroundStoryboard;
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
        }
        
        protected override void Invoke(object parameter)
        {
            var e = parameter as Microsoft.Practices.Prism.Interactivity.InteractionRequest.InteractionRequestedEventArgs;
            var noti = e.Context as Settings.Notifications.ChangeBackgroundNotification;
            var animation = (ColorAnimation)this.BackgroundStoryboard.Children[0];
            animation.To = (Color)ColorConverter.ConvertFromString(noti.Background);
            this.BackgroundStoryboard.Begin();
        }
    }
}
