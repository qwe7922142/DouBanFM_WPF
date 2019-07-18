using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DouBanWPF
{
    /// <summary>
    /// 要放到ScrollViewer中,知乎上的版本
    /// https://www.zhihu.com/question/40313484/answer/85992705
    /// </summary>
    public class LyricPanelZhiHu : Grid
    {
        private List<LyricItem> LyricsList;
        private double LINEHEGHT;
        private int index = 0;
        private int curentLyricPos = 0;
        private double defaultFontSize = 16;

        public LyricPanelZhiHu()
        {
            //new CompositeTransform();

            LINEHEGHT = 40;
            CenterToTop = 2;
            RenderTransform = new TransformGroup();
        }

        /// <summary>
        /// 中间行距离最上方行数
        /// </summary>
        public int CenterToTop { get; set; }

        public void Clear()
        {
            if (LyricsList != null)
            {
                LyricsList.Clear();
            }
            if (this.Children.Count > 0)
            {
                this.Children.Clear();
            }
        }

        public void SetSource(List<LyricItem> list)
        {
            if (this.Children.Count > 0)
            {
                this.Children.Clear();
            }

            this.Height = (list.Count + CenterToTop + 1) * LINEHEGHT;
            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                TextBlock tbk = new TextBlock()
                {
                    RenderTransform = new TransformGroup(),
                    LineHeight = LINEHEGHT,
                    Text = item.Text,
                    TextWrapping = System.Windows.TextWrapping.WrapWithOverflow,
                    TextAlignment = TextAlignment.Center,
                    FontSize = defaultFontSize,
                    Margin = new Thickness(0, (i + CenterToTop) * LINEHEGHT, 0, LINEHEGHT),
                };
                item.TranslateY = -i * LINEHEGHT;
                this.Children.Add(tbk);
            }
            this.LyricsList = list;
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
            if (LyricsList == null || LyricsList.Count == 0)
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
            sb.Children.Add(GetTimeline(LyricsList[index].TranslateY));
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
            int time = ts.Seconds;
            for (int i = 0; i < LyricsList.Count; i++)
            {
                if (LyricsList[i].Time > time)
                {
                    idx = i - 1;
                    break;
                }
                if (i == LyricsList.Count - 1)
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
