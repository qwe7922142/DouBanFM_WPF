using SmokeMusic.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Commands;
using SmokeMusic.Common.Events.Channel;
using SmokeMusic.Common.Events.Search;
using SmokeMusic.Common.Events.Application;

namespace SmokeMusic.Modules.Channel.ViewModels
{
    public class ChannelListViewModel : ViewModelBase
    {
        #region 构造器
        public ChannelListViewModel()
        {
            this.MyChannelList = new ObservableCollection<Logic.Models.Channel>();
            if (!IsInDesignMode)
            {
                this.ChannelBLL = this.UnityContainer.Resolve<Logic.Core.Channel>();
                this.EventAggregator.GetEvent<ChooseSearchResultEvent>().Subscribe(this.OnChooseSearchResult);
            }
        }
        #endregion

        #region 属性
        private bool _hasErrors;
        /// <summary>
        /// 是否发生了错误
        /// </summary>
        public bool HasErrors
        {
            get { return _hasErrors; }
            set
            {
                if (_hasErrors != value)
                {
                    _hasErrors = value;
                    this.RaisePropertyChanged("HasErrors");
                }
            }
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
                    _currentChannel = null;
                    this.RaisePropertyChanged("CurrentChannel");
                    _currentChannel = value;
                    this.RaisePropertyChanged("CurrentChannel");

                    this.EventAggregator.GetEvent<ChangeChannelEvent>().Publish(value);
                }
            }
        }
        /// <summary>
        /// 所有频道
        /// </summary>
        public List<Logic.Models.Channel> AllChannel { get; private set; }        
        /// <summary>
        /// 私人频道列表
        /// </summary>
        public ObservableCollection<Logic.Models.Channel> MyChannelList { get; private set; }
       
        /// 重写Title,用于注册Region
        /// </summary>
        public override string Title
        {
            get
            {
                return "Channels";
            }
        }
        /// <summary>
        /// 重写KeepAlive,用于注册Region
        /// </summary>
        public override bool KeepAlive
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// 频道操作类
        /// </summary>
        public Logic.Core.Channel ChannelBLL { get; private set; }
        #endregion

        #region 命令
        private DelegateCommand<string> _searchCommand;
        /// <summary>
        /// 搜索命令
        /// </summary>
        public DelegateCommand<string> SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new DelegateCommand<string>(this.SearchChannel);
                }
                return _searchCommand;
            }
        }
        private DelegateCommand _reloadCommand;
        /// <summary>
        /// 重新加载频道命令
        /// </summary>
        public DelegateCommand ReloadCommand
        {
            get
            {
                if (_reloadCommand == null)
                {
                    _reloadCommand = new DelegateCommand(this.LoadChannels);
                }
                return _reloadCommand;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 加载所有频道
        /// </summary>
        public void LoadChannels()
        {
            //在TabControl中,每切换一次都会触发UserControl的Loaded事件
            //此处应先做次判断,避免无谓的请求服务器
            if (this.AllChannel != null) return;
            this.HasErrors = false;
            var action = new Func<List<Logic.Models.Channel>>(this.ChannelBLL.GetChannelList);
            action.BeginInvoke(ar =>
            {
                List<Logic.Models.Channel> channelList = null;
                try
                {
                    channelList = action.EndInvoke(ar);
                }
                catch
                {
                    this.EventAggregator.GetEvent<LoadChannelFailedEvent>().Publish("加载频道失败!请检查网络设置!");
                    this.HasErrors = true;
                    return;
                }
                this.AllChannel = channelList;
                this.InvokeOnUIDispatcher(new Action(() =>
                {
                    foreach (var item in channelList)
                    {
                        this.MyChannelList.Add(item);
                    }

                    //默认加载一个频道
                    this.CurrentChannel = this.MyChannelList.OrderBy(t => Guid.NewGuid()).FirstOrDefault();
                }));

            }, null);
        }
        /// <summary>
        /// 搜索DJ电台
        /// </summary>
        /// <param name="keywords"></param>
        public void SearchChannel(string keywords)
        {
            if (this.AllChannel == null) return;
            if (string.IsNullOrEmpty(keywords)) return;
          
            
        }
        /// <summary>
        /// 在选中搜索结果时触发的方法
        /// </summary>
        /// <param name="result"></param>
        public void OnChooseSearchResult(Logic.Models.SearchResult result)
        {
            if (result == null) return;
            var channel = new Logic.Models.Channel() { ID = "0", Name = result.Title };
            this.CurrentChannel = channel;
        }
        /// <summary>
        /// 所有模块加载完毕后会调用的方法
        /// 此时应该尝试加载频道列表
        /// </summary>
        /// <param name="data"></param>
        protected override void OnAllModulesLoaded(object data)
        {
            base.OnAllModulesLoaded(data);
            this.LoadChannels();
        }
        #endregion
    }
}
