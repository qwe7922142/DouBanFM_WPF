using System;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using SmokeMusic.Common;
using SmokeMusic.Common.Events.Player;

namespace SmokeMusic.Modules.Settings.ViewModels
{
    public class UISettingsViewModel : ChildWindowViewModelBase
    {
        public UISettingsViewModel()
            : base(WindowNames.UISettings)
        {
            this.EventAggregator.GetEvent<ChangeBackgroundEvent>().Subscribe(this.OnChangeBackground, true);
            this.ChangeBackgroundRequest = new InteractionRequest<Notifications.ChangeBackgroundNotification>();
            this.LoadSettings();            
        }

        #region 属性
        /// <summary>
        /// 显示的标题
        /// </summary>
        public override string Title
        {
            get
            {
                return "界面设置";
            }
        }
        private bool _showBalloonWhenChangingSongs;
        /// <summary>
        /// 是否总是在换歌时显示气泡
        /// </summary>
        public bool ShowBalloonWhenChangingSongs
        {
            get { return _showBalloonWhenChangingSongs; }
            set
            {
                if (_showBalloonWhenChangingSongs != value)
                {
                    _showBalloonWhenChangingSongs = value;
                    this.RaisePropertyChanged("ShowBalloonWhenChangingSongs");
                }
            }
        }
        private bool _alwaysInFront;
        /// <summary>
        /// 是否总在最前
        /// </summary>
        public bool AlwaysInFront
        {
            get { return _alwaysInFront; }
            set
            {
                if (_alwaysInFront != value)
                {
                    _alwaysInFront = value;
                    this.RaisePropertyChanged("AlwaysInFront");
                }
            }
        }
        private bool _alwaysShowNotifyIcon;
        /// <summary>
        /// 是否显示托盘图标
        /// </summary>
        public bool AlwaysShowNotifyIcon
        {
            get { return _alwaysShowNotifyIcon; }
            set
            {
                if (_alwaysShowNotifyIcon != value)
                {
                    _alwaysShowNotifyIcon = value;
                    this.RaisePropertyChanged("AlwaysShowNotifyIcon");
                }
            }
        }
        private double _windowOpacity;
        /// <summary>
        /// 窗口透明度
        /// </summary>
        public double WindowOpacity
        {
            get { return _windowOpacity; }
            set
            {
                if (value < 0) value = 0;
                if (value > 1) value = 1;
                if (_windowOpacity != value)
                {                    
                    _windowOpacity = value;
                    this.RaisePropertyChanged("WindowOpacity");
                }
            }
        }
        private bool _allowSetWindowBackground;
        /// <summary>
        /// 是否手动指定窗口颜色
        /// </summary>
        public bool AllowSetWindowBackground
        {
            get { return _allowSetWindowBackground; }
            set
            {
                if (_allowSetWindowBackground != value)
                {
                    _allowSetWindowBackground = value;
                    this.RaisePropertyChanged("AllowSetWindowBackground");
                }
            }
        }
        private string _windowBackground;
        /// <summary>
        /// 窗口颜色
        /// </summary>
        public string WindowBackground
        {
            get { return _windowBackground; }
            set
            {
                if (_windowBackground != value)
                {
                    _windowBackground = value;
                    this.RaisePropertyChanged("WindowBackground");
                    this.ChangeBackground();
                }
            }
        }
        private bool _isChangingBackground;
        /// <summary>
        /// 是否正在更改背景
        /// </summary>
        public bool IsChangingBackground
        {
            get { return _isChangingBackground; }
            set
            {
                if (_isChangingBackground != value)
                {
                    _isChangingBackground = value;
                    this.RaisePropertyChanged("IsChangingBackground");
                }
            }
        }
        /// <summary>
        /// 背景颜色更改的通知
        /// </summary>
        public InteractionRequest<Notifications.ChangeBackgroundNotification> ChangeBackgroundRequest { get; private set; }
        #endregion

        #region 方法
        /// <summary>
        /// 关闭窗口
        /// </summary>
        public override void Close()
        {
            base.Close();
            
            //保存配置到本地文件

        }
        public void LoadSettings()
        {
            this.WindowOpacity = 0.8;
            this.AlwaysInFront = true;
            this.WindowBackground = "#FF1960AF";
        }
        public void OnChangeBackground(BitmapSource source)
        {
            if (this.IsChangingBackground) return;
            this.IsChangingBackground = true;
            SmokeMusic.Common.Helpers.ColorHelper.GetImageColorForBackgroundAsync(source, c =>
            {
                this.InvokeOnUIDispatcher(new Action(() => 
                {
                    this.WindowBackground = c.ToString();
                    this.IsChangingBackground = false;
                }));                
            });
        }
        public void ChangeBackground()
        {
            //if (this.AllowSetWindowBackground) return;
            var noti = new Notifications.ChangeBackgroundNotification() { Background = this.WindowBackground.ToString() };
            this.ChangeBackgroundRequest.Raise(noti);
        }
        #endregion
    }
}
