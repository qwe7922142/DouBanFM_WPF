using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokeMusic.Common.Events.Channel;

namespace SmokeMusic.Modules.Player.ViewModels
{
    public class TitleViewModel : ViewModelBase
    {
        public TitleViewModel()
        {
            if (!IsInDesignMode)
            {
                this.EventAggregator.GetEvent<ChangeChannelEvent>().Subscribe(this.OnChangeChannel);
            }
        }
        void OnChangeChannel(Logic.Models.Channel channel)
        {
            this.CurrentChannel = channel;
        }
        private Logic.Models.Channel _currentChannel;
        /// <summary>
        /// 当前频道
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
    }
}
