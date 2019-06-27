using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokeMusic.Logic.Core
{
    public class Song
    {
        /// <summary>
        /// 根据频道和歌曲,得到歌曲列表
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="song"></param>
        /// <param name="type">n-New</param>
        /// <returns></returns>
        public Models.SonglistInfo GetSongList(Models.Channel channel, Models.Song song, string type = "n")
        {
            Parameters parameters = new Parameters();
            parameters["channel"] = channel.ID;
            parameters["from"] = "mainsite";
            parameters["pt"] = "0.0";
            parameters["kbps"] = "128";
            parameters["formats"] = "aac";
            parameters["alt"] = "json";
            parameters["app_name"] = "radio_iphone";

            parameters["apikey"] = "02646d3fb69a52ff072d47bf23cef8fd";
            parameters["client"] = "s%3Amobile%7Cy%3AiOS%2010.2%7Cf%3A115%7Cd%3Ab88146214e19b8a8244c9bc0e2789da68955234d%7Ce%3AiPhone7%2C1%7Cm%3Aappstore";
            parameters["client_id"] = "02646d3fb69a52ff072d47bf23cef8fd";
            parameters["icon_cate"] = "xlarge";
            parameters["udid"] = "b88146214e19b8a8244c9bc0e2789da68955234d";
            parameters["douban_udid"] = "b635779c65b816b13b330b68921c0f8edc049590";
            parameters["version"] = "115";
            parameters["type"] = type;
            parameters["channel"] = channel.ID;

            string url = ConnectionBase.ConstructUrlWithParameters("https://api.douban.com/v2/fm/playlist", parameters);
            //string url = ConnectionBase.ConstructUrlWithParameters("https://douban.fm/j/v2/user_info?avatar_size=large", null);
            //获取列表
            string json = new ConnectionBase().Get(url);
            var songList = Framework.Common.Helpers.JsonHelper.Deserialize<Models.SonglistInfo>(json);

            //将小图更换为大图
            foreach (var s in songList.Songs)
            {
                s.Picture = new Uri(s.Picture.ToString().Replace("/mpic/", "/lpic/").Replace("//otho.", "//img3."));
            }

            //去广告
            songList.Songs.RemoveAll(s => s.IsAd);

            return songList;
        }
    }
}
