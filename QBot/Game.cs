using System.Drawing;
using System.Threading;

namespace QMapleBot
{
    class Game
    {
        // init ss2
        private static Bitmap ss2 = null;

        // run quest
        public static void Do_Quest(Bitmap ss)
        {
            bool checkHp = Tool.PixelSearch(15, 63, 0xDD280A, ss);       // check hp
            // check auto btn before lv20
            bool checkAuto1 = Tool.PixelSearch(202, 590, 0x606161, ss);  // check mid of A in Auto (gray)
            bool checkAuto2 = Tool.PixelSearch(219, 603, 0x5c5d5c, ss);  // check bot of l in Battle (gray)
            bool checkAuto3 = Tool.PixelSearch(215, 603, 0x6f6f6f, ss);  // check bot of 2nd t in Battle (gray)

            bool checkAuto4 = Tool.PixelSearch(200, 591, 0xE1D195, ss); // check top of A in AUTO (yellow)
            bool checkAuto5 = Tool.PixelSearch(215, 591, 0xEADA9C, ss); // check top of T in AUTO (yellow)
            bool checkAuto6 = Tool.PixelSearch(222, 591, 0xAFA375, ss); // check top of O in AUTO (yellow)

            bool checkQuestTab = Tool.PixelSearch(23, 145, 0xF9492B, ss);
            if ((checkQuestTab && checkHp && checkAuto1 && checkAuto2 && checkAuto3) ||
                (checkQuestTab && checkHp && checkAuto4 && checkAuto5 && checkAuto6))
            {
                Tool.Mouse_Click(Bot.hwnd, 13, 151);  // click open quest tab
                Thread.Sleep(50);
            }


            if ((checkHp && checkAuto1 && checkAuto2 && checkAuto3) ||
               (checkHp && checkAuto4 && checkAuto5 && checkAuto6))
            {
                Thread.Sleep(500);
                Tool.Mouse_Click(Bot.hwnd, 13, 256);   // click 2nd quest
                Thread.Sleep(500);
                //Write_Log("Doing 2nd quest");
            }
            // check if 2nd quest is clicked, if not click 1st quest
            ss2 = Tool.PrintWindow(Bot.hwnd);
            // check auto btn before lv20
            checkHp = Tool.PixelSearch(15, 63, 0xDD280A, ss2);   // check hp
            checkAuto1 = Tool.PixelSearch(202, 590, 0x606161, ss2);  // check mid of A in Auto (gray)
            checkAuto2 = Tool.PixelSearch(219, 603, 0x5c5d5c, ss2);  // check bot of l in Battle (gray)
            checkAuto3 = Tool.PixelSearch(215, 603, 0x6f6f6f, ss2);  // check bot of 2nd t in Battle (gray)

            checkAuto4 = Tool.PixelSearch(200, 591, 0xE1D195, ss2); // check top of A in AUTO (yellow)
            checkAuto5 = Tool.PixelSearch(215, 591, 0xEADA9C, ss2); // check top of T in AUTO (yellow)
            checkAuto6 = Tool.PixelSearch(222, 591, 0xAFA375, ss2); // check top of O in AUTO (yellow)
            // release resources after use
            ss2.Dispose();

            if ((checkHp && checkAuto1 && checkAuto2 && checkAuto3) ||
               (checkHp && checkAuto4 && checkAuto5 && checkAuto6))
            {
                Tool.Mouse_Click(Bot.hwnd, 11, 177);   // click 1st quest if no 2nd quest
                Thread.Sleep(500);
                //Write_Log("2nd quest not found");
                //Write_Log("Doing 1st quest");
            }
        }

        // run skip
        public static void Do_Skip(Bitmap ss)
        {
            // mini skip
            bool checkSkip1 = Tool.PixelSearch(42, 60, 0xFFFFFF, ss);    // check S in Skip>> (white)
            bool checkSkip2 = Tool.PixelSearch(88, 64, 0xFFFFFF, ss);    // check 2nd > in Skip>> (white)
            bool checkSkip3 = Tool.PixelSearch(51, 65, 0xF8F8F5, ss);    // check k in Skip>> (white)
            //bool checkSkip3 = Tool.PixelSearch(766, 64, 0xFFFFFF, ss);   // check x
            //bool checkSkip4 = Tool.PixelSearch(18, 64, 0x6F1405, ss);    // check hp when darker
            if (checkSkip1 && checkSkip2 && checkSkip3)
            {
                Tool.Mouse_Click(Bot.hwnd, 56, 66);    // click skip
                Thread.Sleep(50);
                //Write_Log("Skip");
            }

            // big skip
            bool checkSkipBig1 = Tool.PixelSearch(751, 99, 0xFFFFFF, ss);    // check S in SKIP (white)
            bool checkSkipBig2 = Tool.PixelSearch(763, 109, 0xFEFEFE, ss);    // check K  in Skip (white)
            bool checkSkipBig3 = Tool.PixelSearch(776, 107, 0xFFFFFF, ss);   // check I
            bool checkSkipBig4 = Tool.PixelSearch(18, 64, 0xDD280A, ss);    // check hp when darker
            if (checkSkipBig1 && checkSkipBig2 && checkSkipBig3 && checkSkipBig4)
            {
                Tool.Mouse_Click(Bot.hwnd, 770, 100);    // click skip
                Thread.Sleep(50);
                //Write_Log("Skip");
            }
        }

        // run confirm tut
        public static void Do_Confirm(Bitmap ss)
        {
            bool checkConfirmTut1 = Tool.PixelSearch(656, 576, 0xFF7B50, ss);
            bool checkConfirmTut2 = Tool.PixelSearch(713, 575, 0xFF7B50, ss);
            if (checkConfirmTut1 && checkConfirmTut2)
            {
                Tool.Mouse_Click(Bot.hwnd, 710, 587);  // click accept
                Thread.Sleep(50);
                //Write_Log("Confirm");
            }

            bool checkConfirmDead1 = Tool.PixelSearch(350, 431, 0xFF7B50, ss);
            bool checkConfirmDead2 = Tool.PixelSearch(459, 441, 0xFF7B50, ss);
            if (checkConfirmDead1 && checkConfirmDead2)
            {
                Tool.Mouse_Click(Bot.hwnd, 402, 449);
                Thread.Sleep(50);
                //Write_Log("Confirm quest dead");
            }
        }

        // run claim
        public static void Do_Claim(Bitmap ss)
        {
            bool checkClaim1 = Tool.PixelSearch(333, 503, 0xFF7B50, ss);
            bool checkClaim2 = Tool.PixelSearch(463, 503, 0xFF7B50, ss);
            if (checkClaim1 && checkClaim2)
            {
                Tool.Mouse_Click(Bot.hwnd, 399, 505);
                Thread.Sleep(50);
                //Write_Log("Claim reward");
            }
        }

        // run auto equip
        public static void Do_Equip(Bitmap ss)
        {
            bool checkEquip1 = Tool.PixelSearch(705, 372, 0xFF7B50, ss);
            bool checkEquip2 = Tool.PixelSearch(650, 371, 0x548FBA, ss);
            bool checkEquip3 = Tool.PixelSearch(710, 373, 0xFFFEFE, ss);
            if (checkEquip1 && checkEquip2 && checkEquip3)
            {
                Tool.Mouse_Click(Bot.hwnd, 727, 378);
                Thread.Sleep(50);
                //Write_Log("Equip");
            }
        }

        // run available (2 quest appear)
        public static void Do_Available(Bitmap ss)
        {
            // complete quest (2 quest appear)
            bool checkComplete1 = Tool.PixelSearch(188, 514, 0x59B0A8, ss);
            bool checkComplete2 = Tool.PixelSearch(259, 520, 0x59B0A8, ss);
            if (checkComplete1 && checkComplete2)
            {
                Tool.Mouse_Click(Bot.hwnd, 222, 515);
                Thread.Sleep(50);
                //Write_Log("Confirm quest");
            }

            // click 1st available quest
            bool checkAvailable1 = Tool.PixelSearch(188, 514, 0xFF7B50, ss);
            bool checkAvailable2 = Tool.PixelSearch(259, 520, 0xFF7B50, ss);
            if (checkAvailable1 && checkAvailable2)
            {
                Tool.Mouse_Click(Bot.hwnd, 222, 515);
                Thread.Sleep(50);
                //Write_Log("Select quest");
            }
        }

        // run teleport
        public static void Do_Teleport(Bitmap ss)
        {
            // TODO: make this on/off
            bool checkTele1 = Tool.PixelSearch(466, 234, 0xFF7B50, ss);
            bool checkTele2 = Tool.PixelSearch(322, 236, 0x548FBA, ss);
            bool checkTele3 = Tool.PixelSearch(381, 238, 0x548FBA, ss);
            if (checkTele1 && checkTele2 && checkTele3)
            {
                Tool.Mouse_Click(Bot.hwnd, 448, 244);
                Thread.Sleep(50);
                //Write_Log("Use teleport");
            }
        }

        public static void Do_CheckTeleport(Bitmap ss)
        {
            // detect level use teleport base on skill equiped (DK)
            bool checkLevelTele1 = Tool.PixelSearch(583, 521, 0x9966FF, ss);
            bool checkLevelTele2 = Tool.PixelSearch(610, 521, 0x330066, ss);
            bool checkLevelTele4 = Tool.PixelSearch(583, 548, 0x9966FF, ss);
            bool checkLevelTele5 = Tool.PixelSearch(596, 535, 0xCC66FF, ss);
            if (checkLevelTele1 && checkLevelTele2 && checkLevelTele4 && checkLevelTele5)
            {
                Bot.checkTele = true;
                Thread.Sleep(50);
            }

            // detect level use teleport base on skill equiped (DW)
            bool checkLevelDW1 = Tool.PixelSearch(603, 520, 0xFF4400, ss);
            bool checkLevelDW2 = Tool.PixelSearch(593, 530, 0xCC1100, ss);
            bool checkLevelDW3 = Tool.PixelSearch(606, 549, 0x0099EE, ss);
            bool checkLevelDW4 = Tool.PixelSearch(609, 533, 0xFFEE00, ss);
            if (checkLevelDW1 && checkLevelDW2 && checkLevelDW3 && checkLevelDW4)
            {
                Bot.checkTele = true;
                Thread.Sleep(50);
            }
        }

        // run revive
        public static void Do_Revive(Bitmap ss)
        {
            bool checkRevive1 = Tool.PixelSearch(187, 420, 0x548FBA, ss);    // revive in town
            bool checkRevive2 = Tool.PixelSearch(309, 419, 0x548FBA, ss);    // revive in town
            bool checkRevive3 = Tool.PixelSearch(383, 420, 0x59B0A8, ss);
            if (checkRevive1 && checkRevive2 && checkRevive3)
            {
                Tool.Mouse_Click(Bot.hwnd, 249, 426);
                Thread.Sleep(50);
                //Write_Log("Revive");
            }
        }

        // run auto skill
        public static void Do_Skill(Bitmap ss)
        {
            // TODO: auto up skills
            bool checkSkill1 = Tool.PixelSearch(706, 372, 0xFF7B50, ss); // check Use btn
            bool checkSkill2 = Tool.PixelSearch(650, 372, 0x548FBA, ss); // check Close btn
            bool checkSkill3 = Tool.PixelSearch(610, 348, 0xF03030, ss); // check SP icon
            if (checkSkill1 && checkSkill2 && checkSkill3)
            {
                Tool.Mouse_Click(Bot.hwnd, 723, 373);
                Thread.Sleep(50);
                //Write_Log("Use Skill");
            }

            bool checkSkillEquip = Tool.PixelSearch(723, 227, 0x68717A, ss);
            bool checkSkillMain1 = Tool.PixelSearch(719, 253, 0xFF7B50, ss);    // not maxed skill 1
            bool checkSkillMain2 = Tool.PixelSearch(717, 255, 0xC2C2C2, ss);    // maxed skill 1

            bool checkSkillTab1 = Tool.PixelSearch(287, 131, 0x515F6E, ss);
            bool checkSkillTab2 = Tool.PixelSearch(675, 131, 0x515F6E, ss);
            bool checkSkillTab3 = Tool.PixelSearch(777, 129, 0xFFFFFF, ss);

            // check if skill equip
            if (!checkSkillEquip)
            {
                Tool.Mouse_Click(Bot.hwnd, 665, 261);  // click equip
                Thread.Sleep(50);
                Tool.Mouse_Click(Bot.hwnd, 177, 327);  // click equip
                Thread.Sleep(50);
            }
            // check if skill not max level
            if (checkSkillMain1)
            {
                //Write_Log("Skill up");
                Tool.Mouse_Click(Bot.hwnd, 737, 260);  // level up 1st skill
                Thread.Sleep(50);
                Tool.Mouse_Click(Bot.hwnd, 737, 260);  // level up 1st skill
                Thread.Sleep(50);
                Tool.Mouse_Click(Bot.hwnd, 737, 260);  // level up 1st skill
                Thread.Sleep(50);
                Tool.Mouse_Click(Bot.hwnd, 737, 260);  // level up 1st skill
                Thread.Sleep(50);
                Tool.Mouse_Click(Bot.hwnd, 737, 260);  // level up 1st skill
                Thread.Sleep(50);
            }

            // check if skill is maxed
            if (checkSkillMain2 && checkSkillTab1 && checkSkillTab2 && checkSkillTab3)
            {
                //Write_Log("Exit skill menu");
                Tool.Mouse_Click(Bot.hwnd, 777, 130);  // exit skill menu
                Thread.Sleep(500);
                Tool.Mouse_Click(Bot.hwnd, 77, 302);  // exit character menu
                Thread.Sleep(50);
            }
        }

        // close player info
        public static void Do_CloseUnwantedPopUp(Bitmap ss)
        {
            // close player info popup
            bool checkPlayer1 = Tool.PixelSearch(287, 131, 0x515F6E, ss);
            bool checkPlayer2 = Tool.PixelSearch(454, 131, 0x515F6E, ss);
            bool checkPlayer3 = Tool.PixelSearch(777, 129, 0xFFFFFF, ss);
            if (checkPlayer1 && checkPlayer2 && checkPlayer2)
            {
                Tool.Mouse_Click(Bot.hwnd, 777, 129);   // Close player info
                Thread.Sleep(50);
                //Write_Log("Close player info");
            }

            // close mail
            bool checkMail1 = Tool.PixelSearch(259, 140, 0x515F6E, ss);
            bool checkMail2 = Tool.PixelSearch(341, 195, 0xFF7B50, ss);
            bool checkMail3 = Tool.PixelSearch(648, 141, 0xFFFFFF, ss);
            if (checkMail1 && checkMail2 && checkMail3)
            {
                Tool.Mouse_Click(Bot.hwnd, 648, 141);   // close mail 
                Thread.Sleep(50);
                //Write_Log("Close player info");
            }
        }

        // check player level
        public static void Do_CheckLevel(Bitmap ss)
        {
            bool checkLevel1 = Tool.PixelSearch(29, 42, 0xE4C405, ss);      // top left of 7
            bool checkLevel2 = Tool.PixelSearch(32, 42, 0xEECA05, ss);      // top right of 7
            bool checkLevel3 = Tool.PixelSearch(30, 48, 0xF4D005, ss);    // near bot of 7

            // check 2nd number is 6 for cygnus
            bool checkSubLevel1 = Tool.PixelSearch(37, 42, 0xD4B405, ss);
            bool checkSubLevel2 = Tool.PixelSearch(34, 46, 0xFDD600, ss);
            bool checkSubLevel3 = Tool.PixelSearch(36, 49, 0xE1BE05, ss);
            bool checkSubLevel4 = Tool.PixelSearch(37, 45, 0xEBC708, ss);

            // check lv60 DW skill exists
            bool checkLevelDW1 = Tool.PixelSearch(603, 520, 0xFF4400, ss);
            bool checkLevelDW2 = Tool.PixelSearch(593, 530, 0xCC1100, ss);
            bool checkLevelDW3 = Tool.PixelSearch(606, 549, 0x0099EE, ss);
            bool checkLevelDW4 = Tool.PixelSearch(609, 533, 0xFFEE00, ss);

            // check lv 60 DK skill exists
            bool checkLevelDK1 = Tool.PixelSearch(583, 521, 0x9966FF, ss);
            bool checkLevelDK2 = Tool.PixelSearch(610, 521, 0x330066, ss);
            bool checkLevelDK3 = Tool.PixelSearch(583, 548, 0x9966FF, ss);
            bool checkLevelDK4 = Tool.PixelSearch(596, 535, 0xCC66FF, ss);

            // check auto run
            bool checkAutoRun = Tool.PixelSearch(323, 239, 0x548FBA, ss);    // cancel autorun btn            

            // do some click to switch char
            if ((checkLevel1 && checkLevel2 && checkLevel3 && checkLevelDK1 && checkLevelDK2 && checkLevelDK3) ||
                (checkLevel1 && checkLevel2 && checkLevel3 && checkSubLevel4 && checkSubLevel1 && checkSubLevel2 && checkSubLevel3 &&
                checkLevelDW1 && checkLevelDW2 && checkLevelDW3 && checkLevelDW4))
            {
                if (checkAutoRun)
                {
                    // press cancel autorun
                    Tool.Mouse_Click(Bot.hwnd, 350, 240);
                    Thread.Sleep(1000);
                }

                // press menu
                Tool.Mouse_Click(Bot.hwnd, 775, 51);
                Thread.Sleep(1000);

                // press options
                Tool.Mouse_Click(Bot.hwnd, 685, 584);
                Thread.Sleep(1000);

                // press select char
                Tool.Mouse_Click(Bot.hwnd, 407, 505);
                Thread.Sleep(1000);

                // start worker3 check level
                if (!Bot.worker3.IsBusy)
                {
                    Bot.worker3.RunWorkerAsync();
                }
                
            }
        }

        // check game alive 
        public static void Do_CheckAlive(Bitmap ss)
        {
            /*
             * the game crash and somehow nox's tutorial pop up,
             * we check the tutorial pixel to see if nox is crash.
             * This is for 1gb ram only.
             */
            bool checkGameDis1 = Tool.PixelSearch(12, 37, 0xF200E9, ss);
            bool checkGameDis2 = Tool.PixelSearch(309, 514, 0x37D38A, ss);
            bool checkGameDis3 = Tool.PixelSearch(394, 549, 0xF003F5, ss);
            if (checkGameDis1 && checkGameDis2 && checkGameDis3)
            {
                Tool.Mouse_Click(Bot.hwnd, 397, 52);   // close nox tutorial 
                Thread.Sleep(100);
            }

            bool checkGameDis4 = Tool.PixelSearch(12, 37, 0x54005D, ss);
            bool checkGameDis5 = Tool.PixelSearch(309, 514, 0x165437, ss);
            bool checkGameDis6 = Tool.PixelSearch(315, 341, 0xFFFFFF, ss);
            if (checkGameDis4 && checkGameDis5 && checkGameDis6)
            {
                Tool.Mouse_Click(Bot.hwnd, 401, 400);   // close nox tutorial 2
                Thread.Sleep(100);
            }

            bool checkGameDis7 = Tool.PixelSearch(270, 220, 0xF8B733, ss);
            bool checkGameDis8 = Tool.PixelSearch(129, 199, 0x53C4F7, ss);
            bool checkGameDis9 = Tool.PixelSearch(396, 320, 0xFFFFFF, ss);
            if (checkGameDis7 && checkGameDis8 && checkGameDis9)
            {
                Tool.Mouse_Click(Bot.hwnd, 531, 330);   // close nox tutorial 2
                Thread.Sleep(100);
            }

            bool checkGameDis10 = Tool.PixelSearch(34, 42, 0x7F7F7F, ss);
            bool checkGameDis11 = Tool.PixelSearch(775, 41, 0x7F7F7F, ss);
            bool checkGameDis12 = Tool.PixelSearch(776, 55, 0xFFFFFF, ss);
            if (checkGameDis10 && checkGameDis11 && checkGameDis12)
            {
                Tool.Mouse_Click(Bot.hwnd, 776, 55);   // close maple notice
                Thread.Sleep(100);
            }

            // maple start screen
            bool checkGameDis13 = Tool.PixelSearch(28, 168, 0xA1BA5F, ss);
            bool checkGameDis14 = Tool.PixelSearch(548, 228, 0xF5791F, ss);
            bool checkGameDis15 = Tool.PixelSearch(535, 338, 0xB68D53, ss);
            if (checkGameDis13 && checkGameDis14 && checkGameDis15)
            {
                Tool.Mouse_Click(Bot.hwnd, 390, 479);   // press start
                Thread.Sleep(100);
            }
        }
    }
}
