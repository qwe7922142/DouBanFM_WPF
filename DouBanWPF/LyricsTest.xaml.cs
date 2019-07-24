using DouBanAPI.Lyrics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace DouBanWPF
{
    /// <summary>
    /// LyricsTest.xaml 的交互逻辑
    /// </summary>
    public partial class LyricsTest : Window
    {
        public LyricsTest()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = File.ReadAllText(@"C:\C#\doubanWPF\DouBanWPF\DouBanWPF\test.lrc");
            Lyrics lyrics = new Lyrics(str);
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
   

}
