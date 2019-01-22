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
 *          [x] Auto Teleport when lv50+
 *      [x] Auto Switch Char when lv 70
 *      [ ] Auto Detect Stuck
 *
 * 
 */


namespace QMapleBot
{
    static class Bot
    {
        // nox clone name list
        private static List<string> nox_list = new List<string>();
        //private static List<string> memu_list = new List<string>();

        // init handle
        public static IntPtr hwnd;
        public static IntPtr thumb;
        //public static int emulator;
        private static bool checkHwnd = false;
        private static int pid;
        private static string nox_clonename = "";

        // init worker
        public static BackgroundWorker worker1 = null;
        public static BackgroundWorker worker2 = null;
        public static BackgroundWorker worker3 = null;
        //public static BackgroundWorker worker4 = null;
        //public static BackgroundWorker worker5 = null;

        // init ss
        public static Bitmap ss = null;
        private static Bitmap ss2 = null;

        // checkbox
        public static bool checkTele = false;
        //public static bool checkTut = false;

        // init label status (from Form1)
        public static Label status;

        // init label nox name (from Form1)
        public static Label name;

        // init label maple check alive
        public static Label check_alive;

        // pause worker
        //public static ManualResetEvent pause_bot = new ManualResetEvent(true);

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

            //worker4 = new BackgroundWorker
            //{
            //    WorkerSupportsCancellation = true
            //};

            //worker5 = new BackgroundWorker
            //{
            //    WorkerSupportsCancellation = true
            //};

            // add work
            worker1.DoWork += Worker_RunBot;
            worker2.DoWork += Worker_GetHandle;
            worker3.DoWork += Worker_SwitchChar;
            //worker4.DoWork += Worker_Relogin;
            //worker5.DoWork += Worker_CancelStuck;

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
            if (File.Exists(@"C:\Program Files (x86)\Nox\bin\Nox.exe"))
            {
                start_info.FileName = @"C:\Program Files (x86)\Nox\bin\Nox.exe";
            } else if (File.Exists(@"D:\Program Files\Nox\bin\Nox.exe"))
            {
                start_info.FileName = @"D:\Program Files\Nox\bin\Nox.exe";
            }

            start_info.Arguments = "-clone:" + emu_id + " -title:NoxPlayer -resolution:800x600 -dpi:160 -cpu:1 -memory:1024 -performance:low";
            Process.Start(start_info);
        }

        private static void Quit_Nox(string nox_id)
        {
            var quit_info = new ProcessStartInfo();
            if (File.Exists(@"C:\Program Files (x86)\Nox\bin\Nox.exe"))
            {
                quit_info.FileName = @"C:\Program Files (x86)\Nox\bin\Nox.exe";
            }
            else if (File.Exists(@"D:\Program Files\Nox\bin\Nox.exe"))
            {
                quit_info.FileName = @"D:\Program Files\Nox\bin\Nox.exe";
            }

            quit_info.Arguments = "-clone:" + nox_id + " -quit";
            Process.Start(quit_info);
        }

        private static void GetHandleNox()
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
                        

                        // change nox title
                        string nox_commandline = nox.GetCommandLine();
                        string nox_clone = nox_commandline.Split('-')[1];
                        nox_clonename = nox_clone.Split(':')[1].Trim();

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

                        // turn worker5 on
                        //worker5.RunWorkerAsync();
                        

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
                        string checknox_clonename = "Nox_" + i;
                        if (!nox_list.Contains(checknox_clonename))
                        {
                            Run_Emu(checknox_clonename);

                            // run worker4 for auto login
                            //worker4.RunWorkerAsync();

                            break;
                        }
                    }
                }
            }
            else
            {
                // run first nox
                Run_Emu("Nox_0");

                // run worker4 for auto login
                //worker4.RunWorkerAsync();
            }
        }

        // worker waiting to handle nox
        private static void Worker_GetHandle(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (!checkHwnd)
                {
                    GetHandleNox();
                }

                // check if no nox -> cancel all job
                if (checkHwnd)
                {
                    if (pid != 0 && !Process.GetProcessesByName("Nox").Any(x => x.Id == pid))
                    {
                        checkHwnd = false;
                        hwnd = IntPtr.Zero;
                        worker1.CancelAsync();
                        worker3.CancelAsync();
                        //worker4.CancelAsync();
                        //worker5.CancelAsync();

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
            // stop worker3
            if (worker3.IsBusy)
            {
                worker3.CancelAsync();
            }

            

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
                //pause_bot.WaitOne(Timeout.Infinite);

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

                    Game.Do_CheckLevel(ss);
                    //Game.Do_CheckAlive(ss);

                    Game.Do_EquipPotion(ss);

                    //Game.Do_DetectStuck(ss);

                    Event.Do_Event(ss);


                    Tutorial.Do_Tutorial(ss);
                    Tutorial.Tut_Pet(ss);
                    Tutorial.Tut_Treasure(ss);
                    Tutorial.Tut_Forge(ss);
                    Tutorial.Tut_Fever(ss);
                    Tutorial.Tut_Jewel(ss);
                    Tutorial.Tut_Dungeon(ss);
                    Tutorial.Tut_Auto(ss);



                    // release resource
                    Thread.Sleep(2000);
                    ss.Dispose();
                    Application.DoEvents();
                }
            }
        }

        // worker check level
        private static void Worker_SwitchChar(object sender, DoWorkEventArgs e)
        {
            // stop worker1
            if (worker1.IsBusy)
            {
                Bot.worker1.CancelAsync();
            }

            while (true)
            {
                if (worker3.CancellationPending)
                {
                    // cancel worker when press Stop
                    e.Cancel = true;
                    return;
                }

                ss2 = Tool.PrintWindow(hwnd);

                // close attendance pop up
                bool checkPopup1 = Tool.PixelSearch(17, 122, 0xFFFFFF, ss2);
                bool checkPopup2 = Tool.PixelSearch(497, 129, 0x515F6E, ss2);
                bool checkPopup3 = Tool.PixelSearch(711, 172, 0x548FBA, ss2);
                bool checkPopup4 = Tool.PixelSearch(779, 131, 0xFFFFFF, ss2);
                if (checkPopup1 && checkPopup2 && checkPopup3 && checkPopup4)
                {
                    Tool.MouseClick(779, 131);
                    Thread.Sleep(500);
                }

                // 1st char => 2nd char
                bool checkCurrentChar1a = Tool.PixelSearch(216, 296, 0xFFC812, ss2);
                bool checkCurrentChar1b = Tool.PixelSearch(126, 301, 0xffdb1d, ss2);
                bool checkNextChar1a = Tool.PixelSearch(245, 287, 0x8A5713, ss2);
                bool checkNextChar1b = Tool.PixelSearch(283, 316, 0x8A5713, ss2);
                if (checkCurrentChar1a && checkCurrentChar1b && checkNextChar1a && checkNextChar1b)
                {
                    Tool.MouseClick(294, 257); // switch 2nd char
                    Thread.Sleep(1000);
                    Tool.MouseClick(675, 488); // press start
                    Thread.Sleep(1000);
                }

                // 2nd char => 3rd char
                bool checkCurrentChar2a = Tool.PixelSearch(339, 294, 0xFFC812, ss2);
                bool checkCurrentChar2b = Tool.PixelSearch(254, 299, 0xffdb1d, ss2);
                bool checkNextChar2a = Tool.PixelSearch(368, 287, 0x8A5713, ss2);
                bool checkNextChar2b = Tool.PixelSearch(409, 316, 0x8A5713, ss2);
                if (checkCurrentChar2a && checkCurrentChar2b && checkNextChar2a && checkNextChar2b)
                {
                    Tool.MouseClick(418, 262); // switch 3rd char
                    Thread.Sleep(1000);
                    Tool.MouseClick(675, 488); // press start
                    Thread.Sleep(1000);
                }

                // 3rd char => 4th char
                bool checkCurrentChar3a = Tool.PixelSearch(461, 298, 0xFFC812, ss2);
                bool checkCurrentChar3b = Tool.PixelSearch(377, 300, 0xffdb1d, ss2);
                bool checkNextChar3a = Tool.PixelSearch(59, 468, 0x8A5713, ss2);
                bool checkNextChar3b = Tool.PixelSearch(92, 497, 0x8A5713, ss2);
                if (checkCurrentChar3a && checkCurrentChar3b && checkNextChar3a && checkNextChar3b)
                {
                    Tool.MouseClick(114, 435); // switch 4th char
                    Thread.Sleep(1000);
                    Tool.MouseClick(675, 488); // press start
                    Thread.Sleep(1000);
                }

                // 4th char => 5th char
                bool checkCurrentChar4a = Tool.PixelSearch(155, 478, 0xFFC812, ss2);
                bool checkCurrentChar4b = Tool.PixelSearch(65, 476, 0xffdb1d, ss2);
                bool checkNextChar4a = Tool.PixelSearch(187, 468, 0x8A5713, ss2);
                bool checkNextChar4b = Tool.PixelSearch(213, 497, 0x8A5713, ss2);
                if (checkCurrentChar4a && checkCurrentChar4b && checkNextChar4a && checkNextChar4b)
                {
                    Tool.MouseClick(235, 434); // switch 5th char
                    Thread.Sleep(1000);
                    Tool.MouseClick(675, 488); // press start
                    Thread.Sleep(1000);
                }

                // 5th char => 6th char
                bool checkCurrentChar5a = Tool.PixelSearch(278, 475, 0xFFC812, ss2);
                bool checkCurrentChar5b = Tool.PixelSearch(186, 479, 0xffdb1d, ss2);
                bool checkNextChar5a = Tool.PixelSearch(320, 486, 0x865412, ss2);
                bool checkNextChar5b = Tool.PixelSearch(320, 497, 0x8A5713, ss2);
                if (checkCurrentChar5a && checkCurrentChar5b && checkNextChar5a && checkNextChar5b)
                {
                    Tool.MouseClick(351, 454); // switch 6th char
                    Thread.Sleep(1000);
                    Tool.MouseClick(675, 488); // press start
                    Thread.Sleep(1000);
                }

                // close event
                Event.Do_Event(ss2);

                Game.Do_Skip(ss2);
                Game.Do_Confirm(ss2);

                // check if go into play screen
                bool checkHp = Tool.PixelSearch(15, 63, 0xDD280A, ss2);       // check hp
                bool checkMp = Tool.PixelSearch(15, 77, 0x0096FF, ss2);       // check mp

                bool checkRevive1 = Tool.PixelSearch(188, 429, 0x548FBA, ss2);
                bool checkRevive2 = Tool.PixelSearch(459, 419, 0x59B0A8, ss2);
                bool checkRevive3 = Tool.PixelSearch(537, 420, 0xFF7B50, ss2);
                if ((checkHp && checkMp) || (checkRevive1 && checkRevive2 && checkRevive3))
                {
                    ss2.Dispose();

                    // resume auto bot
                    Bot.worker1.RunWorkerAsync();
                    Bot.checkTele = false;
                }

                // release resource
                Thread.Sleep(1500);
                ss2.Dispose();
                Application.DoEvents();
            }
        }

        //#region relogin
        //// worker relogin
        //private static void Worker_Relogin(object sender, DoWorkEventArgs e)
        //{
        //    // stop worker1
        //    if (worker1.IsBusy)
        //    {
        //        Bot.worker1.CancelAsync();
        //    }

        //    // stop worker5
        //    if (worker5.IsBusy)
        //    {
        //        Bot.worker5.CancelAsync();
        //    }

        //    // run relogin
        //    while (true)
        //    {
        //        // allow worker to stop
        //        if (worker4.CancellationPending)
        //        {
        //            e.Cancel = true;
        //            return;
        //        }

        //        /*
        //        * the game crash and somehow nox's tutorial pop up,
        //        * we check the tutorial pixel to see if nox is crash.
        //        */
        //        ss2 = Tool.PrintWindow(hwnd);

        //        bool checkGameDis1 = Tool.PixelSearch(410, 163, 0xFFFFFF, ss2);
        //        bool checkGameDis2 = Tool.PixelSearch(309, 514, 0x37D38A, ss2);
        //        bool checkGameDis3 = Tool.PixelSearch(394, 549, 0xF003F5, ss2);
        //        if (checkGameDis1 && checkGameDis2 && checkGameDis3)
        //        {
        //            Tool.MouseClick(397, 52);   // close nox tutorial 
        //            Thread.Sleep(100);
        //        }

        //        bool checkGameDis4 = Tool.PixelSearch(12, 37, 0x54005D, ss2);
        //        bool checkGameDis5 = Tool.PixelSearch(309, 514, 0x165437, ss2);
        //        bool checkGameDis6 = Tool.PixelSearch(315, 341, 0xFFFFFF, ss2);
        //        if (checkGameDis4 && checkGameDis5 && checkGameDis6)
        //        {
        //            Tool.MouseClick(401, 400);   // close nox tutorial 2
        //            Thread.Sleep(100);
        //        }

        //        bool checkOpenMaple1 = Tool.PixelSearch(270, 220, 0xF8B733, ss2);
        //        bool checkOpenMaple2 = Tool.PixelSearch(129, 199, 0x53C4F7, ss2);
        //        bool checkOpenMaple3 = Tool.PixelSearch(396, 320, 0xFFFFFF, ss2);
        //        if (checkOpenMaple1 && checkOpenMaple2 && checkOpenMaple3)
        //        {
        //            Tool.MouseClick(531, 330);   // close click maple
        //            Thread.Sleep(100);
        //        }

        //        bool checkGameDis10 = Tool.PixelSearch(34, 42, 0x232C2C, ss2);
        //        bool checkGameDis11 = Tool.PixelSearch(778, 42, 0x111616, ss2);
        //        bool checkGameDis12 = Tool.PixelSearch(776, 55, 0xFFFFFF, ss2);
        //        if (checkGameDis10 && checkGameDis11 && checkGameDis12)
        //        {
        //            Tool.MouseClick(776, 55);   // close maple notice
        //            Thread.Sleep(100);
        //        }

        //        // maple start screen
        //        bool checkGameDis13 = Tool.PixelSearch(28, 168, 0xA1BA5F, ss2);
        //        bool checkGameDis14 = Tool.PixelSearch(548, 228, 0xF5791F, ss2);
        //        bool checkGameDis15 = Tool.PixelSearch(535, 338, 0xB68D53, ss2);
        //        if (checkGameDis13 && checkGameDis14 && checkGameDis15)
        //        {
        //            Tool.MouseClick(390, 479);   // press here to start
        //            Thread.Sleep(100);
        //        }

        //        // select current server to login
        //        bool checkGameDis16 = Tool.PixelSearch(208, 136, 0x515F6E, ss2);
        //        bool checkGameDis17 = Tool.PixelSearch(22, 524, 0xE8BB00, ss2);
        //        bool checkGameDis18 = Tool.PixelSearch(401, 188, 0xF2F2F2, ss2);
        //        if (checkGameDis16 && checkGameDis17 && checkGameDis18)
        //        {
        //            Tool.MouseClick(368, 246);   // press current server
        //            Thread.Sleep(100);
        //        }

        //        // close attendance pop up
        //        bool checkPopup1 = Tool.PixelSearch(17, 122, 0xFFFFFF, ss2);
        //        bool checkPopup2 = Tool.PixelSearch(497, 129, 0x515F6E, ss2);
        //        bool checkPopup3 = Tool.PixelSearch(711, 172, 0x548FBA, ss2);
        //        bool checkPopup4 = Tool.PixelSearch(779, 131, 0xFFFFFF, ss2);
        //        if (checkPopup1 && checkPopup2 && checkPopup3 && checkPopup4)
        //        {
        //            Tool.MouseClick(779, 131); // close pop up
        //            Thread.Sleep(100);
        //        }

        //        // press start for current character
        //        bool checkGameDis19 = Tool.PixelSearch(35, 329, 0xFF7B52, ss2);
        //        bool checkGameDis20 = Tool.PixelSearch(605, 482, 0x9BBC17, ss2);
        //        bool checkGameDis21 = Tool.PixelSearch(537, 329, 0xFF7B52, ss2);
        //        if (checkGameDis19 && checkGameDis20 && checkGameDis21)
        //        {
        //            Tool.MouseClick(654, 487);   // press here to start
        //            Thread.Sleep(100);
        //        }

        //        // close event
        //        Event.Do_Event(ss2);

        //        bool checkHp = Tool.PixelSearch(15, 63, 0xDD280A, ss2);       // check hp
        //        bool checkMp = Tool.PixelSearch(15, 77, 0x0096FF, ss2);       // check mp

        //        bool checkRevive1 = Tool.PixelSearch(188, 429, 0x548FBA, ss2);
        //        bool checkRevive2 = Tool.PixelSearch(459, 419, 0x59B0A8, ss2);
        //        bool checkRevive3 = Tool.PixelSearch(537, 420, 0xFF7B50, ss2);
        //        if ((checkHp && checkMp) || (checkRevive1 && checkRevive2 && checkRevive3))
        //        {
        //            ss2.Dispose();

        //            // start worker1, worker5
        //            worker1.RunWorkerAsync();
        //            worker5.RunWorkerAsync();
        //        }


        //        // release resource
        //        Thread.Sleep(1500);
        //        ss2.Dispose();
        //        Application.DoEvents();
        //    }
        //}
        //#endregion

        //// worker cancel stuck
        //private static void Worker_CancelStuck(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        if (worker5.CancellationPending)
        //        {
        //            // cancel worker when press Stop
        //            e.Cancel = true;
        //            return;
        //        }

        //        Thread.Sleep(60000);
        //        Tool.MouseClick(22, 214);
        //    }
        //}
    }
}
