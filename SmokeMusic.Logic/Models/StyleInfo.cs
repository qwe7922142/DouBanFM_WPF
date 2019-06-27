using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    [DataContract]
     public class StyleInfo
    {
        /// <summary>
        /// 频道编号
        /// </summary>
        [DataMember(Name = "display_text")]
        public string DisplayText { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "bg_color")]
        public string BgColor { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "layout_type")]
        public string LayoutType { get; set; }
        /// <summary>
        /// 频道名称
        /// </summary>
        [DataMember(Name = "bg_image")]
        public string BgImage { get; set; }
    }
}
