using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace asynchronousserv
{
    public class IPDevice : Device
    {
        private ReturnManager rm = new ReturnManager();

        internal ReturnManager Rm
        {
            get
            {
                return rm;
            }

            set
            {
                rm = value;
            }
        }

        public void CheckAll(string ipAddress_in)
        { }

        public string CheckReachable(string ipAddress_in)
        {
            string ris = null;
            // Ping's the desired machine
            Ping pingSender = new Ping();
            IPAddress address = IPAddress.Parse(ipAddress_in);
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

        public double CheckTemperature(string ipAddress_in)
        {
            return 12.5;
        }

        public string CheckTime(string ipAddress_in)
        {
            ErrMsgObj pkg = ConnectAndGet(ipAddress_in, 49152);
            string result = null;
            if (pkg.ErrCode == ENUM.ERRORS.NO_ERRORS)
            {
                result = AnalyzeRemotePackage(pkg.Data, "0.9.1", "0.9.2");
            }
            else
            {
                result = Rm.AnalyzeErrMsgObj(pkg);
            }
            return result;
        }

        public string CheckNodes(string ipAddress_in)
        {
            return "ok,ok,ok";
        }

        public string CheckVoltage(string ipAddress_in)
        {
            ErrMsgObj pkg = ConnectAndGet(ipAddress_in, 49152);
            string result = null;
            if (pkg.ErrCode == ENUM.ERRORS.NO_ERRORS)
            {
                result = AnalyzeRemotePackage(pkg.Data, "32.7.0", "52.7.0", "72.7.0");
            }
            else
            {
                result = Rm.AnalyzeErrMsgObj(pkg);
            }
            return result;
        }

        // connects to the server to get data
        public ErrMsgObj ConnectAndGet(string ipAddress_in, int port)
        {
             TcpClient tcpClient = new TcpClient();
             StringBuilder completeMessage = new StringBuilder();
             ENUM.ERRORS err = ENUM.ERRORS.NO_ERRORS;
             try
             {
                 tcpClient.Connect(ipAddress_in, 49152);
                 NetworkStream tcpStream = tcpClient.GetStream();
                 byte[] writeBuffer = CreateMessage(175, 63, 33, 141, 10); //af 3f 21 8d 0a
                 tcpStream.Write(writeBuffer, 0, writeBuffer.Length);
                 tcpStream.ReadTimeout = 5000;

                 try
                 {
                     byte[] myReadBuffer = new byte[1024];
                     int numberOfBytesRead = 0;

                     // the message is bigger than the buffer. Execute the read until the reach of the "ETX" character (0x3)
                     while (!completeMessage.ToString().Contains("0x3"))
                     {
                         Console.WriteLine("-- " + completeMessage);
                         numberOfBytesRead = tcpStream.Read(myReadBuffer, 0, myReadBuffer.Length);
                         completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                     }

                     // Print out the received message to the console.
                     Console.WriteLine("Received the following message: " + completeMessage);
                 }
                 catch
                 {
                     err = ENUM.ERRORS.TCP_STREAM_READ_FAILED;
                 }
                 finally
                 {
                     tcpStream.Close();
                 }
             }
             catch (SocketException e)
             {               
                 err = ENUM.ERRORS.TCP_CONNECTION_FAILED;
             }
             finally
             {
                 tcpClient.Close();
             }


            /*
            //per quando non funziona il contatore

            ENUM.ERRORS err = ENUM.ERRORS.NO_ERRORS;
            string completeMessage = @"LGZ52ZMD3104407.B24
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
            8.8.2(0000.000 * kvarh)
            32.7.0(1000.000 * kvarh)
            52.7.0(2000.000 * kvarh)
            72.7.0(3000.000 * kvarh)";
            */
            return new ErrMsgObj(err, completeMessage.ToString(), null, 0);

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

        // analyzes the package sent by the server searching for all the registers listed in the "params"
        public string AnalyzeRemotePackage(string pkg_in, params string[] registerId_in)
        {
            string returnString = null;
            string errorString = "Unable to read data";
            StringReader reader = new StringReader(pkg_in.ToString());

            //in caso di stream multilinea
            /*string result = null;
            while ((result = reader.ReadLine()) != null)
            {
                foreach (string r in registerId_in)
                {
                    returnString += GetRegisterValue(r, pkg_in);
                }
            }*/

            foreach (string r in registerId_in)
            {
                returnString += GetRegisterValue(r, pkg_in) + " ";
            }

            if(string.IsNullOrWhiteSpace(returnString))
            {
                returnString = errorString;
            }

            return returnString;
        }

        // gets a register value
        public string GetRegisterValue(string registerNum_in, string result_in)
        {
            string FinalString;
            string selectString = registerNum_in;

            if (result_in.Contains(selectString))
            {
                FinalString = FindRegister(result_in, selectString, ")");
            }
            else
            {
                FinalString = "";
            }

            return FinalString;
        }

        // finds a register selected by registerId_in and extracts its value
        public string FindRegister(string text_in, string firstString_in, string lastString_in)
        {
            string FinalString;
            // Groups[1] gets the data between parenthesis, excluding them and the register number.
            // Without Groups[1] will return also the register number and the parenthesis
            FinalString = Regex.Match(text_in, firstString_in + @"\((.*?)\)").Groups[1].Value; 
            return FinalString;
        }
    }
}