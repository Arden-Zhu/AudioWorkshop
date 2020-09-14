using System;
using System.Diagnostics;
using AudioWorkshop.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AudioWorkshop.Helper.Tests
{
    [TestClass]
    public class DeviceHelperTests
    {
        [TestMethod]
        public void DoListCaptureDevice()
        {
            var devices = DeviceHelper.GetCaptureDevices();
            devices.ForEach(m => Debug.WriteLine(m.FriendlyName));
        }
    }
}
