using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AudioWorkshop.Helpers
{
    public class RecordHelper : IDisposable
    {
        private readonly IWaveIn captureDevice;
        private WaveFileWriter writer;

        public event EventHandler<ProgressReportEventArgs> ProgressReport;

        public RecordHelper(MMDevice device)
        {
            captureDevice = CreateWaveInDevice(device);
        }
        public void Start(string outputFilename)
        {
            writer = new WaveFileWriter(outputFilename, captureDevice.WaveFormat);
            captureDevice.StartRecording();
        }

        public void Stop()
        {
            captureDevice?.StopRecording();
        }

        private IWaveIn CreateWaveInDevice(MMDevice device)
        {
            IWaveIn newWaveIn;
            // can't set WaveFormat as WASAPI doesn't support SRC
            newWaveIn = new WasapiCapture(device);

            // Forcibly turn on the microphone (some programs (Skype) turn it off).
            device.AudioEndpointVolume.Mute = false;

            newWaveIn.DataAvailable += OnDataAvailable;
            newWaveIn.RecordingStopped += OnRecordingStopped;
            return newWaveIn;
        }

        void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            writer.Write(e.Buffer, 0, e.BytesRecorded);
            int secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);
            if (ProgressReport != null)
                ProgressReport(this, new ProgressReportEventArgs(true, secondsRecorded));
        }

        void OnRecordingStopped(object sender, StoppedEventArgs e)
        {
            int secondsRecorded = (int)(writer.Length / writer.WaveFormat.AverageBytesPerSecond);
            FinalizeWaveFile();
            ProgressReport?.Invoke(this, new ProgressReportEventArgs(false, secondsRecorded, e.Exception));
        }

        private void FinalizeWaveFile()
        {
            writer?.Dispose();
            writer = null;
        }

        private void Cleanup()
        {
            captureDevice?.Dispose();

            FinalizeWaveFile();
        }

        public void Dispose()
        {
            Cleanup();
        }

        public bool IsRecording
        {
            get => this.writer != null;
        }
    }
}
