using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Settings.Models
{
    /// <summary>
    /// 界面设置实体类
    /// </summary>
    public class UISettings : NotificationObject
    {
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
                }
            }
        }
    }
}
