﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace QMapleBot
{
    public partial class Form1 : Form
    {
        // init bot
        //Bot bot = new Bot();

        public Form1()
        {
            InitializeComponent();

            // init worker
            Bot.Init_Worker();
            Bot.status = this.label3;
            Bot.name = this.label5;
            Bot.check_alive = this.label6;
        }

        // start btn
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Bot.worker1.IsBusy)
            {
                // start worker1, worker5
                Bot.worker1.RunWorkerAsync();
                //Bot.worker5.RunWorkerAsync();

                // enable btn
                button1.Enabled = false;
                button2.Enabled = true;

                // change color of bot status
                label4.Text = "On";
                label4.ForeColor = Color.Lime;
            }

            UpdateThumb();
        }

        // stop btn
        private void button2_Click(object sender, EventArgs e)
        {
            // stop worker1
            Bot.worker1.CancelAsync();

            // stop worker3
            Bot.worker3.CancelAsync();

            // stop worker5
            //Bot.worker5.CancelAsync();

            // stop tele
            Bot.checkTele = false;
            checkBox2.Checked = false;
            

            // change color of bot status
            label4.Text = "Off";
            label4.ForeColor = Color.Crimson;

            button2.Enabled = false;
            button1.Enabled = true;
        }

        private void form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Tool.RevertWindowTitle();
        }

        // hide window (just move window outside screen)
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Win32.RECT rc;
            Win32.GetWindowRect(Bot.hwnd, out rc);

            if (checkBox1.Checked)
            {
                Win32.MoveWindow(Bot.hwnd, 0, -2000, rc.right - rc.left, rc.bottom - rc.top, true);

            } else
            {
                Win32.MoveWindow(Bot.hwnd, 10, 10, rc.right - rc.left, rc.bottom - rc.top, true);
                Win32.SetWindowPos(Bot.hwnd, Win32.HWND_TOP, 0, 0, 0, 0, Win32.SWP_NOSIZE);
            }
        }

        // checkbox for teleport
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                Bot.checkTele = true;
            } else
            {
                Bot.checkTele = false;
            }
        }

        private void UpdateThumb()
        {
            int i = Win32.DwmRegisterThumbnail(this.Handle, Bot.hwnd, out Bot.thumb);

            Win32.DWM_THUMBNAIL_PROPERTIES props = new Win32.DWM_THUMBNAIL_PROPERTIES();

            props.fVisible = true;
            props.dwFlags = Win32.DWM_TNP_VISIBLE | Win32.DWM_TNP_RECTDESTINATION | Win32.DWM_TNP_OPACITY;
            props.opacity = 255;
            props.rcDestination = new Win32.RECT(pnlPreview.Left, pnlPreview.Top, pnlPreview.Right, pnlPreview.Bottom);

            Win32.DwmUpdateThumbnailProperties(Bot.thumb, ref props);
        }
    }
}
