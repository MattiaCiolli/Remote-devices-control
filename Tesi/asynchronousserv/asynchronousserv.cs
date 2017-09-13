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
        static void Main(string[] args)
        {
            SocketServer.StartServer();
            Console.Read();
        }
    }

    public class SocketServer
    {
        private static TcpListener serverSocket;
        public static void StartServer()
        {
            IPAddress ipAd = IPAddress.Parse("127.0.0.1");
            serverSocket = new TcpListener(ipAd, 8001);
            serverSocket.Start();
            Console.WriteLine("Asynchonous server socket is listening at: 8001");
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

        private async void HandleConnectionAsync(TcpClient tcpClient)
        {
            //Write code here to process the incoming client connections
        }

        private static async void HandleClientRequest(TcpClient clientSocket)
        {
            // sets two streams
            StreamReader sReader = new StreamReader(clientSocket.GetStream(), Encoding.ASCII);
            // you could use the NetworkStream to read and write, 
            // but there is no forcing flush, even when requested

            String sData = null;

            while (clientSocket.Client.Connected)
            {
                // reads from stream
                sData = await sReader.ReadLineAsync();

                // shows content on the console.
                Console.WriteLine("Client " + sData);

                if (sData.Equals("close"))
                {
                    Console.WriteLine("Client " + clientSocket.Client.RemoteEndPoint.ToString() + " disconnecting");
                    clientSocket.GetStream().Close();
                    clientSocket.Close();
                }

                else
                {
                    ThreadWithState tws = new ThreadWithState(sData, clientSocket);
                    // Create the thread object
                    Thread oThread = new Thread(new ThreadStart(tws.Query));
                    // Start the thread
                    oThread.Start();
                    oThread.Join();
                }
            }

            if (!clientSocket.Client.Connected)
            {
                Console.WriteLine("Client disconnected");
            }
        }
    }

}
