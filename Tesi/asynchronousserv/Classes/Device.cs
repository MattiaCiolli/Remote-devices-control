using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    interface Device
    {
        void CheckAll(string address);
        string CheckReachable(string address);
        double CheckTemperature(string address);
        DateTime CheckTime(string address);
        string CheckNodes(string address);
    }
}
