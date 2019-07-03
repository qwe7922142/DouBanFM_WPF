using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmokeMusic.Logic.Models
{
    [DataContract]
    public class SonglistInfo
    {
        /// <summary>
        /// 警告信息
        /// </summary>
        [DataMember(Name = "warning")]
        public String WarnString { get; set; }

        [DataMember(Name = "r")]
        public int R { get; set; }

        [DataMember(Name = "version_max")]
        public int version_max { get; set; }

        [DataMember(Name = "is_show_quick_start")]
        public int is_show_quick_start { get; set; }

        [DataMember(Name = "song")]
        public List<Song> Songs { get; set; }
    }
}