using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using SmokeMusic.Common;
using System;

namespace SmokeMusic.Modules.Player
{
    public class PlayerModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }
        public PlayerModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            this.UnityContainer = unityContainer;
            this.RegionManager = regionManager;
        }
        public void Initialize()
        {
            var playerView = this.UnityContainer.Resolve<Views.Shell>();
            var coverView = this.UnityContainer.Resolve<Views.Cover>();

            this.RegionManager.AddToRegion(RegionNames.PlayerRegion, playerView);
            this.RegionManager.AddToRegion(RegionNames.CoverRegion, coverView);
        }
    }
}
