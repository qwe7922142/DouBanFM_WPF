using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Settings.ViewModels
{
    /// <summary>
    /// 热键设置
    /// </summary>
    public class HotKeySettingsViewModel : ChildWindowViewModelBase
    {
        public HotKeySettingsViewModel()
            : base(WindowNames.HotKeySettings)
        {

        }
        public override string Title
        {
            get
            {
                return "热键设置";
            }
        }
    }
}
