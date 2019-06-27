using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SmokeMusic.Common;
using SmokeMusic.Common.Dialog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Settings
{
    public class SettingsModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }
        public SettingsModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.UnityContainer = unityContainer;
            this.RegionManager = regionManager;
        }
        public void Initialize()
        {
            //注册窗口
            this.UnityContainer.RegisterType<IWindow, Views.UISettings>(WindowNames.UISettings);
            this.UnityContainer.RegisterType<IWindow, Views.GeneralSettings>(WindowNames.GeneralSettings);

            var view = this.UnityContainer.Resolve<Views.Settings>();
            this.RegionManager.AddToRegion(RegionNames.LeftRegion, view);
        }
    }
}
