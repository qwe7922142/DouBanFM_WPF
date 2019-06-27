using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using SmokeMusic.Common;
using SmokeMusic.Common.Events.Channel;
using SmokeMusic.Common.Events.Song;

namespace SmokeMusic.Modules.Notify.ViewModels
{
    public class NotifyViewModel : ViewModelBase
    {
        #region 构造器
        public NotifyViewModel()
        {
            this.ShowErrorRequest = new InteractionRequest<Notifications.ErrorInfo>();
            this.DisposeTaskbarIconRequest = new InteractionRequest<Notification>();
            if (!IsInDesignMode)
            {
                this.EventAggregator.GetEvent<LoadChannelFailedEvent>().Subscribe(this.OnLoadChannelFailed, ThreadOption.UIThread);
                this.EventAggregator.GetEvent<LoadSongListFailedEvent>().Subscribe(this.OnLoadSongListFailed, ThreadOption.UIThread);
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 显示错误信息的通知
        /// </summary>
        public InteractionRequest<Notifications.ErrorInfo> ShowErrorRequest { get; private set; }
        /// <summary>
        /// 释放任务栏图标的通知
        /// </summary>
        public InteractionRequest<Notification> DisposeTaskbarIconRequest { get; private set; }
        #endregion

        #region 方法
        /// <summary>
        /// 加载频道失败时调用的方法
        /// </summary>
        /// <param name="data"></param>
        void OnLoadChannelFailed(string data)
        {
            this.NotifyError(data);
        }
        /// <summary>
        /// 加载歌曲列表失败时调用的方法
        /// </summary>
        /// <param name="data"></param>
        void OnLoadSongListFailed(string data)
        {
            this.NotifyError(data);
        }
        /// <summary>
        /// 应用程序退出时会执行的方法
        /// </summary>
        /// <param name="exitCode"></param>
        protected override void OnExit(int exitCode)
        {
            base.OnExit(exitCode);
            this.DisposeTaskbarIconRequest.Raise(null);
        }
        /// <summary>
        /// 通知错误
        /// </summary>
        /// <param name="content"></param>
        /// <param name="title"></param>
        void NotifyError(string content, string title = "错误")
        {
            this.ShowErrorRequest.Raise(new Notifications.ErrorInfo() { Title = title, Content = content });
        }
        #endregion
    }
}
