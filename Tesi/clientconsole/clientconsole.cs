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

        isConnected = true;
        String sData = null;

        while (isConnected)
        {
            Console.Write("&gt; ");
            sData = Console.ReadLine();

            if (sData.Equals("close"))
            {
                isConnected = false;
                //tells the server to close connection
                sWriter.WriteLine(sData);
                sWriter.Flush();
                client.GetStream().Close();
            }
            else
            {
                // write data and make sure to flush, or the buffer will continue to 
                // grow, and your data might not be sent when you want it, and will
                // only be sent once the buffer is filled.
                sWriter.WriteLine(sData);
                sWriter.Flush();

                // if you want to receive anything
                String sDataIncomming = sReader.ReadLine();
                Console.Write(sDataIncomming + "\n");
            }
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("TCP Client");
        /*Console.WriteLine("Provide IP:");
        String ip = Console.ReadLine();

        Console.WriteLine("Provide Port:");
        int port = Int32.Parse(Console.ReadLine());
        */
        clientconsole client = new clientconsole("127.0.0.1", 8001);
    }
}

