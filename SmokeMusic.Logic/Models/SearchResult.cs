using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    /// <summary>
    /// 搜索结果中的项
    /// </summary>
    public class SearchResult
    {
        /// <summary>
		/// 生成 <see cref="ChannelSearchItem"/> class 的新实例。
		/// </summary>
		/// <param name="title">标题</param>
		/// <param name="picture">图片</param>
		/// <param name="link">链接</param>
		/// <param name="infomations">其他信息</param>
		/// <param name="isArtist">是否是艺术家</param>
		/// <param name="context">上下文</param>
        internal SearchResult(string title, string picture, string link, string[] infomations, bool isArtist, string context)
		{
			Title = title;
			Picture = picture;
			Link = link;
			Infomations = infomations;
			IsArtist = isArtist;
			Context = context;
		}
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture { get; private set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string Link { get; private set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        public string[] Infomations { get; private set; }
        /// <summary>
        /// 是否是艺术家
        /// </summary>
        public bool IsArtist { get; private set; }
        /// <summary>
        /// 上下文
        /// </summary>
        public string Context { get; private set; }
        /// <summary>
        /// 能否收听此项
        /// </summary>
        public bool CanContextPlay
        {
            get { return Context != null && Context.Length > 0; }
        }
    }
}
