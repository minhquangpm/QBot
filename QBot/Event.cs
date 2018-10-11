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

            // event royal style
            bool checkRoyalStyle1 = Tool.PixelSearch(114, 148, 0xF47920, ss); // M icon left corner
            bool checkRoyalStyle2 = Tool.PixelSearch(382, 160, 0x6D3E02, ss);
            bool checkRoyalStyle3 = Tool.PixelSearch(693, 148, 0x5A4427, ss); // x icon            if (checkRoyalStyle1 && checkRoyalStyle2 && checkRoyalStyle3)
            {
                Tool.Mouse_Click(693, 148);   // close event banner
                Thread.Sleep(50);
            }
        }
    }
}
