using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

    public void HandleCommunication()
    {
        sReader = new StreamReader(client.GetStream(), Encoding.ASCII);
        sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
        tryConnection = false;
        isConnected = true;
        String sData = null;

        while (isConnected)
        {

            Console.Write("^_^: ");
            sData = Console.ReadLine();

            if (sData.Equals("close"))
            {
                isConnected = false;

                //tells the server to close the connection
                sWriter.WriteLine(sData);
                sWriter.Flush();
                client.GetStream().Close();
            }
            else
            {
                try
                {
                    // write data on servers's console and flush
                    sWriter.WriteLine(sData);
                    sWriter.Flush();

                    // receive from server
                    String sDataIncomming = sReader.ReadLine();
                    Console.WriteLine(sDataIncomming);
                }
                catch
                {
                    Console.WriteLine("Lost connection to server");
                    tryConnection = true;
                    isConnected = false;
                }

            }
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("TCP Client");
        while (tryConnection == true)
        {
            Console.WriteLine("Provide IP:");
            String ip = Console.ReadLine();
            Console.WriteLine("Provide Port:");
            int port = Int32.Parse(Console.ReadLine());
            try
            {
                clientconsole client = new clientconsole(ip, port);
            }
            catch
            {
                Console.WriteLine("Server not available");
            }
        }
    }
}

