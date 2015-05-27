using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace MS_LAB3
{
    public partial class MainPage : UserControl
    {
        CaptureSource captureSource;
        VideoBrush webcamBrush;

        public MainPage()
        {
            InitializeComponent();
        }

        private void webcamConfigure()
        {
            this.captureSource = new CaptureSource();

            VideoCaptureDevice webcam = CaptureDeviceConfiguration.GetDefaultVideoCaptureDevice();
            AudioCaptureDevice microphone = CaptureDeviceConfiguration.GetDefaultAudioCaptureDevice();

            this.captureSource.VideoCaptureDevice = webcam;
            this.captureSource.AudioCaptureDevice = microphone;
        }

        private void webcamCapture()
        {
            this.webcamBrush = new VideoBrush();
            this.webcamBrush.SetSource(this.captureSource);
            displayRectangle.Fill = webcamBrush;
        }

        private void buttonStartWebcam_Click(object sender, RoutedEventArgs e)
        {
            if(this.captureSource == null || this.webcamBrush == null)
            {
                webcamConfigure();
                webcamCapture();
            }    
            // Request access to the device and verify the VideoCaptureDevice is not null.
            if (CaptureDeviceConfiguration.RequestDeviceAccess() && this.captureSource.VideoCaptureDevice != null)
            {
                try
                {
                    this.captureSource.Start();
                }
                catch (InvalidOperationException ex)
                {
                    // Notify user that the webcam could not be started.
                    MessageBox.Show("There was a problem starting the webcam " + ex.Message);
                }
            }

            if (CaptureDeviceConfiguration.RequestDeviceAccess() && captureSource.VideoCaptureDevice != null)
            {
                try
                {
                    captureSource.Start();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void buttonStopWebcam_Click(object sender, RoutedEventArgs e)
        {
            if(this.captureSource.VideoCaptureDevice != null)
            {
                this.captureSource.Stop();
            }
        }
    }
}
