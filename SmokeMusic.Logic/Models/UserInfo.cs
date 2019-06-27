using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmokeMusic.Logic.Models
{
    [DataContract]
    public class UserInfo
    {
        /// <summary>
        /// 用户主页
        /// </summary>
        [DataMember(Name = "access_token")]
        public string access_token { get; set; }

        /// <summary>
        /// 用于注销的字符串
        /// </summary>
        [DataMember(Name = "douban_user_name")]
        public string douban_user_name { get; set; }



        /// <summary>
        /// 用户ID
        /// </summary>
        [DataMember(Name = "douban_user_id")]
        public string douban_user_id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember(Name = "expires_in")]
        public string expires_in { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DataMember(Name = "refresh_token")]
        public string refresh_token { get; set; }
    }
}
