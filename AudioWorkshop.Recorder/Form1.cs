﻿using AudioWorkshop.Helpers;
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
        int CancelHotkeyId = 2;

        bool isRecording = false;
        RecordHelper recordHelper;
        PlaybackHelper playbackHelper;

        private string lastFileName;
        private bool isSkipPlaybackOnce;
        private string applicationFolder;
        private List<RecordedFile> recordedFiles = new List<RecordedFile>();
        private string nextPlayFile;

        public Form1()
        {
            InitializeComponent();
            applicationFolder = Path.GetDirectoryName(Application.ExecutablePath);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegisterHotKeys();

            InitDevices();

        }

        private void InitDevices()
        {
            var devices = DeviceHelper.GetCaptureDevices();
            var device = devices.Find(m => m.FriendlyName == "Microphone (4- Logitech USB Headset)");

            this.recordHelper = new RecordHelper(device);
            recordHelper.ProgressReport += RecordHelper_ProgressReport;

            this.playbackHelper = new PlaybackHelper();
            playbackHelper.PlayStopped += PlaybackHelper_PlayStopped;
        }

        private void PlaybackHelper_PlayStopped(object sender, StoppedEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<StoppedEventArgs>(PlaybackHelper_PlayStopped), sender, e);
            }
            else
            {
                if (this.nextPlayFile != null && File.Exists(nextPlayFile))
                {
                    playbackHelper.Play(this.nextPlayFile);
                    this.nextPlayFile = null;
                }
            }
        }

        private void RecordHelper_ProgressReport(object sender, ProgressReportEventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler<ProgressReportEventArgs>(RecordHelper_ProgressReport), sender, e);
            }
            else
            {
                if (this.isRecording && !e.IsRecording)
                {
                    // It is just stopped
                    FlashWindow.StopFlash(this.Handle);
                    if (chkPlayback.Checked && !isSkipPlaybackOnce)
                    {
                        playbackHelper.Play(this.lastFileName);
                    }

                    isSkipPlaybackOnce = false;
                    btnRecord.Text = "Record";
                    isRecording = false;
                    Output("Stop recording.");

                    var recordedFile = new RecordedFile(lastFileName, e.Seconds, ConvertSecondToString(e.Seconds));
                    AddNewRecordedFile(recordedFile);
                }
                else
                {
                    lblLength.Text = ConvertSecondToString(e.Seconds);
                }

                if (e.Exception != null)
                {
                    Output(e.Exception);
                }
            }
        }

        private void AddNewRecordedFile(RecordedFile recordedFile)
        {
            this.recordedFiles.Add(recordedFile);

            ListViewItem listViewItem = new ListViewItem(recordedFile.onlyFileName);
            listViewItem.SubItems.Add(recordedFile.length);
            listViewItem.SubItems.Add(recordedFile.whenCreated.ToString("HH:mm:ss"));
            listViewItem.SubItems.Add(recordedFile.tag);
            listViewItem.Tag = recordedFile;

            this.listView1.Items.Add(listViewItem);
        }

        private string ConvertSecondToString(int seconds)
        {
            int second = seconds % 60;
            seconds = seconds / 60;
            int minute = seconds % 60;
            int hour = seconds / 60;
            return $"{hour.ToString().PadLeft(2, '0')}:{minute.ToString().PadLeft(2, '0')}:{second.ToString().PadLeft(2, '0')}";
        }

        private void RegisterHotKeys()
        {
            int hotKeyCode = (int)Keys.F6;
            Boolean hotKeyRegistered = RegisterHotKey(
                this.Handle, RecordHotkeyId, 0x0000, hotKeyCode
            );

            if (!hotKeyRegistered)
            {
                Output("Global Hotkey F6 couldn't be registered !");
            }

            hotKeyCode = (int)Keys.F7;
            hotKeyRegistered = RegisterHotKey(
                this.Handle, CancelHotkeyId, 0x0000, hotKeyCode
            );

            if (!hotKeyRegistered)
            {
                Output("Global Hotkey F7 couldn't be registered !");
            }

        }

        private void Output(string message)
        {
            this.txtOutput.AppendText($"{message}...{DateTime.Now.ToString("HH:mm:ss")}\r\n");
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
                else if (id == CancelHotkeyId)
                {
                    CancelKeyPressed();
                }
            }

            base.WndProc(ref m);
        }

        private void CancelKeyPressed()
        {
            if (isRecording)
            {
                isSkipPlaybackOnce = true;
                StopRecording();
            }
            else if (playbackHelper.IsPlaying)
            {
                playbackHelper.Stop();
            }
            else if (!string.IsNullOrWhiteSpace(lastFileName) && File.Exists(lastFileName))
            {
                playbackHelper.Play(lastFileName);
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            RecordKeyPressed();
        }

        private void RecordKeyPressed()
        {
            if (!isRecording)
            {
                isRecording = true;
                try
                {
                    if (StartRecording())
                    {
                        btnRecord.Text = "Stop";
                        Output("Start recording.");
                    }
                }
                catch (Exception ex)
                {
                    Output(ex);
                }
            }
            else
            {
                StopRecording();
            }
        }

        private void Output(Exception ex)
        {
            Output($"Error: {ex.Message}");
            Output(ex.StackTrace);
        }

        private void StopRecording()
        {
            this.recordHelper.Stop();
        }

        private bool StartRecording()
        {
            if (this.playbackHelper.IsPlaying)
            {
                playbackHelper.Stop();
            }

            var fileName = GetFileName();
            if (fileName == this.lastFileName)
            {
                Output("Click too fast");
                return false;
            }
            else
            {
                this.lastFileName = fileName;

                this.recordHelper.Start(lastFileName);

                FlashWindow.Flash(this.Handle);
                return true;
            }
        }

        private string GetFileName()
        {
            if (!Directory.Exists("Wav"))
            {
                Directory.CreateDirectory("Wav");
            }

            return Path.Combine(applicationFolder, "Wav", $"{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.wav");
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                string fileName = (listView1.SelectedItems[0].Tag as RecordedFile)?.fileName;
                if (!string.IsNullOrWhiteSpace(fileName) && File.Exists(fileName))
                {
                    if (playbackHelper.IsPlaying)
                    {
                        playbackHelper.Stop();
                        this.nextPlayFile = fileName;
                    }
                    else
                    {
                        playbackHelper.Play(fileName);
                    }
                }
            }
        }
    }
}
