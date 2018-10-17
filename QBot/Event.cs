using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QMapleBot
{
    class Event
    {
        // event ingame
        public static void Do_Event(Bitmap ss)
        {
            // event return cygnus
            bool checkReturn1 = Tool.PixelSearch(86, 139, 0x515F6E, ss);
            bool checkReturn2 = Tool.PixelSearch(92, 194, 0x23AFFF, ss);
            bool checkReturn3 = Tool.PixelSearch(716, 142, 0xFFFFFF, ss);
            if (checkReturn1 && checkReturn2 && checkReturn3)
            {
                Tool.Mouse_Click(716, 142);   // close event banner
                Thread.Sleep(50);
            }

            // event golden apple
            bool checkEventGA1 = Tool.PixelSearch(114, 148, 0xF47920, ss); // M icon left corner
            bool checkEventGA2 = Tool.PixelSearch(242, 389, 0xEAB20A, ss); // golden apple
            bool checkEventGA3 = Tool.PixelSearch(691, 150, 0xAF895B, ss); // yellow x icon
            if (checkEventGA1 && checkEventGA2 && checkEventGA3)
            {
                Tool.Mouse_Click(691, 150);   // close event banner
                Thread.Sleep(50);
            }

            // event royal style doctor
            bool checkRoyalStyle1 = Tool.PixelSearch(114, 148, 0xF47920, ss); // M icon left corner
            bool checkRoyalStyle2 = Tool.PixelSearch(513, 512, 0xFFD200, ss); // package shop
            bool checkRoyalStyle3 = Tool.PixelSearch(693, 148, 0x879BB1, ss); // x icon            
            if (checkRoyalStyle1 && checkRoyalStyle2 && checkRoyalStyle3)
            {
                Tool.Mouse_Click(693, 148);   // close event banner
                Thread.Sleep(50);
            }

            // event buff pet
            bool checkBuffPet1 = Tool.PixelSearch(114, 148, 0xF47920, ss); // M icon left corner
            bool checkBuffPet2 = Tool.PixelSearch(513, 512, 0xFFD200, ss); // shop
            bool checkBuffPet3 = Tool.PixelSearch(693, 148, 0xBBBEC4, ss); // x icon            
            if (checkBuffPet1 && checkBuffPet2 && checkBuffPet3)
            {
                Tool.Mouse_Click(693, 148);   // close event banner
                Thread.Sleep(50);
            }

            // event pakage
            bool checkPackage1 = Tool.PixelSearch(114, 148, 0xF47920, ss); // M icon left corner
            bool checkPackage2 = Tool.PixelSearch(513, 512, 0xFFD200, ss); // shop
            bool checkPackage3 = Tool.PixelSearch(693, 148, 0xFFFFFF, ss); // x icon            
            if (checkPackage1 && checkPackage2 && checkPackage3)
            {
                Tool.Mouse_Click(693, 148);   // close event banner
                Thread.Sleep(50);
            }
        }
    }
}
