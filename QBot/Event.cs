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
            // event royal hair big banner
            bool checkRoyalhair1 = Tool.PixelSearch(64, 107, 0x961a2c, ss);
            bool checkRoyalhair2 = Tool.PixelSearch(751, 112, 0x491b13, ss);
            bool checkRoyalhair3 = Tool.PixelSearch(470, 170, 0xd5c09f, ss);
            if (checkRoyalhair1 && checkRoyalhair2 && checkRoyalhair3)
            {
                Tool.Mouse_Click(751, 112);   // close event banner
                Thread.Sleep(50);
            }

            // close big banner
            bool checkBigEvent1 = Tool.PixelSearch(118, 157, 0xf47920, ss);
            bool checkBigEvent2 = Tool.PixelSearch(106, 161, 0xf47920, ss);
            bool checkBigEvent3 = Tool.PixelSearch(131, 162, 0xf47920, ss);
            if (checkBigEvent1 && checkBigEvent2 && checkBigEvent3)
            {
                Tool.Mouse_Click(691, 149);   // close event banner
                Thread.Sleep(50);
            }

            // close small banners
            bool checkSmallEvent1 = Tool.PixelSearch(116, 147, 0xf47920, ss);
            bool checkSmallEvent2 = Tool.PixelSearch(107, 152, 0xf47920, ss);
            bool checkSmallEvent3 = Tool.PixelSearch(123, 152, 0xf47920, ss);
            if (checkSmallEvent1 && checkSmallEvent2 && checkSmallEvent3)
            {
                Tool.Mouse_Click(126, 493);   // do not show again today
                Thread.Sleep(50);
            }


            Tool.Mouse_Click(530, 222);
            Thread.Sleep(100);
        }
    }
}
