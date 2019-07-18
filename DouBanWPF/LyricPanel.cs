using DouBanAPI.Lyrics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace DouBanWPF
{
    /// <summary>
    /// 要放到ScrollViewer中
    /// </summary>
    public class LyricPanel : Grid
    {
        private Lyrics lyrics;
        private double LINEHEGHT;
        private int index = 0;
        private int curentLyricPos = 0;
        private double defaultFontSize = 16;
        TextBlock defaultTB;

        public LyricPanel()
        {
            //new CompositeTransform();

            LINEHEGHT = 40;
            CenterToTop = 2;
            RenderTransform = new TransformGroup();
            defaultTB = new TextBlock
            {
                Text = "歌词正在加载请稍后。。。",
                FontSize = 36,
                Margin = new Thickness(0, (CenterToTop - 1) * LINEHEGHT, 0, 0)
            };
        }

        /// <summary>
        /// 中间行距离最上方行数
        /// </summary>
        public int CenterToTop { get; set; }

        public void Clear()
        {
            if (this.Children.Count > 0)
            {
                this.Children.Clear();
            }
            listYs.Clear();
        }
        List<double> listYs = new List<double>();
        List<TimeSpan> timeSpans = new List<TimeSpan>();
        public void SetDefault()
        {
            this.Children.Clear();
            this.Children.Add(defaultTB);
        }
        public void SetSource(Lyrics lyrics,Uri uri)
        {
            this.lyrics = lyrics;
            //this.Background = new ImageBrush() { ImageSource  = new BitmapImage(uri)};
            Clear();
            this.Height = (lyrics.TimeAndLyrics.Count + CenterToTop + 1) * LINEHEGHT;
            int i = 0;
            foreach (var item in lyrics.TimeAndLyrics.OrderBy(a=>a.Key))
            {
                TextBlock tbk = new TextBlock()
                {
                    RenderTransform = new TransformGroup(),
                    LineHeight = LINEHEGHT,
                    Text = item.Value,
                    TextWrapping = System.Windows.TextWrapping.WrapWithOverflow,
                    TextAlignment = TextAlignment.Center,
                    FontSize = defaultFontSize,
                    Margin = new Thickness(0, (i + CenterToTop) * LINEHEGHT, 0, LINEHEGHT),
                };
                listYs.Add(-i * LINEHEGHT);
                this.Children.Add(tbk);
                i++;

            }
            timeSpans = lyrics.TimeAndLyrics.Select(a => a.Key).ToList();
        }

        public void BuildDefault(string title, string artist)
        {
            if (this.Children.Count > 0)
            {
                this.Children.Clear();
            }

            TextBlock ti = new TextBlock
            {
                Text = title,
                FontSize = 36,
                Margin = new Thickness(0, (CenterToTop - 1) * LINEHEGHT, 0, 0)
            };

            TextBlock ar = new TextBlock
            {
                Text = artist,
                FontSize = 28,
                Margin = new Thickness(0, CenterToTop * LINEHEGHT, 0, 0)
            };

            this.Children.Add(ti);
            this.Children.Add(ar);
        }
    
        public void UpdatePosition(TimeSpan position)
        {
            if (lyrics == null || lyrics.TimeAndLyrics.Count == 0)
            {
                return;
            }

            index = GetCurrentIndex(position);
            if (index == curentLyricPos)
            {
                return;
            }
            if (index >= Children.Count)
            {
                return;
            }
            curentLyricPos = index;

            if (index > 0)
            {
                var previous = Children[index - 1] as TextBlock;
                previous.FontWeight = FontWeights.Normal;
                previous.FontSize = defaultFontSize;
                if (index > 1)
                {
                    previous = Children[index - 2] as TextBlock;
                    previous.FontWeight = FontWeights.Normal;
                    previous.FontSize = defaultFontSize;
                }
            }
            var current = Children[index] as TextBlock;
            current.FontWeight = FontWeights.Bold;
            current.FontSize = defaultFontSize + 5;
            Storyboard sb = new Storyboard();
            sb.Children.Add(GetTimeline(listYs[index]));
            sb.Begin();
        }

        private DoubleAnimationUsingKeyFrames GetTimeline(double y)
        {
            DoubleAnimationUsingKeyFrames daTranslate = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetProperty(daTranslate, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.Y)"));
            Storyboard.SetTarget(daTranslate, this);
            DiscreteDoubleKeyFrame keyframe2_1 = new DiscreteDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0)),
                Value = y + LINEHEGHT
            };
            DiscreteDoubleKeyFrame keyframe2_2 = new DiscreteDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4)),
                Value = y + LINEHEGHT
            };
            EasingDoubleKeyFrame keyframe2_3 = new EasingDoubleKeyFrame()
            {
                KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.8)),
                Value = y
            };
            keyframe2_3.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseOut };
            daTranslate.KeyFrames.Add(keyframe2_1);
            daTranslate.KeyFrames.Add(keyframe2_2);
            daTranslate.KeyFrames.Add(keyframe2_3);
            return daTranslate;
        }

        int GetCurrentIndex(TimeSpan ts)
        {
            int idx = 0;

            for (int i = 0; i < timeSpans.Count; i++)
            {
                if (timeSpans[i]> ts)
                {
                    idx = i - 1;
                    break;
                }
                if (i == timeSpans.Count - 1)
                {
                    idx = i;
                }
            }
            if (idx < 0)
            {
                idx = 0;
            }
            return idx;
        }

    }
}
