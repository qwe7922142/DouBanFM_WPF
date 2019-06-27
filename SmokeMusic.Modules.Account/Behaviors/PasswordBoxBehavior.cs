using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SmokeMusic.Modules.Account.Behaviors
{
    public class PasswordBoxBehavior : Behavior<PasswordBox>
    {
        /// <summary>
        /// 密码改变时执行的命令
        /// </summary>
        public ICommand ChangePasswordCommand
        {
            get { return (ICommand)GetValue(ChangePasswordCommandProperty); }
            set { SetValue(ChangePasswordCommandProperty, value); }
        }

        public static readonly DependencyProperty ChangePasswordCommandProperty =
            DependencyProperty.Register("ChangePasswordCommand", typeof(ICommand), typeof(PasswordBoxBehavior), new PropertyMetadata(null));

        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.ChangePasswordCommand != null)
            {
                this.ChangePasswordCommand.Execute(this.AssociatedObject.Password);
            }
        }
    }
}
