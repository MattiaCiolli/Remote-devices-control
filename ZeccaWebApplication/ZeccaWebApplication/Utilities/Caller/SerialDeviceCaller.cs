﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ZeccaWebAPI
{
    public class SerialDeviceCaller : DeviceCaller
    {
        private SerialPort myPort = new SerialPort("COM4");
        System.Timers.Timer timer1 = new System.Timers.Timer(2000);
        System.Timers.Timer timer2 = new System.Timers.Timer(3000);

        //opens and sets the port
        private void openCOM()
        {
            if (myPort.IsOpen == false)
            {
                //if not open, open the port
                myPort.BaudRate = 9600;
                myPort.Parity = Parity.None;
                myPort.DataBits = 8;
                myPort.StopBits = StopBits.One;
                myPort.Handshake = Handshake.None;
                myPort.Open();
                myPort.DtrEnable = true;
                myPort.RtsEnable = true;
                myPort.ReadTimeout = 2000;
            }
        }

        //pass to command mode with +++ to send close command
        private void CloseCall(Object source, ElapsedEventArgs e)
        {
            myPort.ReadTimeout = 3000;
            timer1.Stop();
            byte[] closeBuffer1 = CreateMessage(43, 43, 43); //+++ -> 2b 2b 2b
            myPort.DataBits = 8;
            myPort.Parity = Parity.None;            
            string closeString1 = WriteAndGet(closeBuffer1, "OK");
            if (closeString1.Contains("OK"))
            {
                //needed for Hayes "+++" command            
                timer2.Elapsed += CloseCall2;
                timer2.Start();
            }
        }

        //end call sending ATH. command
        private void CloseCall2(Object source, ElapsedEventArgs e)
        {
            myPort.ReadTimeout = 400;
            timer2.Stop();
            string closeString = "";
            byte[] closeBuffer = CreateMessage(65, 84, 72, 13); //ATH. -> 41 54 48 0d
            while (closeString.Contains("ATH") == false)
            {
                while (closeString.Contains("OK") == false)
                {
                    closeString = WriteAndGet(closeBuffer, "OK");
                }
            }

            myPort.Close();
        }

        private string PhoneNumber { get; set; }

        public void CheckAll(string phoneNumber_in, string badgeNumber_in)
        { }

        public string CheckReachable(string phoneNumber_in, string badgeNumber_in)
        {
            openCOM();

            myPort.ReadTimeout = 20000;
            string connectString = ConnectToDevice(phoneNumber_in);
            string returnString = " ";

            if (connectString.Contains("CONNECT"))
            {
                returnString = "Raggiungibile";
            }
            else
            {
                returnString = "Non raggiungibile";
            }

            //needed for Hayes "+++" command            
            timer1.Elapsed += CloseCall;
            timer1.Start();

            return returnString;
        }

        public double CheckTemperature(string PhoneNumber_in, string badgeNumber_in)
        {
            return 15;
        }
        public string CheckTime(string phoneNumber_in, string badgeNumber_in)
        {
            openCOM();
            string dataString = " ";
            string result = " ";
            myPort.ReadTimeout = 20000;
            string connectString = ConnectToDevice(phoneNumber_in);
            //if CONNECTED get data
            if (connectString.Contains("Raggiungibile"))
            {
                myPort.ReadTimeout = 2000;
                //create query message
                byte[] fullQuestionBuffer = QueryMessage(badgeNumber_in);

                try
                {
                    myPort.ReadTimeout = 5000;
                    //read until end message
                    dataString = WriteAndGet(fullQuestionBuffer, "\u0003");
                }
                catch
                {
                    dataString = "Errore di lettura dei dati";
                }

                //filter registers
                result = AnalyzeRemotePackage(dataString, "0.9.1", "0.9.2");
            }
            else
            {
                result = "Errore di connessione";
            }

            //needed for Hayes "+++" command
            timer1.Elapsed += CloseCall;
            timer1.Start();

            return result;
        }

        public string CheckNodes(string phoneNumber_in, string badgeNumber_in)
        {
            return "ok,KO,ok";
        }

        public string CheckVoltage(string phoneNumber_in, string badgeNumber_in)
        {
            openCOM();
            string dataString = " ";
            string result = " ";
            myPort.ReadTimeout = 20000;
            string connectString = ConnectToDevice(phoneNumber_in);
            //if CONNECTED get data
            if (connectString.Contains("Raggiungibile"))
            {
                myPort.ReadTimeout = 3000;
                //create query message
                byte[] fullQuestionBuffer = QueryMessage(badgeNumber_in);

                try
                {
                    myPort.ReadTimeout = 5000;
                    //read until end message
                    dataString = WriteAndGet(fullQuestionBuffer, "\u0003");
                }
                catch
                {
                    dataString = "Errore di lettura dei dati";
                }
                //filter registers
                result = AnalyzeRemotePackage(dataString, "32.7.0", "52.7.0", "72.7.0");
            }
            else
            {
                result = "Errore di connessione";
            }

            //needed for Hayes "+++" command
            timer1.Elapsed += CloseCall;
            timer1.Start();

            return result;
        }

        // function to translate integers to exadecimals
        // https://www.binaryhexconverter.com/hex-to-decimal-converter. Convert the hex value in the IONinja log to decimal and pass it as parameters
        public byte[] CreateMessage(params int[] parameters)
        {
            var buf = new byte[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
                Array.Copy(BitConverter.GetBytes(parameters[i]), 0, buf, i, 1);
            return buf;
        }

        //create a message to ask the device to send its registers.
        public byte[] QueryMessage(string badgeNumber_in)
        {
            byte[] twentiesbuffer = CreateMessage(32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 32, 13, 10); // 19spaces..                 ../?37665538!../?37665538!..-> 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 20 0d 0a 2f 3f 33 37 36 36 35 35 33 38 21 0d 0a 2f 3f 33 37 36 36 35 35 33 38 21 0d 0a
            byte[] questionbuffer = CreateMessage(47, 63);// /?
            byte[] closequestionbuffer = CreateMessage(33, 13, 10);// !..
            char[] matricolaArray = badgeNumber_in.ToCharArray();//split badge number into single digits
            byte[] badgeBuffer = new byte[matricolaArray.Length];

            int i = 0;
            //convert each digit and put in buffer
            foreach (char num in matricolaArray)
            {
                badgeBuffer[i] = CreateMessage(num)[0];//num matricola
                i++;
            }

            //create the question message
            byte[] fullQuestionBuffer = new byte[twentiesbuffer.Length + questionbuffer.Length + badgeBuffer.Length + closequestionbuffer.Length + questionbuffer.Length + badgeBuffer.Length + closequestionbuffer.Length];

            //create the message to be sent concatenating the arrays generated
            twentiesbuffer.CopyTo(fullQuestionBuffer, 0);
            questionbuffer.CopyTo(fullQuestionBuffer, twentiesbuffer.Length);
            badgeBuffer.CopyTo(fullQuestionBuffer, twentiesbuffer.Length + questionbuffer.Length);
            closequestionbuffer.CopyTo(fullQuestionBuffer, twentiesbuffer.Length + questionbuffer.Length + badgeBuffer.Length);
            questionbuffer.CopyTo(fullQuestionBuffer, twentiesbuffer.Length + questionbuffer.Length + badgeBuffer.Length + closequestionbuffer.Length);
            badgeBuffer.CopyTo(fullQuestionBuffer, twentiesbuffer.Length + questionbuffer.Length + badgeBuffer.Length + closequestionbuffer.Length + questionbuffer.Length);
            closequestionbuffer.CopyTo(fullQuestionBuffer, twentiesbuffer.Length + questionbuffer.Length + badgeBuffer.Length + closequestionbuffer.Length + questionbuffer.Length + badgeBuffer.Length);

            //change parity bits to even and databits to 7 in order to talk with the device
            myPort.Parity = Parity.Even;
            myPort.DataBits = 7;           

            return fullQuestionBuffer;
        }

        //connects to a device and also checks if reachable
        public string ConnectToDevice(string phone_in)
        {
            string okString = " ";
            string connectString = " ";
            string returnString = " ";
            ENUM.ERRORS err = ENUM.ERRORS.NO_ERRORS;
            //message for AT checking
            byte[] writeBuffer = CreateMessage(65, 84, 13); //AT. -> 41 54 0d
            //wait until Clear To Send
            while (myPort.CtsHolding == false) { }

            //if Clear To Send
            if (myPort.CtsHolding == true)
            {
                try
                {
                    //check if AT is OK
                    okString = WriteAndGet(writeBuffer, "OK");
                }
                catch
                {
                    returnString = "Rete assente";
                    err = ENUM.ERRORS.SERIAL_GSM_CONNECTION_FAILED;
                }

                //if OK
                if (okString.Contains("OK"))
                {
                    //create the call message
                    byte[] callBuffer = DeviceCallMsg(phone_in);

                    try
                    {
                        //check if a connection can be established, so if reachable
                        connectString = WriteAndGet(callBuffer, "CONNECT");
                    }
                    catch
                    {
                        returnString = "Non raggiungibile";
                        err = ENUM.ERRORS.SERIAL_GSM_CONNECTION_FAILED;
                    }
                }
                else
                {
                    returnString = "Non raggiungibile";
                }
            }

            if (connectString.Contains("CONNECT"))
            {
                returnString = "Raggiungibile";
            }

            return returnString;
        }


        //write a message (buffer_in) in the serial port and wait for the response. Listen until checkString_in is reached
        public string WriteAndGet(byte[] buffer_in, string checkString_in)
        {
            ENUM.ERRORS err = ENUM.ERRORS.NO_ERRORS;
            StringBuilder completeMessage = new StringBuilder();
            //write the newly created array
            myPort.Write(buffer_in, 0, buffer_in.Length);
            string returnMsg = "";
            byte[] myReadBuffer = new byte[1024];
            //if received data doesn't contain the checkstring, return the received data anyway
            bool exit = false;
            // if the message is bigger than the buffer, execute the read until the reach of the "ETX" character (\u0003)
            while (returnMsg.Contains(checkString_in) == false && exit == false) 
            {
                try
                {
                    //numberOfBytesRead = myPort.Read(myReadBuffer, 0, myReadBuffer.Length);
                    // completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead));
                    if (exit == false)
                    {
                        returnMsg = returnMsg + myPort.ReadLine();
                    }
                }
                catch
                {
                    err = ENUM.ERRORS.SERIAL_GSM_READ_FAILED;
                    exit = true;
                }
            }

            /*
            if (returnMsg.Contains(checkString_in) == false)
            {
                try
                {
                    myPort.ReadTimeout = 1000;
                    returnMsg = returnMsg + myPort.ReadLine();
                }
                catch { }
            }*/

            return returnMsg;

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
                writeBuffer2[i] = CreateMessage(num)[0];// tel #
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

        // analyzes the package sent by the server searching for all the registers listed in the "params"
        public string AnalyzeRemotePackage(string pkg_in, params string[] registerId_in)
        {
            string returnString = null;
            string errorString = "Unable to read data";
            StringReader reader = new StringReader(pkg_in.ToString());

            foreach (string r in registerId_in)
            {
                returnString += GetRegisterValue(r, pkg_in) + " ";
            }

            if (string.IsNullOrWhiteSpace(returnString))
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
