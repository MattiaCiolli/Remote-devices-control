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
        void CheckAll(string address_in);
        string CheckReachable(string address_in);
        double CheckTemperature(string address_in);
        DateTime CheckTime(string address_in);
        string CheckNodes(string address_in);
    }
}
