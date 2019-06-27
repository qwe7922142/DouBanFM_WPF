using Microsoft.Practices.Prism.Events;
using SmokeMusic.Common;
using SmokeMusic.Common.Events.Song;

namespace SmokeMusic.Modules.Player.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CoverViewModel : ViewModelBase
    {
        public CoverViewModel()
        {
            if (!IsInDesignMode)
            {
                this.EventAggregator.GetEvent<ChangeSongEvent>().Subscribe(this.OnChangeSong, ThreadOption.BackgroundThread, true);
            }
        }

        private Logic.Models.Song _currentSong;
        /// <summary>
        /// 当前播放的歌曲
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
                }
            }
        }
        #region 方法
        void OnChangeSong(Logic.Models.Song song)
        {
            this.CurrentSong = song;
        }
        #endregion
    }
}
