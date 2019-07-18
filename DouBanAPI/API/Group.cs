using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DouBanAPI.API
{
    public class Group
    {
        public List<Models.GroupInfo> GetGroupInfoList()
        {
            var conn = new ConnectionBase();
            var json = conn.Get("https://api.douban.com/v2/fm/app_channels");
            Models.GroupListInfo groups = Framework.Common.Helpers.JsonHelper.Deserialize<Models.GroupListInfo>(json);
            return groups.GroupList;
        }
    }
}
