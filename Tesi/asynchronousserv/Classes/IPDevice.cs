using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
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

        public string CheckVoltage(string IpAddress_in)
        {
            TcpClient client = new TcpClient();
            client.Connect(IpAddress_in, 49152);
            NetworkStream tcpStream = client.GetStream();
            byte[] myWriteBuffer = CreateMessage(175, 63, 33, 141, 10); //"af 3f 21 8d 0a";
            tcpStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);

            try
            {
                byte[] myReadBuffer = new byte[1024];
                StringBuilder myCompleteMessage = new StringBuilder();
                int numberOfBytesRead = 0;

                // Incoming message may be larger than the buffer size.
                do
                {
                    numberOfBytesRead = tcpStream.Read(myReadBuffer, 0, myReadBuffer.Length);

                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));

                }
                while (tcpStream.DataAvailable);

                // Print out the received message to the console.
                Console.WriteLine("You received the following message : " +
                                             myCompleteMessage);
            }
            catch
            {
                Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
            }
            return "10,10,10";
        }

        public byte[] CreateMessage(params int[] parameters)
        {
            var buf = new byte[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                Array.Copy(BitConverter.GetBytes(parameters[i]), 0, buf, i, 1);
            return buf;
        }
    }
}
