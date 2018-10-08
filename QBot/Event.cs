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
            bool checkRoyalStyle1 = Tool.PixelSearch(55, 114, 0xF47920, ss); // M icon left corner
            bool checkRoyalStyle2 = Tool.PixelSearch(404, 118, 0xFFDF48, ss); // yellow banner
            bool checkRoyalStyle3 = Tool.PixelSearch(750, 111, 0xFDB8BC, ss); // blue x icon
            if (checkRoyalStyle1 && checkRoyalStyle2 && checkRoyalStyle3)
            {
                Tool.Mouse_Click(750, 111);   // close event banner
                Thread.Sleep(50);
            }
        }
    }
}
