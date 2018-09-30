﻿using System;
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
 *          [ ] Auto Switch Cygnus when lv78
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

        // init handle
        public static IntPtr hwnd;
        private static bool checkHwnd = false;

        // init worker
        public static BackgroundWorker worker1 = null;
        public static BackgroundWorker worker2 = null;
        //public BackgroundWorker worker3 = null;

        // init ss
        private static Bitmap ss = null;
        //private static Bitmap ss2 = null;

        // checkbox
        public static bool checkTele = false;

        // init label status (from Form1)
        public static Label status;

        // init label nox name (from Form1)
        public static Label name;

        // init label maple check alive
        public static Label check_alive;

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

            //worker3 = new BackgroundWorker
            //{
            //    WorkerSupportsCancellation = true
            //};

            // add work
            worker1.DoWork += Worker_RunBot;
            worker2.DoWork += Worker_GetHandle;
            //worker3.DoWork += Worker_CheckLevel;

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

        // worker waiting to handle nox
        private static void Worker_GetHandle(object sender, DoWorkEventArgs e)
        {
            int noxPid = 0;

            while (true)
            {
                // get handle first time
                if (!checkHwnd)
                {
                    Process[] nox_running = Process.GetProcessesByName("Nox");

                    // get handle if there is nox running
                    if (nox_running.Length >= 1)
                    {
                        bool check_nox = false;

                        foreach (Process nox in nox_running)
                        {
                            if (nox.MainWindowTitle.Equals("NoxPlayer"))
                            {
                                hwnd = nox.MainWindowHandle;
                                // change nox title
                                string nox_commandline = nox.GetCommandLine();
                                string nox_clone = nox_commandline.Split('-')[1];
                                string nox_clonename = nox_clone.Split(':')[1];

                                Win32.SetWindowText(hwnd, nox_clonename);


                                noxPid = nox.Id;
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

                                // mark nox exist
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
                                    Run_Nox(nox_clonename);
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        // run first nox
                        Run_Nox("Nox_0");
                    }
                    
                }

                // check if no nox -> cancel all job
                if (checkHwnd)
                {
                    if (noxPid != 0 && !Process.GetProcessesByName("nox").Any(x => x.Id == noxPid))
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
                    } else
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



                    Thread.Sleep(1000);

                    // release resource
                    ss.Dispose();
                    Application.DoEvents();
                }
            }
        }

        private static void Run_Nox(string nox_id)
        {
            var start_info = new ProcessStartInfo();
            start_info.FileName = @"C:\Program Files (x86)\Nox\bin\Nox.exe";
            start_info.Arguments = "-clone:" + nox_id + " -title:NoxPlayer -resolution:800x600 -dpi:160 -cpu:1 -memory:1500 -performance:middle";
            Process.Start(start_info);
        }

        // worker check level
        //private void Worker_CheckLevel(object sender, DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        if (worker3.CancellationPending)
        //        {
        //            // cancel worker when press Stop
        //            e.Cancel = true;
        //            return;
        //        }

        //        ss2 = Tool.PrintWindow(hwnd);
        //        // detect level use teleport base on skill equiped (DK)
        //        bool checkLevelTele1 = Tool.PixelSearch(583, 521, 0x9966FF, ss2);
        //        bool checkLevelTele2 = Tool.PixelSearch(610, 521, 0x330066, ss2);
        //        bool checkLevelTele4 = Tool.PixelSearch(583, 548, 0x9966FF, ss2);
        //        bool checkLevelTele5 = Tool.PixelSearch(596, 535, 0xCC66FF, ss2);
        //        if (checkLevelTele1 && checkLevelTele2 && checkLevelTele4 && checkLevelTele5)
        //        {
        //            checkTele = true;

        //            // stop bot when tele is on
        //            return;
        //        }


        //        // release resource
        //        ss2.Dispose();

        //        // give the bot some breathes
        //        Application.DoEvents();
        //        Thread.Sleep(30000);
        //    }
        //}
    }
}