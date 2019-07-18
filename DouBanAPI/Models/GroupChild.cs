using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DouBanAPI.Models
{
    /// <summary>
    /// 频道列表实体
    /// </summary>
    [DataContract]
    public class GroupInfo
    {
       
        [DataMember(Name="chls")]
        public List<Channel> Chls { get; set; }
        /// <summary>
        /// </summary>
        [DataMember(Name = "group_id")]
        public string GroupId { get; set; }
        /// <summary>
        /// </summary>
        [DataMember(Name = "group_name")]
        public string GroupName { get; set; }
    }

    [DataContract]
    public class GroupListInfo
    {
        [DataMember(Name = "groups")]
        public List<GroupInfo> GroupList { get; set; }

    }

}
