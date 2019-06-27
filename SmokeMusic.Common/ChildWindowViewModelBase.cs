using SmokeMusic.Common.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Commands;

namespace SmokeMusic.Common
{
    /// <summary>
    /// 子窗口ViewModel基类
    /// </summary>
    public class ChildWindowViewModelBase : ViewModelBase
    {
        public ChildWindowViewModelBase(string windowName)
            : base()
        {
            this.WindowName = windowName;
        }
        /// <summary>
        /// 窗口注册的名称
        /// </summary>
        public string WindowName { get; private set; }
        /// <summary>
        /// 窗口实体
        /// </summary>
        public IWindow Window { get; set; }
        private bool _isShow;
        /// <summary>
        /// 窗口是否在显示窗台
        /// </summary>
        public bool IsShow
        {
            get { return _isShow; }
            set
            {
                if (_isShow != value)
                {
                    _isShow = value;
                    this.RaisePropertyChanged("IsShow");
                }
            }
        }
        #region 命令
        private DelegateCommand _showCommand;
        /// <summary>
        /// 打开命令
        /// </summary>
        public DelegateCommand ShowCommand
        {
            get
            {
                if (_showCommand == null)
                {
                    _showCommand = new DelegateCommand(this.Show);
                }
                return _showCommand;
            }
        }
        private DelegateCommand _closeCommand;
        /// <summary>
        /// 关闭命令
        /// </summary>
        public DelegateCommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new DelegateCommand(this.Close);
                }
                return _closeCommand;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 显示窗口
        /// </summary>
        public virtual void Show()
        {
            var window = this.UnityContainer.Resolve<IWindow>(this.WindowName);
            if (window != null)
            {
                this.Window = window;
                this.Window.DataContext = this;
                this.Window.Show();
                this.IsShow = true;
            }
        }
        /// <summary>
        /// 关闭窗口
        /// </summary>
        public virtual void Close()
        {
            if (this.Window != null)
            {
                this.Window.Close();
                this.Window = null;
                this.IsShow = false;
            }
        }
        #endregion
    }
}
