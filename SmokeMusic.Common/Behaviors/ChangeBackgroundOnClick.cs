using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

namespace SmokeMusic.Common.Behaviors
{
    public class ChangeBackgroundOnClick : Behavior<Control>
    {
        public Brush Normal
        {
            get { return (Brush)GetValue(NormalProperty); }
            set { SetValue(NormalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Normal.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalProperty =
            DependencyProperty.Register("Normal", typeof(Brush), typeof(ChangeBackgroundOnClick), new PropertyMetadata(null));



        public Brush OnClick
        {
            get { return (Brush)GetValue(OnClickProperty); }
            set { SetValue(OnClickProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OnClick.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OnClickProperty =
            DependencyProperty.Register("OnClick", typeof(Brush), typeof(ChangeBackgroundOnClick), new PropertyMetadata(null));

        
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObject_PreviewMouseLeftButtonDown;
            this.AssociatedObject.PreviewMouseLeftButtonUp += AssociatedObject_PreviewMouseLeftButtonUp;
        }

        void AssociatedObject_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AssociatedObject.Background = Normal;
        }

        void AssociatedObject_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AssociatedObject.Background = OnClick;
        }
    }
}
