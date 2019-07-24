using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Threading;

namespace DouBanWPF
{
    /// <summary>
    /// LyricsTest.xaml 的交互逻辑
    /// </summary>
    public partial class LyricsTestZhiHu : Window
    {
        private List<LyricItem> LyricsList;

        public LyricsTestZhiHu()
        {
            InitializeComponent();
        }

        private void Parse(string lyric)
        {
            if (LyricsList == null)
            {
                LyricsList = new List<LyricItem>();
            }
            LyricsList.Clear();
           
            List<string> Titles = new List<string>();
            List<string> Timespans = new List<string>();
            string[] arr = lyric.Split(new char[] { '\r', '\n' });
            Regex r = new Regex(@"\[\d{2}:\d{2}(.\d{2})*\]");
            if (arr != null && arr.Length > 0)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    var str = arr[i].Replace("by", "")
                            .Replace("all", "")
                            .Replace("ti", "")
                            .Replace("offset", "");
                    var matches = r.Matches(str);
                    if (matches != null && matches.Count > 0)
                    {
                        for (int j = 0; j < matches.Count; j++)
                        {
                            Timespans.Add(matches[j].Value);
                        }
                    }
                }
            }
            Timespans.Sort();
            Timespans.Distinct();

            for (int i = 0; i < Timespans.Count; i++)
            {
                for (int j = 0; j < arr.Length; j++)
                {
                    var index = arr[j].IndexOf(Timespans[i]);
                    if (index > -1)
                    {
                        LyricsList.Add(new LyricItem
                        {
                            Text = arr[j].Substring(arr[j].LastIndexOf(']') + 1).TrimEnd('\r').Trim(),
                            Time = GetSeconds(Timespans[i])
                        });
                        break;
                    }
                }
            }
        }

        private int GetSeconds(string v)
        {
            Match mc = Regex.Match(v, @"\[(?'minutes'\d+):(?'seconds'\d+(\.\d+)?)\]",
                                          RegexOptions.None);
            int m = int.Parse(mc.Groups["minutes"].Value);
            double s = double.Parse(mc.Groups["seconds"].Value);
            int seconds = (int)(m * 60 + s);
            return seconds;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = File.ReadAllText(@"C:\C#\doubanWPF\DouBanWPF\DouBanWPF\test.lrc");
            Parse(str);
            test.BuildDefault("周杰轮", "蔡徐坤呵呵哒");
            test.SetSource(this.LyricsList);
            //开始刷新歌曲进度
            _progressRefreshTimer = new DispatcherTimer();
            _progressRefreshTimer.Interval = new TimeSpan(0, 0, 1);
            _progressRefreshTimer.Tick += new EventHandler(timer_Tick);
            _progressRefreshTimer.Start();
        }
        /// <summary>
		/// 进度更新计时器
		/// </summary>
		private DispatcherTimer _progressRefreshTimer;
        private TimeSpan time;

        private void timer_Tick(object sender, EventArgs e)
        {
            time = time + _progressRefreshTimer.Interval;
            test.UpdatePosition(time);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
    public class LyricItem
    {
        public string Text { get; set; }
        public int Time { get; set; }
        public double TranslateY { get; set; }
    }

}
