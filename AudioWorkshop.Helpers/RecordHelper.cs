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
        private IWaveIn captureDevice;
        // private string outputFilename;
        private WaveFileWriter writer;

        public event EventHandler<ProgressReportEventArgs> ProgressReport;
        public event EventHandler<ThreadExceptionEventArgs> OnException;

        public void Start(MMDevice device, string outputFilename)
        {
            if (device == null)
                throw new ArgumentNullException();

            // this.outputFilename = outputFilename;

            if (captureDevice == null)
            {
                captureDevice = CreateWaveInDevice(device);
            }

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
            FinalizeWaveFile();
            ProgressReport?.Invoke(this, new ProgressReportEventArgs(false, 0));

            if (e.Exception != null)
            {
                OnException?.Invoke(this, new ThreadExceptionEventArgs(e.Exception));
            }
        }

        private void FinalizeWaveFile()
        {
            writer?.Dispose();
            writer = null;
        }

        private void Cleanup()
        {
            captureDevice?.Dispose();
            captureDevice = null;

            FinalizeWaveFile();
        }

        public void Dispose()
        {
            Cleanup();
        }
    }
}
