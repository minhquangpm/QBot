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
                Tool.MouseClick(751, 112);   // close event banner
                Thread.Sleep(50);
            }

            // close big banner
            bool checkBigEvent1 = Tool.PixelSearch(118, 157, 0xf47920, ss);
            bool checkBigEvent2 = Tool.PixelSearch(106, 161, 0xf47920, ss);
            bool checkBigEvent3 = Tool.PixelSearch(131, 162, 0xf47920, ss);
            if (checkBigEvent1 && checkBigEvent2 && checkBigEvent3)
            {
                Tool.MouseClick(691, 149);   // close event banner
                Thread.Sleep(50);
            }

            // close small banners
            bool checkSmallEvent1 = Tool.PixelSearch(116, 147, 0xf47920, ss);
            bool checkSmallEvent2 = Tool.PixelSearch(107, 152, 0xf47920, ss);
            bool checkSmallEvent3 = Tool.PixelSearch(123, 152, 0xf47920, ss);

            bool checkSmallEvent4 = Tool.PixelSearch(106, 155, 0xf47920, ss);
            bool checkSmallEvent5 = Tool.PixelSearch(121, 154, 0xf47920, ss);

            if ((checkSmallEvent1 && checkSmallEvent2 && checkSmallEvent3) ||
                (checkSmallEvent1 && checkSmallEvent4 && checkSmallEvent5))
            {
                Tool.MouseClick(126, 493);   // do not show again today
                Thread.Sleep(50);
            }

            // close attendance
            bool checkAttendance1 = Tool.PixelSearch(101, 134, 0x515f6e, ss);
            bool checkAttendance2 = Tool.PixelSearch(666, 142, 0x515f6e, ss);
            bool checkAttendance3 = Tool.PixelSearch(677, 276, 0xff2b4c, ss);
            if (checkAttendance1 && checkAttendance2 && checkAttendance3)
            {
                Tool.MouseClick(717, 143);   // close attendance banner
                Thread.Sleep(50);
            }


            //Tool.MouseClick(796, 370);
            //Thread.Sleep(100);
        }
    }
}
