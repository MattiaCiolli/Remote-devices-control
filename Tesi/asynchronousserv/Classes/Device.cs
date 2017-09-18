using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    interface Device
    {
        void CheckReachable(string address);
        void CheckTemperature(string address);
        void CheckTime(string address);
        void CheckNodes(string address);
    }

    public class IPDevice : Device
    {
        private string IpAddress { get; set; }

        public void CheckReachable(string IpAddress)
        { }
        public void CheckTemperature(string IpAddress)
        { }
        public void CheckTime(string IpAddress)
        { }
        public void CheckNodes(string IpAddress)
        { }
    }

    public class SerialDevice : Device
    {
        private string PhoneNumber { get; set; }

        public void CheckReachable(string PhoneNumber)
        { }
        public void CheckTemperature(string PhoneNumber)
        { }
        public void CheckTime(string PhoneNumber)
        { }
        public void CheckNodes(string PhoneNumber)
        { }
    }
}
