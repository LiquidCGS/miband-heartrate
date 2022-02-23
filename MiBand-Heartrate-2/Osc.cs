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

        static public string BPMParam = "BPM";
        static public string TimeParam = "IrlTime";

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

            float preset = 200;
            float formattedBPM = (BPM/preset);

            //Start sending Packages, this currently manipulates both Horizontal and Vertical inputs
            sender.Send(new OscMessage("/avatar/parameters/"+ BPMParam, formattedBPM));
        }


        static public void OscSendTime()
        {

            if (!OSCRealTimeEnable) return;

            int currentTime = System.DateTime.Now.Minute + (System.DateTime.Now.Hour * 100);

            float preset = 2359;
            float formattedTime = (currentTime / preset);

            //Start sending Packages, this currently manipulates both Horizontal and Vertical inputs
            sender.Send(new OscMessage("/avatar/parameters/" + TimeParam, formattedTime));
        }
    }
}
