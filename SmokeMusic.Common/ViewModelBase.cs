using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Commands;
using SmokeMusic.Common.Dialog;
using SmokeMusic.Common.Commands;
using SmokeMusic.Common.Events.Application;

namespace SmokeMusic.Common
{
    public class ViewModelBase : NotificationObject, IRegionMemberLifetime, IDataErrorInfo, INavigationAware
    {
        #region 构造器
        public ViewModelBase()
        {
            if (!IsInDesignMode)
            {
                this.EventAggregator.GetEvent<AllModulesLoadedEvent>().Subscribe(this.OnAllModulesLoaded, ThreadOption.UIThread);
                this.EventAggregator.GetEvent<ExitEvent>().Subscribe(this.OnExit, ThreadOption.UIThread);
            }
        }
        #endregion

        #region 字段
        public static IUnityContainer StaticUnityContainer;
        #endregion

        #region 属性
        /// <summary>
        /// 显示的标题
        /// </summary>
        public virtual string Title
        {
            get
            {
                return null;
            }
        }        
        private bool _isLoading;
        /// <summary>
        /// 是否正在加载中
        /// </summary>
        public virtual bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                if (_isLoading != value)
                {
                    _isLoading = value;
                    this.RaisePropertyChanged("IsLoading");
                }
            }
        }
        /// <summary>
        /// 当前是否处于设计器模式
        /// </summary>
        public bool IsInDesignMode
        {
            get
            {
                return DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject());
            }
        }
        /// <summary>
        /// RegionManager实例
        /// </summary>
        public IRegionManager RegionManager
        {
            get
            {
                return this.UnityContainer.Resolve<IRegionManager>();
            }
        }
        /// <summary>
        /// 依赖注入容器
        /// </summary>
        public IUnityContainer UnityContainer
        {
            get
            {
                return StaticUnityContainer;
            }
        }
        /// <summary>
        /// 事件聚合器
        /// </summary>
        public IEventAggregator EventAggregator
        {
            get
            {
                return this.UnityContainer.Resolve<IEventAggregator>();
            }
        }
        /// <summary>
        /// 对话框服务
        /// </summary>
        public Dialog.IDialogService DialogService
        {
            get
            {
                return this.UnityContainer.Resolve<Dialog.IDialogService>();
            }
        }
        #endregion

        #region 命令
        private DelegateCommand<string> _showWindowCommand;
        /// <summary>
        /// 打开窗口命令
        /// </summary>
        public DelegateCommand<string> ShowWindowCommand
        {
            get
            {
                if (_showWindowCommand == null)
                {
                    _showWindowCommand = new DelegateCommand<string>((t) =>
                    {
                        var window = this.UnityContainer.Resolve<IWindow>(t);
                        if (window != null)
                        {
                            window.Show();
                        }
                    });
                }
                return _showWindowCommand;
            }
        }
        OpenLinkCommand _openLinkCommand;
        /// <summary>
        /// 打开链接命令
        /// </summary>
        public OpenLinkCommand OpenLinkCommand
        {
            get
            {
                if (_openLinkCommand == null)
                {
                    _openLinkCommand = new Common.Commands.OpenLinkCommand();
                }
                return _openLinkCommand;
            }
        }
        #endregion

        #region IRegionMemberLifetime成员
        public virtual bool KeepAlive
        {
            get { return false; }
        }
        #endregion

        #region 方法
        protected void InvokeOnUIDispatcher(Delegate callback, params object[] args)
        {
            Application.Current.Dispatcher.BeginInvoke(callback, args);
        }
        public static void InitContainer(IUnityContainer unityContainer)
        {
            if (StaticUnityContainer == null)
            {
                StaticUnityContainer = unityContainer;
            }
        }
        #endregion

        #region IDataErrorInfo成员
        public virtual string Error
        {
            get { return null; }
        }

        public virtual string this[string columnName]
        {
            get { return null; }
        }
        #endregion

        #region INavigationAware成员
        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
        #endregion

        #region 注册应用程序事件的方法
        /// <summary>
        /// 在所有模块加载完毕时会执行的方法
        /// </summary>
        /// <param name="data"></param>
        protected virtual void OnAllModulesLoaded(object data)
        {
 
        }
        /// <summary>
        /// 在应用程序退出时会执行的方法
        /// </summary>
        /// <param name="exitCode"></param>
        protected virtual void OnExit(int exitCode)
        {
 
        }
        #endregion
    }
}
