using System.Drawing;
using System.Threading;

namespace QMapleBot
{
    class Quest
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
            bool checkSkip3 = Tool.PixelSearch(766, 64, 0xFFFFFF, ss);   // check x
            bool checkSkip4 = Tool.PixelSearch(18, 64, 0x6F1405, ss);    // check hp when darker
            if (checkSkip1 && checkSkip2 && checkSkip3 && checkSkip4)
            {
                Tool.Mouse_Click(Bot.hwnd, 56, 66);    // click skip
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
    }
}
