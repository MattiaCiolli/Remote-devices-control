using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ZeccaWebAPI
{
    interface DeviceCaller
    {
        void CheckAll(string address_in, string badgeNumber_in);
        string CheckReachable(string address_in, string badgeNumber_in);
        double CheckTemperature(string address_in, string badgeNumber_in);
        string CheckTime(string address_in, string badgeNumber_in);
        string CheckNodes(string address_in, string badgeNumber_in);
        string CheckVoltage(string address_in, string badgeNumber_in);
    }
}
