using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Farming
{

    class Screenshot
    {
        #region Dll reference for GetWindowCapture
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rectangle rect);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(
         IntPtr hdc // handle to DC
         );
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleBitmap(
         IntPtr hdc,         // handle to DC
         int nWidth,      // width of bitmap, in pixels
         int nHeight      // height of bitmap, in pixels
         );
        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(
         IntPtr hdc,           // handle to DC
         IntPtr hgdiobj    // handle to object
         );
        [DllImport("gdi32.dll")]
        private static extern int DeleteDC(
         IntPtr hdc           // handle to DC
         );
        [DllImport("user32.dll")]
        private static extern bool PrintWindow(
         IntPtr hwnd,                // Window to copy,Handle to the window that will be copied.
         IntPtr hdcBlt,              // HDC to print into,Handle to the device context.
         UInt32 nFlags               // Optional flags,Specifies the drawing options. It can be one of the following values.
         );
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(
         IntPtr hwnd
         );
        #endregion

        public static Bitmap GetWindowCapture(IntPtr hWnd)
        {
            IntPtr hscrdc = GetWindowDC(hWnd);
            Rectangle windowRect = new Rectangle();
            GetWindowRect(hWnd, ref windowRect);
            int width = Math.Abs(windowRect.X - windowRect.Width);
            int height = Math.Abs(windowRect.Y - windowRect.Height);
            IntPtr hbitmap = CreateCompatibleBitmap(hscrdc, width, height);
            IntPtr hmemdc = CreateCompatibleDC(hscrdc);
            SelectObject(hmemdc, hbitmap);
            PrintWindow(hWnd, hmemdc, 0);
            Bitmap bmp = Image.FromHbitmap(hbitmap);
            DeleteDC(hscrdc);//Delete used objects
            DeleteDC(hmemdc);//Delete used objects
            return bmp;
        }
    }

}
