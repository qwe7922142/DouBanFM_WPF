using DouBanAPI.Lyrics;
using DouBanAPI.Models;
using DoubanFM.Bass;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace DouBanWPF.ViewModel
{
    public class DouBanWindowViewModel : ViewModelBase
    {
        public Visibility shongGridVisible;
        public Visibility ShongGridVisible
        {
            get
            { return shongGridVisible; }
            set
            {
                shongGridVisible = value;
                this.RaisePropertyChanged("ShongGridVisible");
            }
        }
        public Visibility channelGridVisible;
        public Visibility ChannelGridVisible
        {
            get
            { return channelGridVisible; }
            set
            {
                channelGridVisible = value;
                this.RaisePropertyChanged("ChannelGridVisible");
            }
        }
       
        private Channel currentChannel;
        /// <summary>
        /// 当前频道
        /// </summary>
        public Channel CurrentChannel
        {
            get { return currentChannel; }
            set
            {
                //if (currentChannel != value)
                //{
                   
                //}
                currentChannel = value;
                ShongGridVisible = Visibility.Visible;
                ChannelGridVisible = Visibility.Hidden;
                this.RaisePropertyChanged("CurrentChannel");
                Songs.Clear();
                var list = SongBLL.GetSongList(CurrentChannel);
                var ll = list.Take(20);
                foreach (var item in ll)
                {
                    Songs.Add(item);
                }
                CurrentSong = Songs.First();
                var action = new Func<Song, Lyrics>(LyricsHelper.GetLyrics);
                this.lyricPanel.Dispatcher.Invoke(()=> {
                    this.lyricPanel.SetDefault();
                });
                action.BeginInvoke(CurrentSong, a =>
                {
                    try
                    {
                      string  LrcCode = action.EndInvoke(a).LrcCode;
                      CurrentLyrics = new Lyrics(LrcCode);
                        this.lyricPanel.Dispatcher.Invoke(()=> {
                            this.lyricPanel.SetSource(CurrentLyrics, CurrentSong.Picture);
                        });
                    }
                    catch (Exception ex)
                    {

                    }
                }, null);
               
            }
        }
   
       
        private void RefeshSong(Song song)
        {
            BassEngine.Instance.OpenUrlAsync(song.Url.ToString());
            BassEngine.Instance.ChannelPosition = new TimeSpan(0);
            BassEngine.Instance.Play();
            _progressRefreshTimer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            this.lyricPanel.UpdatePosition(BassEngine.Instance.ChannelPosition);
        }
        private Song currentSong;
        public Song CurrentSong
        {
            get => currentSong;
            set
            {
                if (currentSong != value&&value!=null)
                {
                    currentSong = value;
                    RefeshSong(currentSong);
                    this.RaisePropertyChanged("CurrentSong");
                }
            }
        }
        private DispatcherTimer _progressRefreshTimer;
        private Lyrics currentLyrics;
        public Lyrics CurrentLyrics
        {
            get => currentLyrics;
            set
            {
                if (currentLyrics != value)
                {
                    currentLyrics = value;

                    this.RaisePropertyChanged("CurrentLyrics");

                }
            }
        }
        /// <summary>
        /// 所有频道
        /// </summary>
        public ObservableCollection<Channel> AllChannel { get; private set; }
        LyricPanel lyricPanel;
        internal void SetLyricPanel(LyricPanel lyricPanel)
        {
            this.lyricPanel = lyricPanel;
        }

        public ObservableCollection<Song> Songs { get; private set; }
        
        private RelayCommand playOrPuseCommand;
        public RelayCommand PlayOrPuseCommand
        {
            get
            {
                if (playOrPuseCommand == null)
                    return new RelayCommand(() => {
                        if (Bass.IsPlaying)
                        {
                            Bass.Pause();
                        }
                        else
                        {
                            Bass.Play();
                        }
                    });
                return playOrPuseCommand;
            }
            set { playOrPuseCommand = value; }
        }
        private RelayCommand reloadCommand;
        public RelayCommand ReloadCommand
        {
            get
            {
                if (reloadCommand == null)
                    return new RelayCommand(() => ReloadChannel());
                return reloadCommand;
            }
            set { reloadCommand = value; }
        }
        private RelayCommand lastPageCommand;
        public RelayCommand LastPageCommand
        {
            get
            {
                if (lastPageCommand == null)
                    return new RelayCommand(() => {
                        this.ChannelGridVisible = Visibility.Visible;
                        this.ShongGridVisible = Visibility.Hidden;
                    });
                return lastPageCommand;
            }
            set { lastPageCommand = value; }
        }
        private RelayCommand nextPageCommand;
        public RelayCommand NextPageCommand
        {
            get
            {
                if (nextPageCommand == null)
                    return new RelayCommand(() => {
                        this.ChannelGridVisible = Visibility.Hidden;
                        this.ShongGridVisible = Visibility.Visible;
                    });
                return nextPageCommand;
            }
            set { nextPageCommand = value; }
        }
      
        public bool HasErrors { get; private set; }
        public DouBanAPI.API.Song SongBLL { get; private set; }
        public DouBanAPI.API.Channel ChannelBLL { get; private set; }
        public BassEngine Bass { get; private set; }
        public void ReloadChannel()
        {
            //在TabControl中,每切换一次都会触发UserControl的Loaded事件
            //此处应先做次判断,避免无谓的请求服务器
            this.ShongGridVisible = Visibility.Hidden;
            this.ChannelGridVisible = Visibility.Visible;
            if (this.AllChannel.Count != 0) return;
            this.HasErrors = false;
            var action = new Func<List<Channel>>(this.ChannelBLL.GetChannelList);
            action.BeginInvoke(ar =>
            {
                List<Channel> channelList = null;
                try
                {
                    channelList = action.EndInvoke(ar);
                }
                catch
                {
                    this.HasErrors = true;
                    return;
                }
               
                this.InvokeOnUIDispatcher(new Action(() =>
                {
                    //这里这样写肯定就不能继续绑定了 要遍历赋值才行
                    //this.AllChannel = new ObservableCollection<Channel>(channelList);
                    foreach (var item in channelList.OrderBy(a => a.CoverSize))
                    {
                        this.AllChannel.Add(item);
                    }
                    //默认加载一个频道
                    //this.CurrentChannel = this.AllChannel.OrderBy(t => Guid.NewGuid()).FirstOrDefault();

                }));

            }, null);
        }
        protected void InvokeOnUIDispatcher(Delegate callback, params object[] args)
        {
            Application.Current.Dispatcher.BeginInvoke(callback, args);
        }
        public DouBanWindowViewModel()
        {
            this.AllChannel = new ObservableCollection<Channel>();
            this.Songs = new ObservableCollection<Song>();
            if (IsInDesignMode)
            {
                for (int i = 0; i < 40; i++)
                {
                    this.AllChannel.Add(new Channel {SongNum=10, Name = "test" });
                }
               
            }
            else
            {
                this.ChannelBLL = new DouBanAPI.API.Channel();
                this.SongBLL = new DouBanAPI.API.Song();
            }
            InitBass();
            ChannelGridVisible = Visibility.Visible;
            ShongGridVisible = Visibility.Hidden;
            //开始刷新歌曲进度
            _progressRefreshTimer = new DispatcherTimer();
            _progressRefreshTimer.Interval = new TimeSpan(0, 0, 1);
            _progressRefreshTimer.Tick += new EventHandler(timer_Tick);
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
                BassEngine.Instance.Volume = 0.5;
                Bass = BassEngine.Instance;
            }
            catch (BassInitializationFailureException ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButton.OK, MessageBoxImage.Error);
                App.Current.Shutdown(0);
            }
        }

        private void Instance_OpenSucceeded(object sender, EventArgs e)
        {
        }
    }
}