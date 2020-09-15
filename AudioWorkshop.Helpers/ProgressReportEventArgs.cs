using System;

namespace AudioWorkshop.Helpers
{
    public class ProgressReportEventArgs : EventArgs
    {
        public bool IsRecording { get; set; }
        public int Seconds { get; set; }
        public Exception Exception { get; set; }

        public ProgressReportEventArgs(bool isRecording, int seconds, Exception exception = null)
        {
            IsRecording = isRecording;
            Seconds = seconds;
            Exception = exception;
        }
    }
}