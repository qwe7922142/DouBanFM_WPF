using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SmokeMusic.Client
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// 启动器
        /// </summary>
        BootStrapper BootStrapper { get; set; }
        public App()
        {
            this.BootStrapper = new BootStrapper();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            base.OnStartup(e);
            this.BootStrapper.Run();

        }

        void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            this.BootStrapper.HandlerException(e.Exception);
            //MessageBox.Show("系统发生错误!");
            e.Handled = true;
        }
        protected override void OnExit(ExitEventArgs e)
        {
            this.BootStrapper.Exit(e.ApplicationExitCode);
            base.OnExit(e);
        }
    }
}
