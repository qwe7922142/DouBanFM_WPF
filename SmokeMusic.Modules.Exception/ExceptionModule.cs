using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SmokeMusic.Common.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Exception
{
    /// <summary>
    /// 异常处理模块
    /// </summary>
    public class ExceptionModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }
        public ExceptionModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.UnityContainer = unityContainer;
            this.RegionManager = regionManager;
        }
        public void Initialize()
        {
            //注册窗口
            this.UnityContainer.RegisterType<IWindow, Views.ExceptionWindow>(WindowNames.ExceptionWindow);

            var exceptionViewModel = new ViewModels.ExceptionViewModel();
            this.UnityContainer.RegisterInstance<ViewModels.ExceptionViewModel>(exceptionViewModel);
        }
    }
}
