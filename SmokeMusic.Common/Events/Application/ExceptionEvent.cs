using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Common.Events.Application
{
    /// <summary>
    /// 异常发生时触发的事件
    /// 参数应携带具体的异常信息
    /// </summary>
    public class ExceptionEvent : CompositePresentationEvent<Exception>
    {
    }
}
