using SmokeMusic.Common;
using SmokeMusic.Modules.Player.ViewModels;
using SmokeMusic.Modules.Settings.ViewModels;

namespace SmokeMusic.Client.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel(
            UISettingsViewModel uiSettings,
            PlayerViewModel player
            )
        {
            this.UISettings = uiSettings;
            this.Player = player;
        }

        #region 属性

        public UISettingsViewModel UISettings { get; private set; }
        /// <summary>
        /// 播放器ViewModel
        /// </summary>
        public PlayerViewModel Player { get; private set; }
        #endregion
    }
}
