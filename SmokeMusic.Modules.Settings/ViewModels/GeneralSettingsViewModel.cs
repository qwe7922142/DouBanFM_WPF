using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Settings.ViewModels
{
    /// <summary>
    /// 常规设置
    /// </summary>
    public class GeneralSettingsViewModel : ChildWindowViewModelBase
    {
        public GeneralSettingsViewModel()
            : base(WindowNames.GeneralSettings)
        {

        }
        public override string Title
        {
            get
            {
                return "常规设置";
            }
        }
    }
}
