using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeccaWebAPI
{
    public class SerialDevice : Device
    {
        private SerialPort myPort = new SerialPort("COM4");

        private void openCOM()
        {
            if (myPort.IsOpen == false)
            {
                //if not open, open the port
                myPort.BaudRate = 9600;
                myPort.Parity = Parity.None;
                myPort.StopBits = StopBits.One;
                myPort.Handshake = Handshake.None;
                myPort.Open();
                myPort.DtrEnable = true;
                myPort.RtsEnable = true;
                myPort.ReadTimeout = 200;
            }
        }

        private string PhoneNumber { get; set; }

        public void CheckAll(string phoneNumber_in)
        { }
        public string CheckReachable(string phoneNumber_in)
        {
            openCOM();

            string okString = " ";
            string connectString = " ";

            //message for AT checking
            byte[] writeBuffer = CreateMessage(65, 84, 13); //AT. -> 41 54 0d

            //wait until Clear To Send
            while (myPort.CtsHolding == false) { }

            //if Clear To Send
            if (myPort.CtsHolding == true)
            {
                //check if AT is OK
                okString = WriteAndGet(writeBuffer, "OK");

                //if OK
                if (okString.Contains("OK"))
                {
                    //create the call message
                    byte[] callBuffer = DeviceCallMsg(phoneNumber_in);

                    //check if a connection can be established, so if reachable
                    connectString = WriteAndGet(callBuffer, "CONNECT");                   
                }

                myPort.Close();
            }

            string returnString = " ";
            if (connectString.Contains("CONNECT"))
            {
                returnString = "Raggiungibile";
            }
            else
            {
                returnString = "Non raggiungibile";
            }

            return returnString;
        }

        public double CheckTemperature(string PhoneNumber_in)
        {
            return 15;
        }
        public string CheckTime(string PhoneNumber_in)
        {
            return new DateTime().AddDays(2).ToString();
        }
        public string CheckNodes(string PhoneNumber_in)
        {
            return "ok,KO,ok";
        }

        public string CheckVoltage(string PhoneNumber_in)
        {
            return "10,11,10";
        }

        // function to translate integers to exadecimals
        public byte[] CreateMessage(params int[] parameters)
        {
            var buf = new byte[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                Array.Copy(BitConverter.GetBytes(parameters[i]), 0, buf, i, 1);
            return buf;
        }


        //write a message (buffer_in) in the serial port and wait for the response. Listen until checkString_in is reached
        public string WriteAndGet(byte[] buffer_in, string checkString_in)
        {
            string returnString = " ";
            //write the newly created array
            myPort.Write(buffer_in, 0, buffer_in.Length);
            myPort.ReadTimeout = 10000;
            if (myPort.BytesToRead > 0)
            {
                byte[] inbyte = new byte[1];

                while (returnString.Contains(checkString_in) == false)
                {
                    try
                    {
                        myPort.Read(inbyte, 0, 1);
                        if (inbyte.Length > 0)
                        {
                            byte value = (byte)inbyte.GetValue(0);
                            returnString += System.Text.Encoding.ASCII.GetString(new[] { value });
                            //do other necessary processing you may want. 
                        }
                    }
                    catch { }
                }
            }

            return returnString;
        }


        //build a message to call a device
        public byte[] DeviceCallMsg(string phoneNumber_in)
        {
            //call the device
            //ATDT3331234567. -> 41 54 44 54 33 33 33 31 32 33 34 35 36 37 (each phone digit is a "3" followed by the digit itself)
            byte[] writeBuffer1 = CreateMessage(65, 84, 68, 84);//ATDT 
            char[] phoneNumArray = phoneNumber_in.ToCharArray();//split phonenumber into single digits
            byte[] writeBuffer2 = new byte[phoneNumArray.Length];
            int i = 0;
            //convert each digit and put in buffer
            foreach (char num in phoneNumArray)
            {
                writeBuffer2[i] = CreateMessage(num)[0];//num tel
                i++;
            }
            i = 0;
            byte[] writeBuffer3 = CreateMessage(13);//.
            byte[] callBuffer = new byte[writeBuffer1.Length + writeBuffer2.Length + writeBuffer3.Length];

            //create the message to be sent concatenating the 3 arrays generated
            writeBuffer1.CopyTo(callBuffer, 0);
            writeBuffer2.CopyTo(callBuffer, writeBuffer1.Length);
            writeBuffer3.CopyTo(callBuffer, writeBuffer1.Length + writeBuffer2.Length);

            return callBuffer;
        }
    }
}
