using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Common.Events.Search
{
    /// <summary>
    /// 选中搜索结果时触发的事件
    /// </summary>
    public class ChooseSearchResultEvent : CompositePresentationEvent<Logic.Models.SearchResult>
    {

    }
}
