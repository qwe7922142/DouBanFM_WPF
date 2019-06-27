using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SmokeMusic.Client.Behaviors
{
    public class MouseWheelBehavior : Behavior<Window>
    {
        public ICommand MouseWheelCommand
        {
            get { return (ICommand)GetValue(MouseWheelCommandProperty); }
            set { SetValue(MouseWheelCommandProperty, value); }
        }

        public static readonly DependencyProperty MouseWheelCommandProperty =
            DependencyProperty.Register("MouseWheelCommand", typeof(ICommand), typeof(MouseWheelBehavior), new PropertyMetadata(null));
        
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseWheel += AssociatedObject_MouseWheel;
        }

        void AssociatedObject_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (this.MouseWheelCommand != null)
            {
                this.MouseWheelCommand.Execute(e.Delta);
            }
        }
    }
}
