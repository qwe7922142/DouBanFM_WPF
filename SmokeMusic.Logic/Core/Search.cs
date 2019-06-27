using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SmokeMusic.Logic.Core
{
    public class Search
    {
        public List<Models.SearchResult> RemoteSearch(string keywords, int pageIndex)
        {
            Parameters parameters = new Parameters();
            parameters["start"] = ((pageIndex - 1) * 15).ToString();
            parameters["search_text"] = keywords;
            string url = ConnectionBase.ConstructUrlWithParameters("http://music.douban.com/subject_search", parameters);

            ConnectionBase connection = new ConnectionBase(true);
            string file = string.Empty;
            try
            {
                file = connection.Get(url);
            }
            catch (Exception ex)
            {
                file = new ConnectionBase().Get("http://music.douban.com");
                file = new ConnectionBase().Get(url);
            }

            var searhResult = GetSearchItems(file);
            var previous = GetPreviousPageLink(file);
            var next = GetNextPageLink(file);
            return searhResult.ToList();
        }
        /// <summary>
        /// 获取搜索的结果
        /// </summary>
        /// <returns></returns>
        private IEnumerable<Models.SearchResult> GetSearchItems(string file)
        {
            List<Models.SearchResult> items = new List<Models.SearchResult>();
            try
            {
                bool isSearchFilterEnabled = true;

                //找出艺术家
                MatchCollection mc = Regex.Matches(file, @"<div class=\""result-item musician\"".*?>.*?</h3>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                foreach (Match mm in mc)
                {
                    string temp = mm.Groups[0].Value;
                    string titleTemp = Regex.Match(temp, @"<a.*?class=\""nbg\"".*?/?>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[0].Value;
                    string title = Regex.Match(titleTemp, @".*?title=\""([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    string link = Regex.Match(titleTemp, @".*?href=\""([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    string pictureTemp = Regex.Match(temp, @"<img.*?class=\""answer_pic\"".*?/?>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[0].Value;
                    string picture = Regex.Match(pictureTemp, @".*?src=\""([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    Match ma = Regex.Match(temp, @".*?href=\""http://douban\.fm/\?context=([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    string context = null;
                    if (ma != null) context = ma.Groups[1].Value;
                    Models.SearchResult item = new Models.SearchResult(title, picture, link, null, true, context);
                    if (!isSearchFilterEnabled || !string.IsNullOrEmpty(item.Context))
                        items.Add(item);
                }

                //找出专辑
                mc = Regex.Matches(file, @"<tr.*?class=\""item\"">.*?</tr>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                foreach (Match mm in mc)
                {
                    string temp = mm.Groups[0].Value;
                    string titleTemp = Regex.Match(temp, @"<a.*?class=\""nbg\"".*?/?>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[0].Value;
                    string subject = Regex.Match(titleTemp, @"href=\"".*?subject/(\d+)", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    string title = Regex.Match(titleTemp, @".*?title=\""([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    string link = Regex.Match(titleTemp, @".*?href=\""([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    string pictureTemp = Regex.Match(temp, @"<img.*?/?>", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[0].Value;
                    string picture = Regex.Match(pictureTemp, @".*?src=\""([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Singleline).Groups[1].Value;
                    Match ma = Regex.Match(temp, @".*?href=\""http://douban\.fm/\?context=([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Singleline);

                    string context = null;
                    if (ma != null && ma.Success) context = ma.Groups[1].Value;
                    if (string.IsNullOrEmpty(context))
                    {
                        context = MakeContext(subject);
                    }

                    Models.SearchResult item = new Models.SearchResult(title, picture, link, null, false, context);
                    if (!isSearchFilterEnabled || !string.IsNullOrEmpty(item.Context))
                        items.Add(item);
                }
            }
            catch { }

            return items;
        }
        private static string MakeContext(string subject)
        {
            try
            {
                string file = new ConnectionBase().Get("http://api.douban.com/music/subject/" + subject);
                if (file != null)
                {
                    MatchCollection mc = Regex.Matches(file, @"<db:attribute[^>]*index=""\d+""[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    foreach (Match ma in mc)
                    {
                        if (ma.Success)
                        {
                            Match ma2 = Regex.Match(ma.Value, @"name=\""tracks?\""", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                            if (ma2.Success)
                            {
                                return "channel:0|subject_id:" + subject;
                            }
                        }
                    }
                }
            }
            catch { }
            return null;
        }
        /// <summary>
        /// 获取上一页的链接
        /// </summary>
        /// <returns></returns>
        private static string GetPreviousPageLink(string file)
        {
            try
            {
                Match mc = Regex.Match(file, @"<span class=\""prev\"">.*?</span>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match mc2 = Regex.Match(mc.Groups[0].Value, @"<a href=\""([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                return mc2.Groups[1].Value;
            }
            catch { }
            return null;
        }
        /// <summary>
        /// 获取下一页的链接
        /// </summary>
        /// <returns></returns>
        private static string GetNextPageLink(string file)
        {
            try
            {
                Match mc = Regex.Match(file, @"<span class=\""next\"">.*?</span>", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                Match mc2 = Regex.Match(mc.Groups[0].Value, @"<a href=\""([^\""]+)\""", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                return mc2.Groups[1].Value;
            }
            catch { }
            return null;
        }
    }
}
