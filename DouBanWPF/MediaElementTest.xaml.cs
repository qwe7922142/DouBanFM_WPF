using System;
using System.Windows;
using System.Windows.Input;

namespace DouBanWPF
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class MediaElementTest
    {
        public MediaElementTest()
        {
            InitializeComponent();
            myMediaElement.LoadedBehavior = System.Windows.Controls.MediaState.Manual;
            myMediaElement.Source = new Uri(@"C:\C#\doubanWPF\DouBanWPF\DouBanWPF\media\bee.wmv");
        }
        // Play the media.
        private void OnMouseDownPlayMedia(object sender, MouseButtonEventArgs args)
        {
            // The Play method will begin the media if it is not currently active or 
            // resume media if it is paused. This has no effect if the media is
            // already running.
            myMediaElement.Play();

            // Initialize the MediaElement property values.
            InitializePropertyValues();
        }

        // Pause the media.
        private void OnMouseDownPauseMedia(object sender, MouseButtonEventArgs args)
        {
            // The Pause method pauses the media if it is currently running.
            // The Play method can be used to resume.
            myMediaElement.Pause();
        }

        // Stop the media.
        private void OnMouseDownStopMedia(object sender, MouseButtonEventArgs args)
        {
            // The Stop method stops and resets the media to be played from
            // the beginning.
            myMediaElement.Stop();
        }

        // Change the volume of the media.
        public void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.Volume = volumeSlider.Value;
        }

        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.
        private void MyMediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        // Jump to different parts of the media (seek to). 
        public void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            var sliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            var ts = new TimeSpan(0, 0, 0, 0, sliderValue);
            myMediaElement.Position = ts;
        }

        private void InitializePropertyValues()
        {
            // Set the media's starting Volume to the current value of the
            // its slider control.
            myMediaElement.Volume = volumeSlider.Value;
        }

       
    }
}
