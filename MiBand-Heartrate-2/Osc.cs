using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Rug.Osc;

namespace MiBand_Heartrate_2
{
    public class Osc
    {
        

        static public void OscSend(float BPM)
        {
            float preset = 200;
            float formattedBPM = (BPM/preset);

            //Create a new OscSender to send packages
            OscSender sender = new OscSender(IPAddress.Parse("127.0.0.1"), 0, 9000);
            //Connect the sender with given port and IP
            sender.Connect();
            //Start sending Packages, this currently manipulates both Horizontal and Vertical inputs
            sender.Send(new OscMessage("/avatar/parameters/BPM", formattedBPM));
        }
    }
}
