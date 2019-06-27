using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace SmokeMusic.Modules.Player.Behaviors
{
    /// <summary>
    /// 设置遮罩Grid的行为
    /// </summary>
    public class CoverGridBehavior : Behavior<Grid>
    {
        public CoverGridBehavior()
        {
            this.SlideCoverLeftTimer = new DispatcherTimer();
            this.SlideCoverLeftTimer.Interval = timerTicks;
            this.SlideCoverLeftTimer.Tick += SlideCoverLeftTimer_Tick;
            this.SlideCoverRightTimer = new DispatcherTimer();
            this.SlideCoverRightTimer.Interval = timerTicks;
            this.SlideCoverRightTimer.Tick += SlideCoverRightTimer_Tick;
        }

        private TimeSpan timerTicks = TimeSpan.FromMilliseconds(1000);

        #region 属性
        /// <summary>
        /// 是否在移动
        /// </summary>
        public bool IsMoving { get; set; }        
        private FrameworkElement _leftPanel;
        /// <summary>
        /// 左侧导航的容器
        /// </summary>
        private FrameworkElement LeftPanel
        {
            get
            {
                if (_leftPanel == null)
                {
                    _leftPanel = Application.Current.MainWindow.FindName("LeftPanel") as FrameworkElement;
                }
                return _leftPanel;
            }
        }
        private FrameworkElement _rightPanel;
        /// <summary>
        /// 右方播放器的容器
        /// </summary>
        private FrameworkElement RightPanel
        {
            get
            {
                if (_rightPanel == null)
                {
                    _rightPanel = Application.Current.MainWindow.FindName("RightPanel") as FrameworkElement;
                }
                return _rightPanel;
            }
        }
        /// <summary>
        /// Grid是否处在左边
        /// </summary>
        public bool IsInLeftSide
        {
            get
            {
                var transformGroup = (TransformGroup)this.AssociatedObject.GetValue(UIElement.RenderTransformProperty);
                var transform = transformGroup.Children[3] as TranslateTransform;
                return transform.X == 0;
            }
        }

        private Storyboard _slideCoverLeftStoryboard;
        /// <summary>
        /// 向左移动的动画
        /// </summary>
        public Storyboard SlideCoverLeftStoryboard
        {
            get
            {
                if (_slideCoverLeftStoryboard == null)
                {
                    _slideCoverLeftStoryboard = this.AssociatedObject.FindResource("SlideCoverLeftStoryboard") as Storyboard;
                    _slideCoverLeftStoryboard.Completed += Storyboard_Completed;
                } 
                return _slideCoverLeftStoryboard;
            }
        }
        private Storyboard _slideCoverRightStoryboard;
        /// <summary>
        /// 向右移动的动画
        /// </summary>
        public Storyboard SlideCoverRightStoryboard
        {
            get
            {
                if (_slideCoverRightStoryboard == null)
                {
                    _slideCoverRightStoryboard = this.AssociatedObject.FindResource("SlideCoverRightStoryboard") as Storyboard;
                    _slideCoverRightStoryboard.Completed += Storyboard_Completed;
                }
                return _slideCoverRightStoryboard;
            }
        }
        /// <summary>
        /// 控制向左移动的计时器
        /// </summary>
        DispatcherTimer SlideCoverLeftTimer { get; set; }
        /// <summary>
        /// 控制向右移动的计时器
        /// </summary>
        DispatcherTimer SlideCoverRightTimer { get; set; }
        #endregion

        #region 方法
        /// <summary>
        /// 附加完成时触发的方法
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.MouseMove += AssociatedObject_MouseMove;
            this.AssociatedObject.MouseLeave += AssociatedObject_MouseLeave;
            this.AssociatedObject.MouseLeftButtonDown += AssociatedObject_MouseLeftButtonDown;
        }        
        void SlideCoverRightTimer_Tick(object sender, EventArgs e)
        {
            SlideCoverRightTimer.Stop();
            if (IsMoving) return;
            this.IsMoving = true;
            this.SlideCoverRightStoryboard.Begin();            
        }
        void SlideCoverLeftTimer_Tick(object sender, EventArgs e)
        {
            SlideCoverLeftTimer.Stop();
            if (IsMoving) return;
            this.IsMoving = true;
            this.SlideCoverLeftStoryboard.Begin();            
        }        
        /// <summary>
        /// Grid的MouseLeftButtonDown事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.AssociatedObject.CaptureMouse();
            if (!IsMoving)
            {
                IsMoving = true;
                if (this.IsInLeftSide)
                {
                    SlideCoverRightStoryboard.Begin();
                }
                else
                {
                    SlideCoverLeftStoryboard.Begin();
                }
            }
            this.AssociatedObject.ReleaseMouseCapture();
        }
        /// <summary>
        /// Gird的MouseLeave事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.SlideCoverLeftTimer.Stop();
            this.SlideCoverRightTimer.Stop();
        }
        /// <summary>
        /// Grid的MouseMove事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void AssociatedObject_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (this.IsInLeftSide)
            {
                SlideCoverRightTimer.Start();
                SlideCoverLeftTimer.Stop();
            }
            else
            {
                SlideCoverLeftTimer.Start();
                SlideCoverRightTimer.Stop();
            }
        }
        /// <summary>
        /// 动画停止的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Storyboard_Completed(object sender, EventArgs e)
        {
            this.IsMoving = false;
        }
        #endregion
    }
}
