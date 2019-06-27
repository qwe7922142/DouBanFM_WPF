using System;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using SmokeMusic.Common;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using SmokeMusic.Common.Events.Application;

namespace SmokeMusic.Client
{
    /// <summary>
    /// 应用程序启动器
    /// </summary>
    public class BootStrapper : UnityBootstrapper
    {
        #region 属性
        /// <summary>
        /// 事件聚合器
        /// </summary>
        IEventAggregator EventAggregator
        {
            get
            {
                return this.Container.Resolve<IEventAggregator>();
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 创建应用程序壳子
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            var shell = this.Container.Resolve<Shell>();
            var vm = this.Container.Resolve<ViewModels.ShellViewModel>();
            shell.DataContext = vm;
            Application.Current.MainWindow = shell;
            shell.Show();

            return shell;
        }
        /// <summary>
        /// 创建基于配置文件的依赖注入容器
        /// </summary>
        /// <returns></returns>
        protected override IUnityContainer CreateContainer()
        {
            var container = base.CreateContainer();
            container.LoadConfiguration();

            //在需要用到的地方初始化依赖注入容器
            ViewModelBase.InitContainer(container);

            return container;
        }
        /// <summary>
        /// 创建基于配置文件的模块目录
        /// </summary>
        /// <returns></returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            //选择从配置文件加载模块
            return new ConfigurationModuleCatalog();
        }

        /// <summary>
        /// 初始化所有模块
        /// </summary>
        protected override void InitializeModules()
        {
            base.InitializeModules();

            var moduleManager = this.Container.Resolve<IModuleManager>();

            //注册模块加载完毕的事件
            moduleManager.LoadModuleCompleted += moduleManager_LoadModuleCompleted;

            foreach (var module in this.ModuleCatalog.Modules)
            {
                moduleManager.LoadModule(module.ModuleName);
            }
        }

        /// <summary>
        /// 每个模块加载完毕都会调用的方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            if (this.ModuleCatalog.Modules.Count(t => t.State == ModuleState.Initialized) == this.ModuleCatalog.Modules.Count())
            {
                var eventAggregator = this.Container.Resolve<IEventAggregator>();
                eventAggregator.GetEvent<AllModulesLoadedEvent>().Publish(this);
            }            
        }
        /// <summary>
        /// 应用程序退出时调用的方法
        /// </summary>
        public void Exit(int exitCode)
        {
            this.EventAggregator.GetEvent<ExitEvent>().Publish(exitCode);
        }
        /// <summary>
        /// 应用程序发生异常时触发的方法
        /// </summary>
        /// <param name="ex"></param>
        public void HandlerException(Exception ex)
        {
            this.EventAggregator.GetEvent<ExceptionEvent>().Publish(ex);
        }
        #endregion
    }
}
