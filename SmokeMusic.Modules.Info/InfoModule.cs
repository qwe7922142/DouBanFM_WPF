using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SmokeMusic.Common;
using SmokeMusic.Common.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SmokeMusic.Modules.Info
{
    public class InfoModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }
        public InfoModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.UnityContainer = unityContainer;
            this.RegionManager = regionManager;
        }

        public void Initialize()
        {
            var view = this.UnityContainer.Resolve<Views.About>();
            this.RegionManager.AddToRegion(RegionNames.LeftRegion, view);

            //注册子窗口
            this.UnityContainer.RegisterType<IWindow, Views.Help>(WindowNames.Help);
        }
    }
}
