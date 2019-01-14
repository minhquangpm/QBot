using System.Drawing;
using System.Threading;

namespace QMapleBot
{
    class Tutorial
    {
        // run tutorial
        public static void Do_Tutorial(Bitmap ss)
        {
            // TODO: auto all tutorial
            bool checkTutor1 = Tool.PixelSearch(25, 168, 0xF9F9E8, ss);
            bool checkTutor2 = Tool.PixelSearch(21, 216, 0xF9F7F3, ss);
            bool checkTutor3 = Tool.PixelSearch(143, 173, 0xF8F5EA, ss);
            if (checkTutor1 && checkTutor2 && checkTutor3)
            {
                Tool.MouseClick(36, 192);   // Click tutorials
                Thread.Sleep(50);
                //Write_Log("Auto Tutorial");
            }
        }

        // run tutorial pet
        public static void Tut_Pet(Bitmap ss)
        {
            // character icon 
            bool checkCharIcon1 = Tool.PixelSearch(118, 91, 0xFD6B39, ss);  // arrow > char icon
            //bool checkCharIcon2 = Tool.PixelSearch(115, 54, 0xFFFFFF, ss);
            if (checkCharIcon1)
            {
                Tool.MouseClick(116, 59);   // click character icon
                Thread.Sleep(50);
            }

            // pet icon
            bool checkPetIcon1 = Tool.PixelSearch(293, 167, 0xFE6F32, ss);  // arrow > pet icon
            //bool checkPetIcon2 = Tool.PixelSearch(313, 130, 0xCECEBE, ss);
            if (checkPetIcon1)
            {
                Tool.MouseClick(293, 135);   // click pet icon
                Thread.Sleep(50);
            }

            // choose pet
            bool checkChoosePet1 = Tool.PixelSearch(458, 233, 0xFBFBF9, ss);
            bool checkChoosePet2 = Tool.PixelSearch(528, 250, 0xFBFBF9, ss);
            bool checkChoosePet3 = Tool.PixelSearch(552, 211, 0x548FBA, ss);
            if (checkChoosePet1 && checkChoosePet2 && !checkChoosePet3)
            {
                Tool.MouseClick(488, 240);   // click choose pet
                Thread.Sleep(50);
            }

            // confirm pet
            bool checkConfirmPet1 = Tool.PixelSearch(687, 502, 0xFBFBF9, ss);
            bool checkConfirmPet2 = Tool.PixelSearch(781, 502, 0xFBFBF9, ss);
            bool checkConfirmPet3 = Tool.PixelSearch(705, 502, 0xFF7B50, ss);
            if (checkConfirmPet1 && checkConfirmPet2 && checkConfirmPet3)
            {
                Tool.MouseClick(739, 506);   // click choose pet
                Thread.Sleep(50);
            }
        }

        // run tutorial treasure
        public static void Tut_Treasure(Bitmap ss)
        {
            // find treasure icon
            bool checkTreasureIcon1 = Tool.PixelSearch(659, 91, 0xFF7B41, ss);
            bool checkTreasureIcon2 = Tool.PixelSearch(681, 55, 0xFFFFFE, ss);
            if (checkTreasureIcon1 && checkTreasureIcon2)
            {
                Tool.MouseClick(655, 51);   // click treasure icon
                Thread.Sleep(50);
            }

            // treasure tab
            bool checkTreasureTab1 = Tool.PixelSearch(25, 379, 0xCBCBCB, ss);
            bool checkTreasureTab2 = Tool.PixelSearch(48, 396, 0x4E4E4E, ss);
            bool checkTreasureTab3 = Tool.PixelSearch(41, 444, 0xCBCBCB, ss);
            if (checkTreasureTab1 && checkTreasureTab2 && checkTreasureTab3)
            {
                Tool.MouseClick(36, 413);   // click treasure tab
                Thread.Sleep(50);
            }

            // treasure free use
            bool checkTreasureFree1 = Tool.PixelSearch(615, 491, 0xFF7B50, ss);
            bool checkTreasureFree2 = Tool.PixelSearch(759, 488, 0xFF7B50, ss);
            bool checkTreasureFree3 = Tool.PixelSearch(625, 520, 0xE45F36, ss);
            if (checkTreasureFree1 && checkTreasureFree2 && checkTreasureFree3)
            {
                Tool.MouseClick(685, 500);   // click treasure free
                Thread.Sleep(50);
            }

            // treasure confirm
            bool checkTreasureConfirm1 = Tool.PixelSearch(335, 535, 0xBCBCB9, ss);
            bool checkTreasureConfirm2 = Tool.PixelSearch(468, 535, 0xBCB7B2, ss);
            bool checkTreasureConfirm3 = Tool.PixelSearch(443, 520, 0xFF7B50, ss);
            if (checkTreasureConfirm1 && checkTreasureConfirm2 && checkTreasureConfirm3)
            {
                Tool.MouseClick(390, 508);   // click treasure confirm
                Thread.Sleep(50);
            }
        }

        // run tutorial forge
        public static void Tut_Forge(Bitmap ss)
        {
            // forge
            bool checkTutForge1 = Tool.PixelSearch(459, 96, 0xFD6D3D, ss);
            //bool checkTutForge2 = Tool.PixelSearch(456, 48, 0xFFFFFF, ss);
            if (checkTutForge1)
            {
                Tool.MouseClick(461, 62);   // click forge icon
                Thread.Sleep(50);
            }

            bool checkArmor1 = Tool.PixelSearch(545, 190, 0xFBFBFB, ss);
            bool checkArmor2 = Tool.PixelSearch(556, 181, 0xC2C2C2, ss);
            bool checkArmor3 = Tool.PixelSearch(574, 174, 0xFBFBFB, ss);
            if (checkArmor1 && checkArmor2 && checkArmor3)
            {
                Tool.MouseClick(563, 191);  // click armor tab inventory
                Thread.Sleep(50);
            }

            bool checkArmor4 = Tool.PixelSearch(456, 238, 0xFBFBFA, ss);
            bool checkArmor5 = Tool.PixelSearch(526, 240, 0xFBFBFB, ss);
            if (checkArmor4 && checkArmor5)
            {
                Tool.MouseClick(491, 235);  // click armor inventory
                Thread.Sleep(50);
            }

            bool checkAutoSelect1 = Tool.PixelSearch(694, 519, 0xFBFBFB, ss);
            bool checkAutoSelect2 = Tool.PixelSearch(794, 519, 0xFBFBFB, ss);
            if (checkAutoSelect1 && checkAutoSelect2)
            {
                Tool.MouseClick(739, 522);  // click auto-select
                Thread.Sleep(50);
            }

            bool checkLevelup1 = Tool.PixelSearch(349, 518, 0xFFFFFF, ss);
            bool checkLevelup2 = Tool.PixelSearch(434, 531, 0xFF7B50, ss);
            if (checkLevelup1 && checkLevelup2)
            {
                Tool.MouseClick(399, 516);  // click level up
                Thread.Sleep(50);
            }

            // click enhance
            bool checkEnhance1 = Tool.PixelSearch(27, 322, 0xCBCBCB, ss);
            bool checkEnhance2 = Tool.PixelSearch(32, 388, 0xCBCBCB, ss);
            bool checkEnhance3 = Tool.PixelSearch(47, 338, 0x4E4E4E, ss);


            if (checkEnhance1 && checkEnhance2 && checkEnhance3)
            {
                Tool.MouseClick(47, 357);
                Thread.Sleep(50);
            }

            // stop starforce
            bool checkStopSF1 = Tool.PixelSearch(445, 440, 0xC6D0D6, ss);
            bool checkStopSF2 = Tool.PixelSearch(432, 439, 0xFF7B50, ss);
            if (checkStopSF1 && checkStopSF2)
            {
                Tool.MouseClick(401, 445);  // click level up
                Thread.Sleep(50);
            }
        }

        public static void Tut_Fever(Bitmap ss)
        {
            // fever
            bool checkFever11 = Tool.PixelSearch(520, 559, 0xFD6D3D, ss);
            //bool checkFever12 = Tool.PixelSearch(522, 592, 0xFFFAF2, ss);
            if (checkFever11)
            {
                Tool.MouseClick(517, 593);
                Thread.Sleep(50);
            }

            // purchase fever
            bool checkPurchase1 = Tool.PixelSearch(420, 449, 0xFBFBF9, ss);
            bool checkPurchase2 = Tool.PixelSearch(604, 449, 0xFBFBFB, ss);
            bool checkPurchase3 = Tool.PixelSearch(576, 471, 0xE45F36, ss);
            if (checkPurchase1 && checkPurchase2 && checkPurchase3)
            {
                Tool.MouseClick(502, 449);
                Thread.Sleep(50);
            }

            // confirm purchase fever
            bool checkConfirmPurchase1 = Tool.PixelSearch(401, 443, 0xFBFBFA, ss);
            bool checkConfirmPurchase2 = Tool.PixelSearch(587, 439, 0xFBFBFA, ss);
            bool checkConfirmPurchase3 = Tool.PixelSearch(551, 426, 0xFF7B50, ss);
            if (checkConfirmPurchase1 && checkConfirmPurchase2 && checkConfirmPurchase3)
            {
                Tool.MouseClick(498, 430);
                Thread.Sleep(50);
            }
        }

        public static void Tut_Jewel(Bitmap ss)
        {
            // Jewel icon
            bool checkJewelIcon1 = Tool.PixelSearch(380, 168, 0xFF6B39, ss); // the arrow
            bool checkJewelIcon2 = Tool.PixelSearch(377, 130, 0xFFFFFF, ss); // the jewel icon
            bool checkJewelIcon3 = Tool.PixelSearch(377, 130, 0x808080, ss); // the jewel icon
            if ((checkJewelIcon1 && checkJewelIcon2) || (checkJewelIcon1 && checkJewelIcon3))
            {
                Tool.MouseClick(381, 131);   // click jewel icon tut
                Thread.Sleep(50);
            }

            // click jewel
            bool checkJewel1 = Tool.PixelSearch(458, 246, 0xFFFFFD, ss);
            bool checkJewel2 = Tool.PixelSearch(528, 246, 0xFFFFFD, ss);
            bool checkJewel3 = Tool.PixelSearch(486, 112, 0xFFFFFE, ss);
            if (checkJewel1 && checkJewel2 && checkJewel3)
            {
                Tool.MouseClick(489, 246);   // click jewel tut
                Thread.Sleep(50);
            }

            // register jewel
            bool checkRegisterJewel1 = Tool.PixelSearch(504, 509, 0xFBFBFA, ss);
            bool checkRegisterJewel2 = Tool.PixelSearch(689, 509, 0xFBFBFA, ss);
            bool checkRegisterJewel3 = Tool.PixelSearch(538, 508, 0xFF7B50, ss);
            if (checkRegisterJewel1 && checkRegisterJewel2 && checkRegisterJewel3)
            {
                Tool.MouseClick(595, 503);   // click register jewel tut
                Thread.Sleep(50);
            }

            // choose jewel slot
            bool checkJewelSlot1 = Tool.PixelSearch(227, 227, 0xFBFBFB, ss);
            bool checkJewelSlot2 = Tool.PixelSearch(324, 227, 0xFBFBFB, ss);
            bool checkJewelSlot3 = Tool.PixelSearch(276, 231, 0xBCCDD4, ss);
            if (checkJewelSlot1 && checkJewelSlot2 && checkJewelSlot3)
            {
                Tool.MouseClick(275, 222);   // click jewel slot tut
                Thread.Sleep(50);
            }
        }

        public static void Tut_Dungeon(Bitmap ss)
        {
            // dungeon icon
            bool checkDungeonIcon1 = Tool.PixelSearch(686, 95, 0xFF803E, ss); // arrow > dungeon icon
            //bool checkDungeonIcon2 = Tool.PixelSearch(685, 63, 0xFFFFFF, ss);
            if (checkDungeonIcon1)
            {
                Tool.MouseClick(688, 58);   // click dungeon icon tut
                Thread.Sleep(50);
            }

            // daily dungeon tut
            bool checkDailyDungeon1 = Tool.PixelSearch(72, 191, 0xf0c24b, ss);
            bool checkDailyDungeon2 = Tool.PixelSearch(158, 253, 0xf2c24a, ss);
            bool checkDailyDungeon3 = Tool.PixelSearch(76, 366, 0xdcc255, ss);
            if (checkDailyDungeon1 && checkDailyDungeon2 && checkDailyDungeon3)
            {
                Tool.MouseClick(88, 277);   // click daily dungeon icon tut
                Thread.Sleep(50);
            }

            // elite dungeon tut
            bool checkEliteDungeon1 = Tool.PixelSearch(207, 191, 0xf0c24b, ss);
            bool checkEliteDungeon2 = Tool.PixelSearch(297, 257, 0xe4c04d, ss);
            bool checkEliteDungeon3 = Tool.PixelSearch(216, 366, 0xdcc255, ss);
            if (checkEliteDungeon1 && checkEliteDungeon2 && checkEliteDungeon3)
            {
                Tool.MouseClick(216, 298);   // click elite dungeon icon tut
                Thread.Sleep(50);
            }

            //// daily dungeon tut
            //bool checkDailyDungeon1 = Tool.PixelSearch(16, 395, 0xedc24c, ss);
            //bool checkDailyDungeon2 = Tool.PixelSearch(158, 397, 0xf2c24a, ss);
            //bool checkDailyDungeon3 = Tool.PixelSearch(95, 364, 0xebc24e, ss);
            //if (checkDailyDungeon1 && checkDailyDungeon2 && checkDailyDungeon3)
            //{
            //    Tool.MouseClick(87, 446);   // click daily dungeon icon tut
            //    Thread.Sleep(50);
            //}

            //// elite dungeon tut
            //bool checkEliteDungeon1 = Tool.PixelSearch(297, 430, 0xe4c04d, ss);
            //bool checkEliteDungeon2 = Tool.PixelSearch(177, 539, 0xe1c252, ss);
            //bool checkEliteDungeon3 = Tool.PixelSearch(216, 364, 0xebc24e, ss);
            //if (checkEliteDungeon1 && checkEliteDungeon2 && checkEliteDungeon3)
            //{
            //    Tool.MouseClick(252, 450);   // click elite dungeon icon tut
            //    Thread.Sleep(50);
            //}
        }

        public static void Tut_Auto(Bitmap ss)
        {
            // check auto icon
            bool checkAutoIcon1 = Tool.PixelSearch(207, 557, 0xFF7139, ss); // arrow > auto
            //bool checkAutoIcon2 = Tool.PixelSearch(200, 591, 0xE1D195, ss); // check top of A in AUTO (yellow)
            //bool checkAutoIcon3 = Tool.PixelSearch(215, 591, 0xEADA9C, ss); // check top of T in AUTO (yellow)
            if (checkAutoIcon1)
            {
                Tool.MouseClick(210, 593);   // click elite dungeon icon tut
                Thread.Sleep(50);
            }

            // use free 2hours auto
            bool checkUseFreeAuto1 = Tool.PixelSearch(563, 361, 0x59B0A8, ss);
            bool checkUseFreeAuto2 = Tool.PixelSearch(609, 370, 0x59B0A8, ss); // check top of A in AUTO (yellow)
            bool checkUseFreeAuto3 = Tool.PixelSearch(609, 358, 0xFC492B, ss); // check top of T in AUTO (yellow)
            if (checkUseFreeAuto1 && checkUseFreeAuto2 && checkUseFreeAuto3)
            {
                Tool.MouseClick(573, 363);   // click elite dungeon icon tut
                Thread.Sleep(50);
            }

            // close auto battle panel
            bool checkCloseAuto1 = Tool.PixelSearch(420, 341, 0x448EE0, ss); // blue free label 
            bool checkCloseAuto2 = Tool.PixelSearch(353, 240, 0xFFD741, ss); // yellow time remain label
            bool checkCloseAuto3 = Tool.PixelSearch(350, 345, 0xFF7B52, ss); // red circle
            if (checkCloseAuto1 && checkCloseAuto2 && checkCloseAuto3)
            {
                Tool.MouseClick(618, 186);   // click elite dungeon icon tut
                Thread.Sleep(50);
            }
        }
    }
}
