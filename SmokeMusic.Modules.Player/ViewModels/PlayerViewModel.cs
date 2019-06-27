using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Unity;
using SmokeMusic.Common;
using SmokeMusic.Common.Events.Channel;
using SmokeMusic.Common.Events.Song;

namespace SmokeMusic.Modules.Player.ViewModels
{
    public class PlayerViewModel : ViewModelBase
    {
        public PlayerViewModel()
        {
            this.MediaStateRequest = new InteractionRequest<Notifications.MediaStateNotification>();
            this.TempSongList = new List<Logic.Models.Song>();
            this.Volumn = 0.9;

            if (!IsInDesignMode)
            {
                this.SongBLL = this.UnityContainer.Resolve<Logic.Core.Song>();
                
                //对频道的更改事件做出监听
                this.EventAggregator.GetEvent<ChangeChannelEvent>().Subscribe(this.OnChangeChannel);
            }
        }

        #region 属性
        private Logic.Models.Channel _currentChannel;
        /// <summary>
        /// 当前选中的频道
        /// </summary>
        public Logic.Models.Channel CurrentChannel
        {
            get { return _currentChannel; }
            set
            {
                if (_currentChannel != value)
                {
                    _currentChannel = value;
                    this.RaisePropertyChanged("CurrentChannel");
                }
            }
        }
        private Logic.Models.Song _currentSong;
        /// <summary>
        /// 当前在听的歌曲
        /// </summary>
        public Logic.Models.Song CurrentSong
        {
            get { return _currentSong; }
            set
            {
                if (_currentSong != value)
                {
                    _currentSong = value;
                    this.RaisePropertyChanged("CurrentSong");
                    this.PlayOrPause();
                    this.EventAggregator.GetEvent<ChangeSongEvent>().Publish(value);
                }
            }
        }
        private TimeSpan _currentTime;
        /// <summary>
        /// 当前时间
        /// </summary>
        public TimeSpan CurrentTime
        {
            get { return _currentTime; }
            set
            {
                if (_currentTime != value)
                {
                    _currentTime = value;
                    this.RaisePropertyChanged("CurrentTime");
                }
            }
        }
        private TimeSpan _totalTime;
        /// <summary>
        /// 歌曲总持续时间
        /// </summary>
        public TimeSpan TotalTime
        {
            get { return _totalTime; }
            set
            {
                if (_totalTime != value)
                {
                    _totalTime = value;
                    this.RaisePropertyChanged("TotalTime");
                }
            }
        }
        private double _volumn;
        /// <summary>
        /// 音量
        /// </summary>
        public double Volumn
        {
            get { return _volumn; }
            set
            {
                if (value < 0) value = 0;
                if (value > 1) value = 1;
                if (_volumn != value)
                {                    
                    _volumn = value;
                    this.RaisePropertyChanged("Volumn");
                }
            }
        }
        private bool _isPlaying = true;
        /// <summary>
        /// 是否正在播放
        /// </summary>
        public bool IsPlaying
        {
            get { return _isPlaying; }
            set
            {
                if (_isPlaying != value)
                {
                    _isPlaying = value;
                    this.RaisePropertyChanged("IsPlaying");

                    //发出通知
                    this.PlayOrPause();                    
                }
            }
        }
        /// <summary>
        /// 是否可以播放歌曲
        /// </summary>
        public bool CanPlay
        {
            get
            {
                if (this.CurrentChannel == null) return false;
                if (this.TempSongList == null) return false;
                return true;
            }
        }
        /// <summary>
        /// 临时保存的歌曲列表
        /// </summary>
        public List<Logic.Models.Song> TempSongList
        {
            get;
            set;
        }
        /// <summary>
        /// 调度MediaElement的通知
        /// </summary>
        public InteractionRequest<Notifications.MediaStateNotification> MediaStateRequest
        {
            get;
            set;
        }
        /// <summary>
        /// 歌曲操作类
        /// </summary>
        public Logic.Core.Song SongBLL { get; set; }
        #endregion

        #region 命令
        private DelegateCommand _loadedCommand;
        /// <summary>
        /// 界面加载完毕命令
        /// </summary>
        public DelegateCommand LoadedCommand
        {
            get
            {
                if (_loadedCommand == null)
                {
                    _loadedCommand = new DelegateCommand(this.Loaded);
                }
                return _loadedCommand;
            }
        }

        private DelegateCommand<MediaElement> _mediaOpenedCommand;
        /// <summary>
        /// 歌曲打开触发的命令
        /// </summary>
        public DelegateCommand<MediaElement> MediaOpenedCommand
        {
            get
            {
                if (_mediaOpenedCommand == null)
                {
                    _mediaOpenedCommand = new DelegateCommand<MediaElement>(this.MediaOpened);
                }
                return _mediaOpenedCommand;
            }
        }
        private DelegateCommand _nextSongCommand;
        /// <summary>
        /// 播放下一曲的命令
        /// </summary>
        public DelegateCommand NextSongCommand
        {
            get
            {
                if (_nextSongCommand == null)
                {
                    _nextSongCommand = new DelegateCommand(this.PlayNextSong);
                }
                return _nextSongCommand;
            }
        }
        private DelegateCommand<TimeSpan?> _progressChangedCommand;
        /// <summary>
        /// 播放器进度变化时执行的命令
        /// </summary>
        public DelegateCommand<TimeSpan?> ProgressChangedCommand
        {
            get
            {
                if (_progressChangedCommand == null)
                {
                    _progressChangedCommand = new DelegateCommand<TimeSpan?>(this.ProgressChanged);
                }
                return _progressChangedCommand;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 频道更改时触发的方法
        /// </summary>
        /// <param name="channel"></param>
        void OnChangeChannel(Logic.Models.Channel channel)
        {
            this.CurrentChannel = channel;
            this.LoadSongList();
        }
        /// <summary>
        /// 在歌曲打开时调用
        /// </summary>
        /// <param name="naturalDuration"></param>
        public void MediaOpened(MediaElement player)
        {
            if (player.NaturalDuration.HasTimeSpan)
            {
                this.TotalTime = player.NaturalDuration.TimeSpan;
            }
            else
            {
                this.TotalTime = default(TimeSpan);
            }
            this.PlayOrPause();
        }
        /// <summary>
        /// 界面加载完毕执行的方法
        /// </summary>
        public void Loaded()
        {

        }
        /// <summary>
        /// 通知MediaElement播放或暂停
        /// </summary>
        public void PlayOrPause()
        {
            this.InvokeOnUIDispatcher(new Action(() =>
            {
                var noti = new Notifications.MediaStateNotification()
                {
                    MediaState = this.IsPlaying ? Enums.MediaState.Play : Enums.MediaState.Pause
                };
                this.MediaStateRequest.Raise(noti);
            }));
        }
        /// <summary>
        /// 加载歌曲
        /// </summary>
        public void LoadSongList()
        {
            if (this.IsLoading) return;
            this.IsLoading = true;
            var action = new Func<Logic.Models.Channel, Logic.Models.Song, string, Logic.Models.SonglistInfo>(this.SongBLL.GetSongList);
            action.BeginInvoke(this.CurrentChannel, this.CurrentSong, "n", ar =>
            {
                this.IsLoading = false;
                Logic.Models.SonglistInfo songList = null;
                try
                {
                    songList = action.EndInvoke(ar);
                }
                catch
                {
                    this.EventAggregator.GetEvent<LoadSongListFailedEvent>().Publish("加载歌曲列表失败!请检查网络链接!");
                }
                if (songList == null) return;
                this.TempSongList.Clear();
                this.TempSongList.AddRange(songList.Songs);
                this.CurrentSong = this.TempSongList.FirstOrDefault();
            }, null);
        }
        /// <summary>
        /// 播放下一首歌
        /// </summary>
        public void PlayNextSong()
        {
            if (!this.CanPlay) return;
            
            if (this.TempSongList == null)
            {
                //当还没有加载歌曲列表时,加载歌曲列表
                this.LoadSongList();
            }
            else
            {
                if (this.CurrentSong == null)
                {
                    //当加载了歌曲列表,当前歌曲为空时,播放歌曲列表的第一首
                    this.CurrentSong = this.TempSongList.FirstOrDefault();
                }
                else
                {                    
                    var pos = this.TempSongList.IndexOf(this.CurrentSong);
                    if (pos == this.TempSongList.Count - 1)
                    {
                        //当前歌曲已经是播放列表的最后一首歌,重新加载列表
                        this.LoadSongList();
                    }
                    else
                    {
                        //当前歌曲不是播放列表的最后一首歌,播放下一首
                        this.CurrentSong = this.TempSongList[pos + 1];
                    }
                }
            }
        }
        /// <summary>
        /// 歌曲进度改变时调用的方法
        /// </summary>
        /// <param name="currentTime"></param>
        public void ProgressChanged(TimeSpan? currentTime)
        {
            if (currentTime.HasValue)
            {
                this.CurrentTime = currentTime.Value;
            }
            else
            {
                this.CurrentTime = default(TimeSpan);
            }
        }
        #endregion
    }
}
