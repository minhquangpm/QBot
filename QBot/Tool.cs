using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Mail;

namespace QMapleBot
{
    class Tool
    {
        // revert nox title
        public static void RevertWindowTitle()
        {
            Win32.SetWindowText(Bot.hwnd, "NoxPlayer");
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

            // detect pixel 
            Color colorFromEmulator = ss.GetPixel(x, y);

            return Math.Abs(colorInput.R - colorFromEmulator.R) <= shade &&
                Math.Abs(colorInput.G - colorFromEmulator.G) <= shade &&
                Math.Abs(colorInput.B - colorFromEmulator.B) <= shade;
        }


        // send mouse click to nox
        public static void Mouse_Click(int x, int y)
        {
            // 
            int lParam = (y << 16) | (x & 0xFFFF);

            Win32.SendMessage(Bot.hwnd, (int)Win32.WM_LBUTTONDOWN, (IntPtr)1, (IntPtr)lParam);
            Win32.SendMessage(Bot.hwnd, (int)Win32.WM_LBUTTONUP, (IntPtr)0, (IntPtr)lParam);
        }

        public static void SendGmail(string nox_id, string error_msg)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("lolbadass6@gmail.com");
            msg.To.Add("minhquangpm01@gmail.com");
            msg.To.Add("quangvietvm@gmail.com");
            msg.Subject = nox_id + " ERROR: " + DateTime.Now.ToString();
            msg.Body = error_msg;
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("lolbadass6@gmail.com", "botngu1234");
            client.Timeout = 20000;
            try
            {
                client.Send(msg);
                //Console.Write("Mail has been successfully sent!");
            }
            catch (Exception ex)
            {
                //Console.Write("Fail Has error" + ex.Message);
            }
            finally
            {
                msg.Dispose();
            }
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
