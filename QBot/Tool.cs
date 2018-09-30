using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace QMapleBot
{
    class Tool
    {
        // revert nox title
        public static void RevertWindowTitle(IntPtr hwnd)
        {
            Win32.SetWindowText(hwnd, "NoxPlayer");
        }

        // Take screenshot of Nox
        public static Bitmap PrintWindow(IntPtr hwnd)
        {
            Win32.RECT rc;
            Win32.GetWindowRect(hwnd, out rc);

            Bitmap bmp = new Bitmap(rc.right - rc.left, rc.bottom - rc.top, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap = gfxBmp.GetHdc();

            Win32.PrintWindow(hwnd, hdcBitmap, 0);

            gfxBmp.ReleaseHdc(hdcBitmap);
            gfxBmp.Dispose();

            return bmp;
        }

        // compare pixel user input and pixel inside nox
        public static bool PixelSearch(int x, int y, int colorName, Bitmap ss)
        {
            int shade = 10;
            Color colorInput = Color.FromArgb(colorName);
            Color colorFromNox = ss.GetPixel(x, y);

            return Math.Abs(colorInput.R - colorFromNox.R) <= shade &&
                Math.Abs(colorInput.G - colorFromNox.G) <= shade &&
                Math.Abs(colorInput.B - colorFromNox.B) <= shade;
        }


        // send mouse click to nox
        public static void Mouse_Click(IntPtr hwnd, int x, int y)
        {
            Point point = new Point(x, y);
            int lParam = ((y << 16) | (x & 0xFFFF));

            Win32.SendMessage(Bot.hwnd, (int)Win32.WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)lParam);
            Win32.SendMessage(Bot.hwnd, (int)Win32.WM_LBUTTONUP, (IntPtr)0, (IntPtr)lParam);
        }

        // send log to textbox
        //private void Write_Log(string mess)
        //{
        //    textBox1.Invoke((Action)delegate
        //    {
        //        textBox1.AppendText(mess + "\n");
        //    });
        //}
    }
}
