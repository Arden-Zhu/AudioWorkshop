using System;

namespace AudioWorkshop.Helpers
{
    public class ProgressReportEventArgs : EventArgs
    {
        public bool IsRecording { get; set; }
        public int Seconds { get; set; }

        public ProgressReportEventArgs(bool isRecording, int seconds)
        {
            IsRecording = isRecording;
            Seconds = seconds;
        }
    }
}