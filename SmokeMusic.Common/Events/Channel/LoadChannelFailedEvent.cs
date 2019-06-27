using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace SmokeMusic.Common.Events.Channel
{
    /// <summary>
    /// 加载频道失败时触发的事件
    /// 参数应该携带错误信息
    /// </summary>
    public class LoadChannelFailedEvent : CompositePresentationEvent<string>
    {

    }
}
