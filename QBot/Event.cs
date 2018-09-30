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
            bool checkEventReturnC1 = Tool.PixelSearch(88, 138, 0x515F6E, ss);
            bool checkEventReturnC2 = Tool.PixelSearch(651, 136, 0x515F6E, ss);
            bool checkEventReturnC3 = Tool.PixelSearch(716, 142, 0xFFFFFF, ss); // white x icon
            if (checkEventReturnC1 && checkEventReturnC2 && checkEventReturnC3)
            {
                Tool.Mouse_Click(Bot.hwnd, 716, 142);   // close event banner
                Thread.Sleep(50);
            }

            // event golden apple
            bool checkEventGA1 = Tool.PixelSearch(57, 114, 0xF47920, ss); // M icon left corner
            bool checkEventGA2 = Tool.PixelSearch(203, 414, 0xE8B20F, ss); // golden apple
            bool checkEventGA3 = Tool.PixelSearch(750, 113, 0xD8A615, ss); // yellow x icon
            if (checkEventGA1 && checkEventGA2 && checkEventGA3)
            {
                Tool.Mouse_Click(Bot.hwnd, 750, 113);   // close event banner
                Thread.Sleep(50);
            }

            // event golden apple 2
            bool checkEvent2GA1 = Tool.PixelSearch(115, 147, 0xF47920, ss); // M icon left corner
            bool checkEvent2GA2 = Tool.PixelSearch(261, 390, 0xECB009, ss); // golden apple
            bool checkEvent2GA3 = Tool.PixelSearch(691, 150, 0xAD8B1A, ss); // yellow x icon
            if (checkEvent2GA1 && checkEvent2GA2 && checkEvent2GA3)
            {
                Tool.Mouse_Click(Bot.hwnd, 691, 150);   // close event banner
                Thread.Sleep(50);
            }

            // event package
            bool checkEventPackage1 = Tool.PixelSearch(113, 150, 0xF47920, ss); // M icon left corner
            bool checkEventPackage2 = Tool.PixelSearch(568, 499, 0xF5BE1B, ss); // yellow shopping btn
            bool checkEventPackage3 = Tool.PixelSearch(693, 150, 0xB8BFC6, ss); // grey x icon
            if (checkEventPackage1 && checkEventPackage2 && checkEventPackage3)
            {
                Tool.Mouse_Click(Bot.hwnd, 693, 150);   // close event banner
                Thread.Sleep(50);
            }

            // event package cygnus
            bool checkEventPackage4 = Tool.PixelSearch(113, 150, 0xF47920, ss); // M icon left corner
            bool checkEventPackage5 = Tool.PixelSearch(543, 499, 0xF5BE1B, ss); // yellow shopping btn
            bool checkEventPackage6 = Tool.PixelSearch(693, 150, 0x8BCBEB, ss); // blue x icon
            if (checkEventPackage4 && checkEventPackage5 && checkEventPackage6)
            {
                Tool.Mouse_Click(Bot.hwnd, 693, 150);   // close event banner
                Thread.Sleep(50);
            }

            // event royal style
            bool checkRoyalStyle1 = Tool.PixelSearch(55, 114, 0xF47920, ss); // M icon left corner
            bool checkRoyalStyle2 = Tool.PixelSearch(404, 118, 0xFFDF48, ss); // yellow banner
            bool checkRoyalStyle3 = Tool.PixelSearch(750, 111, 0xFDB8BC, ss); // blue x icon
            if (checkEventPackage4 && checkEventPackage5 && checkEventPackage6)
            {
                Tool.Mouse_Click(Bot.hwnd, 750, 111);   // close event banner
                Thread.Sleep(50);
            }
        }
    }
}
