using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;

namespace SmokeMusic.Common.Events.Song
{
    /// <summary>
    /// 加载歌曲列表出错时触发的事件
    /// 参数应携带错误信息
    /// </summary>
    public class LoadSongListFailedEvent : CompositePresentationEvent<string>
    {
    }
}
