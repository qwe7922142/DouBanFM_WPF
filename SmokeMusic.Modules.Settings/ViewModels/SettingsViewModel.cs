using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;

namespace SmokeMusic.Modules.Settings.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        #region 构造器
        public SettingsViewModel()
        {
            this.SettingsCommandList = new ObservableCollection<ChildWindowViewModelBase>();
            if (!IsInDesignMode)
            {
                this.SettingsCommandList.Add(this.UnityContainer.Resolve<GeneralSettingsViewModel>());
                this.SettingsCommandList.Add(this.UnityContainer.Resolve<UISettingsViewModel>());
                this.SettingsCommandList.Add(this.UnityContainer.Resolve<LyricsSettingsViewModel>());
                this.SettingsCommandList.Add(this.UnityContainer.Resolve<HotKeySettingsViewModel>());
                this.SettingsCommandList.Add(this.UnityContainer.Resolve<ShareSettingsViewModel>());
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 设置按钮集合
        /// </summary>
        public ObservableCollection<ChildWindowViewModelBase> SettingsCommandList { get; private set; }
        /// <summary>
        /// 重写Title,用于注册Region
        /// </summary>
        public override string Title
        {
            get
            {
                return "Settings";
            }
        }
        /// <summary>
        /// 重写KeepAlive,用于注册Region
        /// </summary>
        public override bool KeepAlive
        {
            get
            {
                return true;
            }
        }
        #endregion
    }
}
