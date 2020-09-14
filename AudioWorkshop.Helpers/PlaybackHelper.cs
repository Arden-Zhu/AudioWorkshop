using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWorkshop.Helpers
{
    public class PlaybackHelper
    {
        private WaveOutEvent outputDevice;
        private AudioFileReader audioFile;

        public event EventHandler<StoppedEventArgs> PlayStopped;
        public void Play(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("File name should not be empty", nameof(fileName));
            }

            if (!File.Exists(fileName))
            {
                throw new IOException($"File {fileName} does not exist.");
            }

            if (outputDevice == null)
            {
                outputDevice = new WaveOutEvent();
                outputDevice.PlaybackStopped += OnPlaybackStopped;
            }
            if (audioFile == null)
            {
                audioFile = new AudioFileReader(fileName);
                outputDevice.Init(audioFile);
            }
            outputDevice.Play();
        }

        private void OnPlaybackStopped(object sender, StoppedEventArgs args)
        {
            outputDevice.Dispose();
            outputDevice = null;
            audioFile.Dispose();
            audioFile = null;
            PlayStopped?.Invoke(this, new StoppedEventArgs());
        }

        public void Stop()
        {
            outputDevice?.Stop();
        }

        public bool IsPlaying
        {
            get => outputDevice != null;
        }
    }
}
