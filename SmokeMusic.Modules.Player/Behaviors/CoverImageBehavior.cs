using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Imaging;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using SmokeMusic.Common;
using SmokeMusic.Common.Events.Player;

namespace SmokeMusic.Modules.Player.Behaviors
{
    /// <summary>
    /// 此行为主要是处理Cover层的大图片更改
    /// 图片下载成功后才通知窗口更改背景色,但是由于Image控件无下载事件,因此做了Behavior,内部放一个BitmapImage来处理
    /// </summary>
    public class CoverImageBehavior : Behavior<Image>
    {
        IEventAggregator EventAggregator
        {
            get
            {
                return ViewModelBase.StaticUnityContainer.Resolve<IEventAggregator>();
            }
        }
        /// <summary>
        /// 图片路径,更改时会生成一个BitmapImage,并绑定到Image控件上
        /// </summary>
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(CoverImageBehavior), new PropertyMetadata(ImageSourceChanged));

        /// <summary>
        /// ImageSourceProperty改变时回调的事件处理函数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void ImageSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as CoverImageBehavior;
            behavior.ChangeImageSource((string)e.NewValue);
        }

        /// <summary>
        /// 更改Cover图片
        /// 具体处理流程为:创建一个BitmapImage,绑定到Image控件,下载完毕后发出更改背景色的通知
        /// </summary>
        /// <param name="uri"></param>
        public void ChangeImageSource(string uri)
        {
            if (uri == null) return;
            var img = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
            img.DownloadCompleted += (sender, e) =>
            {
                //图片下载完毕后,通知窗口更改背景色
                this.EventAggregator.GetEvent<ChangeBackgroundEvent>().Publish(img);
            };
            this.AssociatedObject.Source = img;
        }
    }
}
