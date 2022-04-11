using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Rug.Osc;
using MiBand_Heartrate_2.Extras;

namespace MiBand_Heartrate_2
{

    public class Osc
    {
        static public bool OSCRealTimeEnable = false;
        static public bool OSCHeartRateEnable = false;

        static public string BPMParam = "w_BPM";
        static public string TimeHour = "w_HR";
        static public string TimeMins = "w_MN";

        static private OscSender sender = null;

        static public void OscStart()
        {
            //Create a new OscSender to send packages
            sender = new OscSender(IPAddress.Parse("127.0.0.1"), 0, 9000);
            //Connect the sender with given port and IP
            sender.Connect();
        }

        static public void OscStop()
        {
            sender.Dispose();
            sender = null;
        }

        static public void OscSendBPM(float BPM)
        {
            if (!OSCHeartRateEnable) return;
            sender.Send(new OscMessage("/avatar/parameters/"+ BPMParam, (BPM - 0) * (15.5f - (-15.5f)) / (255 - 0) + (-15.5f)));
        }


        static public void OscSendTime()
        {

            if (!OSCRealTimeEnable) return;

            sender.Send(new OscMessage("/avatar/parameters/" + TimeMins, (System.DateTime.Now.Minute - 0) * (15.5f - (-15.5f)) / (60 - 0) + (-15.5f)));
            //Start sending Packages, this currently manipulates both Horizontal and Vertical inputs
            sender.Send(new OscMessage("/avatar/parameters/" + TimeHour, (System.DateTime.Now.Hour - 0) * (12f - (-12f)) / (24 - 0) + (-12f)));
        }
    }
}
