using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    /// <summary>
    /// 播放记录实体
    /// </summary>
    [DataContract]
    public class PlayRecord
    {
        /// <summary>
        /// 喜欢的歌曲数
        /// </summary>
        [DataMember(Name="liked")]
        public int Liked { get; set; }

        /// <summary>
        /// 不再播放的歌曲数
        /// </summary>
        [DataMember(Name = "banned")]
        public int Banned { get; set; }

        /// <summary>
        /// 播放过的歌曲数
        /// </summary>
        [DataMember(Name = "played")]
        public int Played { get; set; }
    }
}
