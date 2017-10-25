using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    public class IPDevice : Device
    {
        private string IpAddress { get; set; }

        public void CheckAll(string IpAddress)
        { }

        public string CheckReachable(string IpAddress_in)
        {
            string ris = null;
            // Ping's the desired machine
            Ping pingSender = new Ping();
            IPAddress address = IPAddress.Parse(IpAddress_in);
            PingReply reply = pingSender.Send(address);

            if (reply.Status == IPStatus.Success)
            {
                ris = "Address: " + reply.Address.ToString() + " RoundTrip time: " + reply.RoundtripTime + " Ttl: " + reply.Options.Ttl;
            }
            else
            {
                ris = reply.Status.ToString();
            }

            return ris;
        }

        public double CheckTemperature(string IpAddress_in)
        {
            return 12.5;
        }

        public DateTime CheckTime(string IpAddress_in)
        {
            return new DateTime();
        }

        public string CheckNodes(string IpAddress_in)
        {
            return "ok,ok,ok";
        }
    }
}
