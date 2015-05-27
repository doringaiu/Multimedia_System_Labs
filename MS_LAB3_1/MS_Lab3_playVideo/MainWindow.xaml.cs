using System;
using System.Collections.Generic;
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

namespace MS_Lab3_playVideo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MediaPlayer player;
        string filePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonPlayVideo(object sender, RoutedEventArgs e)
        {
            openFile();
            GC.Collect();
            playVideo();
        }

        private void playVideo()
        {
            this.player = new MediaPlayer();
            player.Open(new Uri(this.filePath));
            VideoDrawing videoDrawing = new VideoDrawing();
            videoDrawing.Rect = new Rect(0, 0, 1280, 720);
            videoDrawing.Player = player;
            DrawingBrush brush = new DrawingBrush(videoDrawing);
            this.Background = brush;
            this.button.Visibility = Visibility.Hidden;
            this.player.Play();
        }

        private void openFile()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog(); 
            dlg.DefaultExt = ".WMV";
            dlg.Filter = "WMV Files (*.wmv)|*.wmv|MP4 Files (*.mp4)|*.mp4|AVI Files (*.avi)|*.avi";
            Nullable<bool> result = dlg.ShowDialog();
            this.filePath = dlg.FileName;      
        }

    }


}
