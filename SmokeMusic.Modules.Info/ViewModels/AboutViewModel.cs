using Microsoft.Practices.Prism.Commands;
using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using SmokeMusic.Common.Dialog;

namespace SmokeMusic.Modules.Info.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        #region 构造器
        public AboutViewModel()
        {
            this.CurrentVersion = "1.0.0";
        }
        #endregion

        #region 属性
        private string _currentVersion;
        /// <summary>
        /// 当前版本号
        /// </summary>
        public string CurrentVersion
        {
            get { return _currentVersion; }
            set
            {
                if (_currentVersion != value)
                {
                    _currentVersion = value;
                    this.RaisePropertyChanged("CurrentVersion");
                }
            }
        }
        /// <summary>
        /// 重写Title,用于注册Region
        /// </summary>
        public override string Title
        {
            get
            {
                return "Info";
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

        #region 命令
        
        #endregion
    }
}
