using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace SmokeMusic.Modules.Player.Behaviors
{
    /// <summary>
    /// 动态改变歌曲信息的行为
    /// </summary>
    public class ChangeSongInfoBehavior : Behavior<FrameworkElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;
        }
        void AssociatedObject_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e != null)
            {
                var song = e.NewValue as Logic.Models.Song;
                if (song != null)
                {
                    var sb = (Storyboard)this.AssociatedObject.FindResource("ChangeSongInfoStoryboard");
                    ((StringAnimationUsingKeyFrames)sb.Children[1]).KeyFrames[0].Value = song.Title;
                    ((StringAnimationUsingKeyFrames)sb.Children[2]).KeyFrames[0].Value = song.Artist;
                    ((StringAnimationUsingKeyFrames)sb.Children[3]).KeyFrames[0].Value = song.AlbumTitle;
                    sb.Begin();
                }
            }
        }
    }
}
