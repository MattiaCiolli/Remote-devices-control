﻿using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace clientconsole
{
    class clientconsole
    {
        private TcpClient client;
        private static bool tryConnection = true;
        private StreamReader sReader;
        private StreamWriter sWriter;
        private Boolean isConnected;

        public clientconsole(String ipAddress, int portNum)
        {
            client = new TcpClient();
            client.Connect(ipAddress, portNum);
            HandleCommunication();
        }

        //handles communication with the server
        public void HandleCommunication()
        {
            //stream to write on server's console
            sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            //stream to read server's messages
            sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            tryConnection = false;
            isConnected = true;
            String sData = null;
            Interpreter intp = new Interpreter();
            //do this when connected
            while (isConnected)
            {
                Console.Write("^_^: ");
                sData = Console.ReadLine();
                string cmd = intp.CheckCommands(sData);
                if (cmd != null)
                {
                    if (cmd.Equals("close"))
                    {
                        isConnected = false;

                        //tells the server to close the connection
                        sWriter.WriteLine(sData);
                        sWriter.Flush();
                        client.GetStream().Close();
                    }
                    else
                    {
                        // write data on servers's console and flush
                        sWriter.WriteLine(cmd);
                        sWriter.Flush();

                        // receive from server
                        String sDataIncomming = sReader.ReadLine();
                        Console.WriteLine(sDataIncomming);

                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("TCP Client");
            while (tryConnection == true)
            {
                /*Console.WriteLine("Provide IP:");
                String ip = Console.ReadLine();
                Console.WriteLine("Provide Port:");
                int port = Int32.Parse(Console.ReadLine());
                */
                try
                {
                    clientconsole client = new clientconsole("127.0.0.1", 8001);
                }
                catch
                {
                    Console.WriteLine("Server not available");
                    tryConnection = true;
                }
            }
        }
    }
}

