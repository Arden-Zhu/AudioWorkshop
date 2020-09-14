using AudioWorkshop.Helpers;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioWorkshop.Recorder
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        int RecordHotkeyId = 1;
        MMDevice device;
        bool isRecording = false;
        RecordHelper recordHelper;
        PlaybackHelper playbackHelper;

        private string lastFileName;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegisterHotKeys();

            InitDevices();
        }

        private void InitDevices()
        {
            var devices = DeviceHelper.GetCaptureDevices();
            this.device = devices.Find(m => m.FriendlyName == "Microphone (4- Logitech USB Headset)");

            this.recordHelper = new RecordHelper();
            recordHelper.ProgressReport += RecordHelper_ProgressReport;

            this.playbackHelper = new PlaybackHelper();
            playbackHelper.PlayStopped += PlaybackHelper_PlayStopped;
        }

        private void PlaybackHelper_PlayStopped(object sender, StoppedEventArgs e)
        {
            
        }

        private void RecordHelper_ProgressReport(object sender, ProgressReportEventArgs e)
        {
            if (this.isRecording && !e.IsRecording)
            {
                // It is just stopped
                if (chkPlayback.Checked)
                {
                    playbackHelper.Play(this.lastFileName);
                }
                
                btnRecord.Text = "Record";
                isRecording = false;
                Output("Stop recording.");
            }
        }

        private void RegisterHotKeys()
        {
            int HotKeyCode = (int)Keys.F9;
            Boolean HotKeyRegistered = RegisterHotKey(
                this.Handle, RecordHotkeyId, 0x0000, HotKeyCode
            );

            if (!HotKeyRegistered)
            {
                Output("Global Hotkey F9 couldn't be registered !");
            }
        }

        private void Output(string message)
        {
            this.txtOutput.AppendText(message + "\r\n");
        }

        protected override void WndProc(ref Message m)
        {
            // Catch when a HotKey is pressed !
            if (m.Msg == 0x0312)
            {
                int id = m.WParam.ToInt32();

                if (id == RecordHotkeyId)
                {
                    RecordKeyPressed();
                }
            }

            base.WndProc(ref m);
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            RecordKeyPressed();
        }

        private void RecordKeyPressed()
        {
            if (!isRecording)
            {
                StartRecording();
                btnRecord.Text = "Stop";
                isRecording = true;
                Output("Start recording.");
            }
            else
            {
                StopRecording();
            }
        }

        private void StopRecording()
        {
            this.recordHelper.Stop();
        }

        private void StartRecording()
        {
            this.lastFileName = GetFileName();
            this.recordHelper.Start(device, lastFileName);
        }

        private string GetFileName()
        {
            if (!Directory.Exists("Wav"))
            {
                Directory.CreateDirectory("Wav");
            }

            return $"Wav/{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.wav";
        }
    }
}
