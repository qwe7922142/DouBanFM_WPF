using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Logic.Core
{
    public class Channel
    {
        public List<Models.Channel> GetChannelList()
        {
            var conn = new ConnectionBase();
            var json = conn.Get("https://api.douban.com/v2/fm/app_channels");
            Models.Groups  groups = Framework.Common.Helpers.JsonHelper.Deserialize<Models.Groups>(json);
            List<Models.Channel> list = new List<Models.Channel>();

            foreach (var groupsChild in groups.ChannelGroups)
            {
                foreach (var item in groupsChild.Chls)
                {
                    list.Add(item);
                }
            }
            return list;
        }
    }
}
