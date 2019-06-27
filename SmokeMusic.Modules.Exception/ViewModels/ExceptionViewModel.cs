using Microsoft.Practices.Prism.Events;
using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Modules.Exception.ViewModels
{
    public class ExceptionViewModel : ChildWindowViewModelBase
    {
        public ExceptionViewModel()
            : base(WindowNames.ExceptionWindow)
        {
            if (!IsInDesignMode)
            {
                this.EventAggregator.GetEvent<Common.Events.Application.ExceptionEvent>().Subscribe(this.OnException, ThreadOption.UIThread);
            }
        }

        #region 属性
        public override string Title
        {
            get
            {
                return "系统发生错误";
            }
        }
        #endregion

        #region 方法
        void OnException(System.Exception ex)
        {
            this.Show();
        }
        #endregion
    }
}
