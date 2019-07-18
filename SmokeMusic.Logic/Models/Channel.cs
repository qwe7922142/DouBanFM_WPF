using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    /// <summary>
    /// 频道信息实体
    /// </summary>
    [DataContract]
    public class Channel
    {
        /// <summary>
        /// 频道编号
        /// </summary>
        [DataMember(Name = "style")]
        public StyleInfo Style { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "cover")]
        public Uri Cover { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "channel_type")]
        public string ChannelType { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "intro")]
        public string Intro { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "song_num")]
        public int SongNum { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "id")]
        public string ID { get; set; }
    }
}
