using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SmokeMusic.Common;

namespace SmokeMusic.Modules.Notify
{
    public class NotifyModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }
        public NotifyModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.UnityContainer = unityContainer;
            this.RegionManager = regionManager;
        }
        public void Initialize()
        {
            var view = this.UnityContainer.Resolve<Views.Notify>();
            this.RegionManager.AddToRegion(RegionNames.NotifyRegion, view);
        }
    }
}
