using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokeMusic.Common;

namespace SmokeMusic.Modules.Channel
{
    public class ChannelModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }
        public ChannelModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.UnityContainer = unityContainer;
            this.RegionManager = regionManager;
        }
        public void Initialize()
        {
            var view = this.UnityContainer.Resolve<Views.ChannelList>();
            this.RegionManager.AddToRegion(RegionNames.LeftRegion, view);
        }
    }
}
