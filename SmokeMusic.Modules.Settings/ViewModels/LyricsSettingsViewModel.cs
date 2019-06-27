using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Settings.ViewModels
{
    /// <summary>
    /// 歌词设置
    /// </summary>
    public class LyricsSettingsViewModel : ChildWindowViewModelBase
    {
        public LyricsSettingsViewModel()
            : base(WindowNames.LyricsSettings)
        {

        }
        public override string Title
        {
            get
            {
                return "歌词设置";
            }
        }
    }
}
