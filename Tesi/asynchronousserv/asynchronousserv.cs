using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace asynchronousserv
{
    class asynchronousserv
    {
        private static Parser Par;
        private static TcpListener serverSocket;
        private static string ip = "127.0.0.1";
        private static int port = 8001;

        static void Main(string[] args)
        {
            StartServer();
            Console.Read();
        }

        //initializes the server
        public static void StartServer()
        {
            IPAddress ipAd = IPAddress.Parse(ip);
            serverSocket = new TcpListener(ipAd, port);
            serverSocket.Start();
            Par = new Parser();
            Console.WriteLine("Asynchronous server socket is listening at: 8001");
            WaitForClients();
        }

        private static void WaitForClients()
        {
            serverSocket.BeginAcceptTcpClient(new System.AsyncCallback(OnClientConnected), null);
        }

        private static void OnClientConnected(IAsyncResult asyncResult)
        {
            try
            {
                TcpClient clientSocket = serverSocket.EndAcceptTcpClient(asyncResult);
                if (clientSocket != null)
                    Console.WriteLine("Received connection request from: " + clientSocket.Client.RemoteEndPoint.ToString());
                HandleClientRequest(clientSocket);
            }
            catch
            {
                throw;
            }
            WaitForClients();
        }

        /*private async void HandleConnectionAsync(TcpClient tcpClient)
        {
            //Write code here to process the incoming client connections
        }*/

        private static async void HandleClientRequest(TcpClient clientSocket)
        {
            // sets two streams
            StreamReader sReader = new StreamReader(clientSocket.GetStream(), Encoding.ASCII);
            // you could use the NetworkStream to read and write, 
            // but there is no forcing flush, even when requested

            String sData = null;

            while (clientSocket.Client.Connected)
            {
                try
                {
                    // reads from stream
                    sData = await sReader.ReadLineAsync();

                    if (sData.Equals("close"))
                    {
                        Console.WriteLine("Client " + clientSocket.Client.RemoteEndPoint.ToString() + " disconnecting");
                        clientSocket.GetStream().Close();
                        clientSocket.Close();
                    }
                    else
                    {
                        ParserReturn Pr = (ParserReturn)Par.ParseClientRequest(sData);
                        // shows content on the console.
                        Console.WriteLine("Client " + clientSocket.Client.RemoteEndPoint.ToString() + ": " + sData);
                        ThreadWithState tws = new ThreadWithState(Pr.ActionId, Pr.ObjId, clientSocket);
                        // Create the thread object
                        Thread oThread = new Thread(new ThreadStart(tws.DBAction));
                        // Start the thread
                        oThread.Start();
                        oThread.Join();
                    }
                }
                catch
                {
                    Console.WriteLine("Client " + clientSocket.Client.RemoteEndPoint.ToString() + " not reachable");
                }

            }

            if (!clientSocket.Client.Connected)
            {
                Console.WriteLine("Client disconnected");
            }
        }
    }

}
