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
            bool pingOk;
            // Ping's the desired machine
            Ping pingSender = new Ping();
            IPAddress address = IPAddress.Parse(IpAddress);
            PingReply reply = pingSender.Send(address);

            if (reply.Status == IPStatus.Success)
            {
                pingOk = true;
                /*
                Console.WriteLine("Address: {0}", reply.Address.ToString());
                Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
                */
            }
            else
            {
                pingOk = false;
                //Console.WriteLine(reply.Status);
            }

            return pingOk;
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
            return false;
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
