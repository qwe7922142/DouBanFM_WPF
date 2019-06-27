using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmokeMusic.Common.Commands
{
    public class NavigateCommand : DelegateCommand<string>
    {        
        static void Navigate(string viewName)
        {
            var unityContainer = ViewModelBase.StaticUnityContainer;
            var regionManager = unityContainer.Resolve<IRegionManager>();
            regionManager.RequestNavigate(RegionNames.PlayerRegion, viewName);
        }
        public NavigateCommand()
            : base(Navigate)
        {

        }
    }
}
