using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoElement
{
    /// <summary>
    /// Interaction logic for AdsControl.xaml
    /// </summary>
    public partial class AdsControl : UserControl
    {
        private List<string> videoList = new List<string>();
        private int index = 0;
        private bool isPlaying = false;
        public AdsControl()
        {
            InitializeComponent();
            this.Loaded += AdsControl_Loaded;
        }

        private void AdsControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                LoadList();
                videoPlayer.MediaEnded += VideoPlayer_MediaEnded;
            }
        }

        private void VideoPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            videoPlayer.Stop();
            videoPlayer.Play();
        }

        public void Play()
        {
            if (isPlaying)
                return;

            index++;
            if(index >= videoList.Count)
            {
                index = 0;
            }
            videoPlayer.Source = new Uri(videoList[index], UriKind.Relative);
            videoPlayer.Play();
            isPlaying = true;
        }

        public void Stop()
        {
            videoPlayer.Stop();
            isPlaying = false;
        }

        private void LoadList()
        {
            var appRoot = AppDomain.CurrentDomain.BaseDirectory;
            var videos = Directory.GetFiles(appRoot + "Video").Where(f => f.EndsWith(".wmv") || f.EndsWith(".mp4"));
            if(videos.Count() == 0)
            {
                MessageBox.Show("Video目录下广告视频不存在wmv视频");
                
            }
            videoList.AddRange(videos);
        }
    }
}
