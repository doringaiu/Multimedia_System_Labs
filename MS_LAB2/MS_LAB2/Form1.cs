using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace MS_LAB2
{
    public partial class Form1 : Form
    {
        string filePath;
        SoundDX sound;
        int playbackSpeed;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Open Audio File";
            dlg.Filter = "WAV Files (*.wav*)|*.wav";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.filePath = dlg.FileName;
                this.sound = new SoundDX(this.filePath, this.Handle);
                this.pictureBox1.Visible = true;
                this.label1.Text = this.filePath.Split('\\').Last();
                this.label1.Visible = true;
                this.playbackSpeed = 1000;

            }
            dlg.Dispose();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
            Environment.Exit(0);
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("LAB2-MS", "About", MessageBoxButtons.OK);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(sound != null)
            {
                this.sound.playSound();
                this.progressBar1.Visible = true;
                this.progressBar1.Maximum = this.sound.getSoundLength() * 1000;
                this.timer1.Start();
            }     
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
            if(this.progressBar1.Value >= progressBar1.Maximum)
            {
                this.timer1.Stop();
                this.progressBar1.Value = 0;
                this.timer1.Enabled = false;
            }
            if(this.timer1.Enabled == false)
            {
                this.timer1.Start();       
            }
            this.progressBar1.Increment(playbackSpeed);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(sound != null)
            {
                this.sound.pauseSound();
                this.timer1.Stop();
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if(this.sound != null)
            {
                double temp = trackBar1.Value;
                double tBarValue = 1;
                if (temp > 10)
                {
                    tBarValue = temp / 10;
                }
                else if (temp < 10)
                {
                    tBarValue = temp / 10;
                }
                else
                {
                    tBarValue = 1;
                }

                playbackSpeed = Convert.ToInt32(tBarValue * 1000);
                this.sound.setFrequency(Convert.ToInt32(44000 * tBarValue));
            }     
        }
    }
}
