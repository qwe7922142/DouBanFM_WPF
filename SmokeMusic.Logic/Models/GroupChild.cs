using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    /// <summary>
    /// 频道列表实体
    /// </summary>
    [DataContract]
    public class GroupChild
    {
        /// <summary>
        /// 私人频道列表
        /// </summary>
        [DataMember(Name="chls")]
        public List<Channel> Chls { get; set; }
        /// <summary>
        /// 公共频道列表
        /// </summary>
        [DataMember(Name = "group_id")]
        public string GroupId { get; set; }
        /// <summary>
        /// DJ频道列表
        /// </summary>
        [DataMember(Name = "group_name")]
        public string GroupName { get; set; }
    }

    [DataContract]
    public class Groups
    {
        [DataMember(Name = "groups")]
        public List<GroupChild> ChannelGroups { get; set; }

    }

}
