using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    [DataContract]
    public class Song
    {
        [DataMember(Name = "all_play_sources")]
        public List<PlaySourceInfo> all_play_sources { get; set; }
        /// <summary>
        /// 唱片标题
        /// </summary>
        [DataMember(Name = "albumtitle")]
        public string AlbumTitle { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        [DataMember(Name = "picture")]
        public Uri Picture { get; set; }
        /// <summary>
        /// 歌曲路径
        /// </summary>
        [DataMember(Name = "url")]
        public Uri Url { get; set; }
        /// <summary>
        /// 唱片路径
        /// </summary>
        [DataMember(Name = "album")]
        public string Album { get; set; }

        /// <summary>
        /// 平均评分
        /// </summary>
        [DataMember(Name = "rating_avg")]
        public double Rating { get; set; }
        /// <summary>
        /// 发行时间
        /// </summary>
        [DataMember(Name = "public_time")]
        public string PublicTime { get; set; }
        /// <summary>
        /// 不知道是什么东西的ID
        /// </summary>
        [DataMember(Name = "ssid")]
        public string Ssid { get; set; }
        /// <summary>
        /// 当前用户是否喜欢
        /// </summary>
        [DataMember(Name = "like")]
        public bool Like { get; set; }
        /// <summary>
        /// 歌手
        /// </summary>
        [DataMember(Name = "artist")]
        public string Artist { get; set; }

        /// <summary>
        /// 歌曲名称
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }
        /// <summary>
        /// 普通音乐应该是""，广告应该是"T"
        /// </summary>
        [DataMember(Name = "subtype")]
        public string SubType { get; set; }
        /// <summary>
        /// 歌曲ID
        /// </summary>
        [DataMember(Name = "sid")]
        public string SongID { get; set; }
        /// <summary>
        /// 不知道是什么东西的长度
        /// </summary>
        [DataMember(Name = "length")]
        public double Length { get; set; }
        /// <summary>
        /// 专辑ID
        /// </summary>
        [DataMember(Name = "aid")]
        public string AlbumID { get; set; }
        /// <summary>
        /// 是否是广告
        /// </summary>
        public bool IsAd
        {
            get
            {
                return this.SubType == "T";
            }
        }
    }
}
