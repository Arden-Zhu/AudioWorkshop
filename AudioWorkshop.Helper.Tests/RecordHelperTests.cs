using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using AudioWorkshop.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AudioWorkshop.Helper.Tests
{
    [TestClass]
    public class RecordHelperTests
    {
        [TestMethod]
        public void DoRecord()
        {
            var devices = DeviceHelper.GetCaptureDevices();
            var device = devices.Find(m => m.FriendlyName == "Microphone (4- Logitech USB Headset)");

            using (var r = new RecordHelper(device))
            {
                r.Start("test_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".wav");
                Thread.Sleep(5000);
                r.Stop();
            }
        }
    }
}
