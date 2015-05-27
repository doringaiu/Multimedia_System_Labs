using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.DirectX;
using Microsoft.DirectX.DirectSound;
using System.Diagnostics;

namespace MS_LAB2
{
    class SoundDX
    {
        private string filePath;
        private Device dSound;
        private SecondaryBuffer sound;
        private BufferDescription dFlags;
        private int soundLength;
        private int soundByteLength;

        public SoundDX(string filePath, IntPtr windowHandle)
        {
            this.filePath = filePath;

            this.dSound = new Device();
            this.dSound.SetCooperativeLevel(windowHandle, CooperativeLevel.Priority);
            this.dFlags = new BufferDescription();
            this.dFlags.ControlFrequency = true;
            this.dFlags.ControlVolume = true;
            this.dFlags.ControlPan = true;
            this.dFlags.ControlEffects = true;

            this.sound = new SecondaryBuffer(this.filePath, this.dFlags, this.dSound);
            this.soundLength = this.sound.Caps.BufferBytes / sound.Format.AverageBytesPerSecond;
            this.soundByteLength = this.sound.Caps.BufferBytes;
    }

        public void playSound()
        {
            this.sound.Play(0, BufferPlayFlags.Default);
        }

        public void pauseSound()
        {
            this.sound.Stop();
        }

        public int getSoundLength()
        {
            return this.soundLength;
        }

        public void setFrequency(int frequency)
        {
            this.sound.Frequency = frequency; 
        }

        public void setPosition(int pos)
        {
            int selectedPosition;
            if(pos == 15)
            {
                selectedPosition = 1;
                this.sound.SetCurrentPosition(selectedPosition);
            }
            else
            {
                selectedPosition = Convert.ToInt32((1 / pos) * soundByteLength);
                this.sound.SetCurrentPosition(selectedPosition);
            }       
        }
    }
}
