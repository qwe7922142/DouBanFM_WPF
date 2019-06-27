using Microsoft.Practices.Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Threading;

namespace SmokeMusic.Modules.Player.Behaviors
{
    /// <summary>
    /// 监视MediaElement进度的行为
    /// </summary>
    public class WatchProgressBehavior : Behavior<MediaElement>
    {
        #region 构造器
        public WatchProgressBehavior()
        {
            this.MainTimer = new DispatcherTimer();
            MainTimer.Interval = TimeSpan.FromSeconds(1);
            MainTimer.Tick += MainTimer_Tick;
        }
        #endregion

        #region 属性
        public DispatcherTimer MainTimer { get; set; }
        /// <summary>
        /// 进度更改时执行的命令
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }
        #endregion

        #region 依赖属性
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(WatchProgressBehavior), new PropertyMetadata(null));
        #endregion

        #region 方法
        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.AssociatedObject == null) return;
            if (this.Command == null) return;
            this.MainTimer.Start();
        }
        void MainTimer_Tick(object sender, EventArgs e)
        {
            var currentTime = this.AssociatedObject.Position;
            this.Command.Execute(currentTime);
        }
        #endregion
    }
}
