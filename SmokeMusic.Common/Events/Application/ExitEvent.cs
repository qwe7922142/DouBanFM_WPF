using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace SmokeMusic.Common.Events.Application
{
    /// <summary>
    /// 应用程序退出时触发的事件
    /// 参数应该携带退出代码
    /// </summary>
    public class ExitEvent : CompositePresentationEvent<int>
    {
    }
}
