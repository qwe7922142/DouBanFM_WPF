using System.Windows.Controls;
using SmokeMusic.Common;
using Microsoft.Practices.Unity;

namespace SmokeMusic.Modules.Player.Views
{
    /// <summary>
    /// Player.xaml 的交互逻辑
    /// </summary>
    public partial class Player : UserControl
    {
        public Player()
        {
            var vm = ViewModelBase.StaticUnityContainer.Resolve<ViewModels.PlayerViewModel>();
            this.DataContext = vm;
            InitializeComponent();            
        }
    }
}
