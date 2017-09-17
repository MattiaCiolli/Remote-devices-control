using System;
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
        private Interpreter intp;

        public clientconsole(String ipAddress, int portNum)
        {
            client = new TcpClient();
            client.Connect(ipAddress, portNum);
            //stream to write on server's console
            sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            //stream to read server's messages
            sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
            //initializes interpreter
            intp = new Interpreter();
            HandleCommunication();
        }

        //handles communication with the server
        public void HandleCommunication()
        {       
            tryConnection = false;
            isConnected = true;
            String sData = null;
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
            Console.WriteLine("Type \"help\" for available commands");
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

