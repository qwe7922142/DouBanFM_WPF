using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace SmokeMusic.Common.Events.Application
{
    /// <summary>
    /// 所有模块加载完毕的事件
    /// 参数一般为null,也可以传递发起事件的对象引用
    /// </summary>
    public class AllModulesLoadedEvent : CompositePresentationEvent<object>
    {

    }
}
