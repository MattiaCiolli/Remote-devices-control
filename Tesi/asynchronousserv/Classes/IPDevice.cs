using System;
using System.Collections.Generic;
using System.IO;
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
            /*TcpClient client = new TcpClient();
            client.Connect(IpAddress_in, 49152);
            NetworkStream tcpStream = client.GetStream();
            byte[] myWriteBuffer = CreateMessage(175, 63, 33, 141, 10); //af 3f 21 8d 0a
            tcpStream.Write(myWriteBuffer, 0, myWriteBuffer.Length);
            StringBuilder myCompleteMessage = new StringBuilder();

            try
            {
                byte[] myReadBuffer = new byte[1024];
                int numberOfBytesRead = 0;

                // Incoming message may be larger than the buffer size.
                do
                {
                    numberOfBytesRead = tcpStream.Read(myReadBuffer, 0, myReadBuffer.Length);

                    myCompleteMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));

                }
                while (tcpStream.DataAvailable);

                // Print out the received message to the console.
                Console.WriteLine("You received the following message : " + myCompleteMessage);
            }
            catch
            {
                Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
            }*/

            string myCompleteMessage = @"LGZ52ZMD3104407.B24
F.F(00000000)
0.0.0(95316176)
0.0.1(95316176)
0.9.1(13:23:08)
0.9.2(17 - 11 - 03)
0.9.6(00:00)
0.9.7(17 - 11 - 01)
0.1.0(19)
1.8.0(000101.96 * kWh)
1.8.0 * 19(000101.71 * kWh)
1.8.1(0000.067 * kWh)
1.8.1 * 19(0000.997 * kWh)
1.8.2(0000.025 * kWh)
1.8.2 * 19(0000.720 * kWh)
1.8.3(0000.164 * kWh)
1.8.3 * 19(0001.401 * kWh)
1.8.4(0000.000 * kWh)
1.8.4 * 19(0000.000 * kWh)
2.8.0(000008.09 * kWh)
2.8.0 * 19(000008.09 * kWh)
2.8.1(0000.000 * kWh)
2.8.1 * 19(0000.000 * kWh)
2.8.2(0000.000 * kWh)
2.8.2 * 19(0000.000 * kWh)
2.8.3(0000.000 * kWh)
2.8.3 * 19(0000.000 * kWh)
2.8.4(0000.000 * kWh)
2.8.4 * 19(0000.000 * kWh)
5.8.0(000145.63 * kvarh)
5.8.0 * 19(000144.36 * kvarh)
5.8.1(0000.333 * kvarh)
5.8.1 * 19(0004.939 * kvarh)
5.8.2(0000.123 * kvarh)
5.8.2 * 19(0003.568 * kvarh)
5.8.3(0000.813 * kvarh)
5.8.3 * 19(0006.913 * kvarh)
5.8.4(0000.000 * kvarh)
5.8.4 * 19(0000.000 * kvarh)
6.8.0(000000.26 * kvarh)
6.8.0 * 19(000000.26 * kvarh)
6.8.1(0000.000 * kvarh)
6.8.1 * 19(0000.000 * kvarh)
6.8.2(0000.000 * kvarh)
6.8.2 * 19(0000.000 * kvarh)
6.8.3(0000.000 * kvarh)
6.8.3 * 19(0000.000 * kvarh)
6.8.4(0000.000 * kvarh)
6.8.4 * 19(0000.000 * kvarh)
7.8.0(000000.80 * kvarh)
7.8.0 * 19(000000.80 * kvarh)
7.8.1(0000.000 * kvarh)
7.8.1 * 19(0000.000 * kvarh)
7.8.2(0000.000 * kvarh)
7.8.2 * 19(0000.000 * kvarh)
7.8.3(0000.000 * kvarh)
7.8.3 * 19(0000.000 * kvarh)
7.8.4(0000.000 * kvarh)
7.8.4 * 19(0000.000 * kvarh)
8.8.0(000000.23 * kvarh)
8.8.0 * 19(000000.23 * kvarh)
8.8.1(0000.000 * kvarh)
8.8.1 * 19(0000.000 * kvarh)
8.8.2(0000.000 * kvarh)";

            string result = null;
            string returnString = null;
            StringReader reader = new StringReader(myCompleteMessage.ToString());
            while ((result = reader.ReadLine()) != null)
            {
                returnString += GetRegisterValue("0.9.1", result);
                returnString += GetRegisterValue("0.9.2", result);
                /*returnString += GetRegisterValue("32.7.0", result);
                returnString += GetRegisterValue("52.7.0", result);
                returnString += GetRegisterValue("72.7.0", result);*/
            }
            return returnString;
        }

        // function to translate integers to exadecimals
        // "af 3f 21 8d 0a" = ".?!.." but "." is represented also with "2e". Use 175 to represent "af" and so on
        public byte[] CreateMessage(params int[] parameters)
        {
            var buf = new byte[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                Array.Copy(BitConverter.GetBytes(parameters[i]), 0, buf, i, 1);
            return buf;
        }

        //gets a register value
        public string GetRegisterValue(string RegisterNum_in, string result_in)
        {
            string FinalString;
            string selectString = RegisterNum_in + "(";

            if (result_in.Contains(selectString))
            {
                FinalString = Between(result_in, selectString, ")");
            }
            else
            {
                FinalString = "";
            }

            return FinalString;
        }

        //extracts a string between FirstString and LastString excluded.
        //Ex. Between("abc", "a", "c") = b
        public string Between(string Text_in, string FirstString_in, string LastString_in)
        {
            string str = Text_in; ;
            string FinalString;

            int Pos1 = str.IndexOf(FirstString_in) + FirstString_in.Length;
            int Pos2 = str.IndexOf(LastString_in);
            FinalString = str.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }
}