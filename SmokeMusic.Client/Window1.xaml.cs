using SmokeMusic.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SmokeMusic.Client
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        ObservableCollection<Channel> channels = new ObservableCollection<Channel>();
        ScrollViewer view;
        public Window1()
        {
            InitializeComponent();
            for (int i = 0; i < 40; i++)
            {
                channels.Add(new Channel()
                {
                    Name = "fuck" + i,
                    Cover = i % 2 == 0 ? new System.Uri("http://img3.doubanio.com/img/fmadmin/large/31878.jpg") : new System.Uri("http://img1.doubanio.com/img/fmadmin/medium/26385.jpg"),
                });
            }
            PersonalChannels.ItemsSource = channels;
            view = (ScrollViewer) PersonalChannels.Template.FindName("fuck1", PersonalChannels);
        }
    }
}
