using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Settings.ViewModels
{
    /// <summary>
    /// 分享设置窗口
    /// </summary>
    public class ShareSettingsViewModel : ChildWindowViewModelBase
    {
        public ShareSettingsViewModel()
            : base(WindowNames.ShareSettings)
        {

        }
        public override string Title
        {
            get
            {
                return "分享设置";
            }
        }
    }
}
