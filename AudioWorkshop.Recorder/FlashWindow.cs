using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace AudioWorkshop.Recorder
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public uint cbSize;
        public IntPtr hwnd;
        public uint dwFlags;
        public uint uCount;
        public uint dwTimeout;
    }


    public class FlashWindow
    {
        // Methods
        private FlashWindow()
        {
        }

        public static bool Flash(IntPtr handleToWindow)
        {
            FLASHWINFO flashwinfo1 = new FLASHWINFO();
            flashwinfo1.cbSize = (uint)Marshal.SizeOf(flashwinfo1);
            flashwinfo1.hwnd = handleToWindow;
            flashwinfo1.dwFlags = 3;
            flashwinfo1.uCount = uint.MaxValue;
            flashwinfo1.dwTimeout = 0;
            return (FlashWindow.FlashWindowEx(ref flashwinfo1) == 0);
        }

        public static bool StopFlash(IntPtr handleToWindow)
        {
            FLASHWINFO flashwinfo1 = new FLASHWINFO();
            flashwinfo1.cbSize = (uint)Marshal.SizeOf(flashwinfo1);
            flashwinfo1.hwnd = handleToWindow;
            flashwinfo1.dwFlags = 0;
            return (FlashWindow.FlashWindowEx(ref flashwinfo1) == 0);
        }

        [DllImport("user32.dll")]
        private static extern short FlashWindowEx(ref FLASHWINFO pwfi);


        // Fields
        public const uint FLASHW_ALL = 3;
        public const uint FLASHW_CAPTION = 1;
        public const uint FLASHW_STOP = 0;
        public const uint FLASHW_TIMER = 4;
        public const uint FLASHW_TIMERNOFG = 12;
        public const uint FLASHW_TRAY = 2;
    }
}

