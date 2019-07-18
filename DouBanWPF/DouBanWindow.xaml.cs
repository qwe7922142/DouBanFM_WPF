using DouBanAPI;
using DouBanAPI.Models;
using DoubanFM.Bass;
using DouBanWPF.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DouBanWPF
{
    /// <summary>
    /// Window.xaml 的交互逻辑
    /// </summary>
    public partial class DouBanWindow 
    {
        public DouBanWindow()
        {
            InitializeComponent();
            this.DataContext =  new DouBanWindowViewModel();
            #region 绑定系统Command
            CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand, (sender, e) =>
            {
                WindowState = WindowState.Minimized;
            }));

            CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand, (sender, e) =>
            {
                WindowState = WindowState.Maximized;
            }));

            CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand, (sender, e) =>
            {
                WindowState = WindowState.Normal;
            }));

            CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand, (sender, e) =>
            {
                Close();
            }));
            #endregion
        }
      
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //左键拖动
            this.DragMove();
            //pressed = true;
            //e.Handled = false;
        }
       
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ((DouBanWindowViewModel)this.DataContext).SetLyricPanel(this.LyricPanel);
            ((DouBanWindowViewModel)this.DataContext).ReloadChannel();
            ApplyProxy();
        }
        ProxyKinds proxyKinds = ProxyKinds.Default;
        /// <summary>
		/// 应用当前代理设置
		/// </summary>
		internal void ApplyProxy()
        {
            try
            {
                switch (proxyKinds)
                {
                    case ProxyKinds.Default:
                        ConnectionBase.UseDefaultProxy();
                        BassEngine.Instance.UseDefaultProxy();
                        break;

                    case ProxyKinds.None:
                        ConnectionBase.DontUseProxy();
                        BassEngine.Instance.DontUseProxy();
                        break;

                    case ProxyKinds.Custom:
                        //ConnectionBase.SetProxy(_player.Settings.ProxyHost, _player.Settings.ProxyPort, _player.Settings.ProxyUsername, _player.Settings.ProxyPassword);
                        //BassEngine.Instance.SetProxy(_player.Settings.ProxyHost, _player.Settings.ProxyPort, _player.Settings.ProxyUsername, _player.Settings.ProxyPassword);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        #region ProxyKinds
        /// <summary>
        /// 代理服务器类型
        /// </summary>
        public enum ProxyKinds
        {
            /// <summary>
            /// 默认代理服务器
            /// </summary>
            Default = 0,
            /// <summary>
            /// 不使用代理服务器
            /// </summary>
            None,
            /// <summary>
            /// 自定义代理服务器
            /// </summary>
            Custom
        }
        #endregion
       
        private void MetroDataGrid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DataGridCellInfo cell_Info = this.MetroDataGrid.SelectedCells[0];
            //用于测试
            Song song = cell_Info.Item as Song;
            string str = string.Format(@"http://music.douban.com/api/song/info?song_id={0}", song.SongID);
            Clipboard.SetText(str);
        }
    }
}
