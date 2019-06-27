using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Account
{
    public class AccountModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }
        public AccountModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.UnityContainer = unityContainer;
            this.RegionManager = regionManager;
        }
        public void Initialize()
        {
            var view = this.UnityContainer.Resolve<Views.Account>();
            this.RegionManager.Regions[RegionNames.LeftRegion].Add(view);
        }
    }
}
