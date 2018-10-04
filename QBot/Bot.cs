using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Management;
using System.Collections.Generic;

/*
 *  Function:
 *  
 *      [x] Auto Quest
 *      [x] Auto Tutorial
 *      [x] Auto Skill up
 *      [x] Auto Teleport
 *          [x] Auto Teleport when lv60+
 *      [ ] Auto Switch Char when lv 70
 *          [ ] Auto Switch Cygnus when lv76
 *      [ ] Auto Re-Login when crash game
 *
 * 
 */


namespace QMapleBot
{
    static class Bot
    {
        // nox clone name list
        private static List<string> nox_list = new List<string>();
        private static List<string> memu_list = new List<string>();

        // init handle
        public static IntPtr hwnd;
        public static int emulator;
        private static bool checkHwnd = false;

        // init worker
        public static BackgroundWorker worker1 = null;
        public static BackgroundWorker worker2 = null;
        public static BackgroundWorker worker3 = null;

        // init ss
        public static Bitmap ss = null;
        //private static Bitmap ss2 = null;

        // checkbox
        public static bool checkTele = false;

        // init label status (from Form1)
        public static Label status;

        // init label nox name (from Form1)
        public static Label name;

        // init label maple check alive
        public static Label check_alive;

        // pause worker
        public static ManualResetEvent pause_bot = new ManualResetEvent(true);

        public static void Init_Worker()
        {
            // create object worker
            worker1 = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            worker2 = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            worker3 = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            // add work
            worker1.DoWork += Worker_RunBot;
            worker2.DoWork += Worker_GetHandle;
            worker3.DoWork += Worker_SwitchChar;

            // get handle and set nox title
            worker2.RunWorkerAsync();
        }

        private static string GetCommandLine(this Process process)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
            using (ManagementObjectCollection objects = searcher.Get())
            {
                return objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
            }
        }

        private static void Run_Emu(string emu_id)
        {
            var start_info = new ProcessStartInfo();
            switch (emulator)
            {
                case 1:
                    start_info.FileName = @"C:\Program Files (x86)\Nox\bin\Nox.exe";
                    start_info.Arguments = "-clone:" + emu_id + " -title:NoxPlayer -resolution:800x600 -dpi:160 -cpu:1 -memory:1500 -performance:middle";
                    Process.Start(start_info);
                    break;
                case 2:
                    start_info.FileName = @"C:\Program Files\Microvirt\MEmu\MEmu.exe";
                    start_info.Arguments = emu_id;
                    Process.Start(start_info);
                    break;
            }
        }

        private static void GetHandleNox(int pid)
        {
            Process[] nox_running = Process.GetProcessesByName("nox");
            // get handle if there is nox running
            if (nox_running.Length >= 1)
            {
                bool check_nox = false;

                foreach (Process nox in nox_running)
                {
                    if (nox.MainWindowTitle.Equals("NoxPlayer"))
                    {
                        // handle nox
                        hwnd = nox.MainWindowHandle;
                        emulator = 1;

                        // change nox title
                        string nox_commandline = nox.GetCommandLine();
                        string nox_clone = nox_commandline.Split('-')[1];
                        string nox_clonename = nox_clone.Split(':')[1];

                        Win32.SetWindowText(hwnd, nox_clonename);


                        pid = nox.Id;
                        checkHwnd = true;

                        // show name of nox
                        name.Invoke((Action)delegate
                        {
                            name.Text = nox_clonename;
                        });

                        // change color of nox status
                        status.Invoke((Action)delegate
                        {
                            status.Text = "On";
                            status.ForeColor = Color.Lime;
                        });

                        // save status nox exist
                        check_nox = true;


                        break;
                    }
                    else
                    {
                        // add nox to list if its name already exist
                        nox_list.Add(nox.MainWindowTitle.Trim());
                    }
                }

                // run nox which does not exist in nox_list if there is no nox to handle
                if (!check_nox)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        string nox_clonename = "Nox_" + i;
                        if (!nox_list.Contains(nox_clonename))
                        {
                            Run_Emu(nox_clonename);
                            break;
                        }
                    }
                }
            }
            else
            {
                // run first nox
                Run_Emu("Nox_0");
            }
        }

        private static void GetHandleMemu(int pid)
        {
            Process[] memu_running = Process.GetProcessesByName("memu");
            // get handle if there is nox running
            if (memu_running.Length >= 1)
            {
                bool check_memu = false;

                foreach (Process memu in memu_running)
                {
                    if (memu.MainWindowTitle.Equals("(MemuPlayer)"))
                    {
                        // handle itools
                        hwnd = memu.MainWindowHandle;
                        emulator = 2;

                        // change itools title
                        string memu_commandline = memu.GetCommandLine();
                        string memu_clonename = memu_commandline.Split(' ')[2];

                        Win32.SetWindowText(hwnd, memu_clonename);


                        pid = memu.Id;
                        checkHwnd = true;

                        // show name of nox
                        name.Invoke((Action)delegate
                        {
                            name.Text = memu_clonename;
                        });

                        // change color of nox status
                        status.Invoke((Action)delegate
                        {
                            status.Text = "On";
                            status.ForeColor = Color.Lime;
                        });

                        // save status nox exist
                        check_memu = true;


                        break;
                    }
                    else
                    {
                        // add nox to list if its name already exist
                        memu_list.Add(memu.MainWindowTitle.Trim());
                    }
                }

                // run nox which does not exist in nox_list if there is no nox to handle
                if (!check_memu)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        string itools_clonename = "";
                        if (i == 0)
                        {
                            itools_clonename = "MEmu";
                        }
                        else
                        {
                            itools_clonename = "MEmu_" + i;
                        }
                        
                        if (!memu_list.Contains(itools_clonename))
                        {
                            Run_Emu(itools_clonename);
                            break;
                        }
                    }
                }
            }
            else
            {
                // run first nox
                Run_Emu("MEmu");
            }
        }


        // worker waiting to handle nox
        private static void Worker_GetHandle(object sender, DoWorkEventArgs e)
        {
            int pid = 0;

            // detect emu to run
            if (File.Exists(@"C:\Program Files (x86)\Nox\bin\Nox.exe"))
            {
                emulator = 1;
            }
            else if (File.Exists(@"C:\Program Files\Microvirt\MEmu\MEmu.exe"))
            {
                emulator = 2;
            }



            while (true)
            {
                // get handle first time
                if (!checkHwnd)
                {
                    switch (emulator)
                    {
                        case 1:
                            GetHandleNox(pid);
                            break;
                        case 2:
                            GetHandleMemu(pid);
                            break;
                    }
                }

                // check if no nox -> cancel all job
                if (checkHwnd)
                {
                    if ((pid != 0 && !Process.GetProcessesByName("Nox").Any(x => x.Id == pid)) ||
                        (pid != 0 && !Process.GetProcessesByName("Memu").Any(x => x.Id == pid)))
                    {
                        checkHwnd = false;
                        hwnd = IntPtr.Zero;
                        worker1.CancelAsync();

                        // change color of nox status
                        status.Invoke((Action)delegate
                        {
                            status.Text = "Off";
                            status.ForeColor = Color.Crimson;
                        });
                    }
                }

                // give the bot some breathes
                Application.DoEvents();
                Thread.Sleep(2000);
            }
        }

        // worker run bot
        private static void Worker_RunBot(object sender, DoWorkEventArgs e)
        {
            // bot main functions
            while (true)
            {
                if (worker1.CancellationPending)
                {
                    // cancel worker when press Stop
                    e.Cancel = true;
                    return;
                }

                // allow bot to be paused
                pause_bot.WaitOne(Timeout.Infinite);

                // capture screenshot every time the bot finish a round of loop
                if (hwnd != IntPtr.Zero)
                {
                    ss = Tool.PrintWindow(hwnd);

                    Game.Do_Quest(ss);
                    Game.Do_Skip(ss);
                    Game.Do_Confirm(ss);
                    Game.Do_Claim(ss);
                    Game.Do_Equip(ss);
                    Game.Do_Available(ss);

                    if (checkTele)
                    {
                        Game.Do_Teleport(ss);
                    }
                    else
                    {
                        Game.Do_CheckTeleport(ss);
                    }

                    Game.Do_Revive(ss);
                    Game.Do_Skill(ss);
                    Game.Do_CloseUnwantedPopUp(ss);

                    Event.Do_Event(ss);

                    Tutorial.Do_Tutorial(ss);
                    Tutorial.Tut_Pet(ss);
                    Tutorial.Tut_Treasure(ss);
                    Tutorial.Tut_Forge(ss);
                    Tutorial.Tut_Fever(ss);
                    Tutorial.Tut_Jewel(ss);
                    Tutorial.Tut_Dungeon(ss);
                    Tutorial.Tut_Auto(ss);
                    Game.Do_CheckLevel(ss);


                    Thread.Sleep(1000);

                    // release resource
                    ss.Dispose();
                    Application.DoEvents();
                }
            }
        }

        // worker check level
        private static void Worker_SwitchChar(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (worker3.CancellationPending)
                {
                    // cancel worker when press Stop
                    e.Cancel = true;
                    return;
                }

                Thread.Sleep(3000);

                ss = Tool.PrintWindow(hwnd);

                // 1st char => 2nd char
                bool checkCurrentChar1a = Tool.PixelSearch(218, 290, 0xFFC812, ss);
                bool checkCurrentChar1b = Tool.PixelSearch(225, 292, 0xA3C54B, ss);
                bool checkNextChar1a = Tool.PixelSearch(265, 316, 0x8A5713, ss);
                bool checkNextChar1b = Tool.PixelSearch(320, 316, 0x8A5713, ss);
                if (checkCurrentChar1a && checkCurrentChar1b && checkNextChar1a && checkNextChar1b)
                {
                    Tool.Mouse_Click(294, 257); // switch 2nd char
                    Thread.Sleep(1000);
                    Tool.Mouse_Click(675, 488); // press start
                    Thread.Sleep(1000);

                    // resume auto bot
                    pause_bot.Set();
                    Bot.checkTele = false;

                    // stop this thread
                    worker3.CancelAsync();
                }

                // 2nd char => 3rd char
                bool checkCurrentChar2a = Tool.PixelSearch(342, 290, 0xFFC812, ss);
                bool checkCurrentChar2b = Tool.PixelSearch(350, 292, 0xA3C54B, ss);
                bool checkNextChar2a = Tool.PixelSearch(391, 316, 0x8A5713, ss);
                bool checkNextChar2b = Tool.PixelSearch(448, 316, 0x8A5713, ss);
                if (checkCurrentChar2a && checkCurrentChar2b && checkNextChar2a && checkNextChar2b)
                {
                    Tool.Mouse_Click(418, 262); // switch 3rd char
                    Thread.Sleep(1000);
                    Tool.Mouse_Click(675, 488); // press start
                    Thread.Sleep(1000);

                    // resume auto bot
                    pause_bot.Set();
                    Bot.checkTele = false;

                    // stop this thread
                    worker3.CancelAsync();
                }

                // 3rd char => 4th char
                bool checkCurrentChar3a = Tool.PixelSearch(446, 290, 0xFFC812, ss);
                bool checkCurrentChar3b = Tool.PixelSearch(475, 292, 0xA3C54B, ss);
                bool checkNextChar3a = Tool.PixelSearch(78, 497, 0x8A5713, ss);
                bool checkNextChar3b = Tool.PixelSearch(134, 497, 0x8A5713, ss);
                if (checkCurrentChar3a && checkCurrentChar3b && checkNextChar3a && checkNextChar3b)
                {
                    Tool.Mouse_Click(114, 435); // switch 4th char
                    Thread.Sleep(1000);
                    Tool.Mouse_Click(675, 488); // press start
                    Thread.Sleep(1000);

                    // resume auto bot
                    pause_bot.Set();
                    Bot.checkTele = false;

                    // stop this thread
                    worker3.CancelAsync();
                }

                // 4th char => 5th char
                bool checkCurrentChar4a = Tool.PixelSearch(150, 474, 0xFFC812, ss);
                bool checkCurrentChar4b = Tool.PixelSearch(162, 473, 0xA7C84D, ss);
                bool checkNextChar4a = Tool.PixelSearch(204, 497, 0x8A5713, ss);
                bool checkNextChar4b = Tool.PixelSearch(260, 497, 0x8A5713, ss);
                if (checkCurrentChar4a && checkCurrentChar4b && checkNextChar4a && checkNextChar4b)
                {
                    Tool.Mouse_Click(235, 434); // switch 5th char
                    Thread.Sleep(1000);
                    Tool.Mouse_Click(675, 488); // press start
                    Thread.Sleep(1000);

                    // resume auto bot
                    pause_bot.Set();
                    Bot.checkTele = false;

                    // stop this thread
                    worker3.CancelAsync();
                }

                // release resource
                ss.Dispose();
                Application.DoEvents();
            }
            
        }
    }
}
