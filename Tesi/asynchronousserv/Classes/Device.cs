using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    interface Device
    {
        void CheckAll(string address);
        bool CheckReachable(string address);
        double CheckTemperature(string address);
        DateTime CheckTime(string address);
        string CheckNodes(string address);
    }

    public class IPDevice : Device
    {
        private string IpAddress { get; set; }

        public void CheckAll(string IpAddress)
        { }

        public bool CheckReachable(string IpAddress)
        {
            //ping
            return false;
        }

        public double CheckTemperature(string IpAddress)
        {        
            return 12.5;       
        }

        public DateTime CheckTime(string IpAddress)
        {
            return new DateTime();
        }

        public string CheckNodes(string IpAddress)
        {
            return "ok,ok,ok";
        }
    }

    public class SerialDevice : Device
    {
        private string PhoneNumber { get; set; }

        public void CheckAll(string PhoneNumber)
        { }
        public bool CheckReachable(string PhoneNumber)
        {
            return true;
        }
        public double CheckTemperature(string PhoneNumber)
        {
            return 15;
        }
        public DateTime CheckTime(string PhoneNumber)
        {
            return new DateTime().AddDays(2);
        }
        public string CheckNodes(string PhoneNumber)
        {
            return "ok,KO,ok";
        }
    }
}
