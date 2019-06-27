using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace SmokeMusic.Common.Behaviors
{
    /// <summary>
    /// 控制弹出子窗口的行为
    /// </summary>
    public class ChildWindowBehavior : Behavior<Window>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.InitWindowStyle();
        }
        public void InitWindowStyle()
        {
            this.AssociatedObject.WindowStyle = WindowStyle.None;
            this.AssociatedObject.AllowsTransparency = true;
            this.AssociatedObject.Owner = Application.Current.MainWindow;
            this.AssociatedObject.ShowInTaskbar = false;
            this.AssociatedObject.Closing += AssociatedObject_Closing;

            Binding binding = new Binding("Background");
            binding.Source = this.AssociatedObject.Owner;
            this.AssociatedObject.SetBinding(Window.BackgroundProperty, binding);

            binding = new Binding("Opacity");
            binding.Source = this.AssociatedObject.Owner;
            this.AssociatedObject.SetBinding(Window.OpacityProperty, binding);
        }

        void AssociatedObject_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.AssociatedObject.Owner.Activate();
        }
    }
}
