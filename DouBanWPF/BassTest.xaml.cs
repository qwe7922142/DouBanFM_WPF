using DoubanFM.Bass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DouBanWPF
{
    /// <summary>
    /// BassTest.xaml 的交互逻辑
    /// </summary>
    public partial class BassTest : Window
    {
        private bool isOk;

        public BassTest()
        {
            InitializeComponent();
            InitBass();
        }
        /// <summary>
		/// 初始化BASS
		/// </summary>
		private void InitBass()
        {
            try
            {
                BassEngine.ExplicitInitialize(null);
                BassEngine.Instance.SetDownloadRateRestriction(false);
                BassEngine.Instance.OpenSucceeded += Instance_OpenSucceeded;
            }
            catch (BassInitializationFailureException ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown(0);
            }
        }

        private void Instance_OpenSucceeded(object sender, EventArgs e)
        {
            timelineSlider.Maximum = BassEngine.Instance.ChannelLength.TotalSeconds;
        }

        /// <summary>
        /// 计时器响应函数，用于更新时间信息和歌词
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            timelineSlider.Value = BassEngine.Instance.ChannelPosition.TotalSeconds;
        }
        private void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs e)
        {
            BassEngine.Instance.Play();
        }

        private void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs e)
        {
            BassEngine.Instance.Pause();
        }

        private void OnMouseDownStopMedia(object sender, MouseButtonEventArgs e)
        {
            BassEngine.Instance.Stop();
        }
       
        /// <summary>
		/// 进度更新计时器
		/// </summary>
		private DispatcherTimer _progressRefreshTimer;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //开始刷新歌曲进度
            _progressRefreshTimer = new DispatcherTimer();
            _progressRefreshTimer.Interval = new TimeSpan(1000000);
            _progressRefreshTimer.Tick += new EventHandler(timer_Tick);
            _progressRefreshTimer.Start();

            BassEngine.Instance.OpenUrlAsync(@"http://mr3.doubanio.com/bcb5121c62d30625d254dada192cf074/0/fm/song/p14746_128k.mp4");
        }

        private void TimelineSlider_MouseDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void TimelineSlider_MouseUp(object sender, MouseButtonEventArgs e)
        {
          

        }

        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            BassEngine.Instance.Volume = volumeSlider.Value;

        }

        private void TimelineSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _progressRefreshTimer.Stop();

        }

        private void TimelineSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BassEngine.Instance.ChannelPosition = TimeSpan.FromMilliseconds(timelineSlider.Value);
            _progressRefreshTimer.Start();
        }
    }
}
