using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

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

        //asynchronously waits for clients without using extra threads
        private static void WaitForClients()
        {
            serverSocket.BeginAcceptTcpClient(new System.AsyncCallback(OnClientConnected), null);
        }

        //callback function launched when a client connects
        private static void OnClientConnected(IAsyncResult asyncResult)
        {
            try
            {
                TcpClient clientSocket = serverSocket.EndAcceptTcpClient(asyncResult);//must be invoked after BeginAcceptTcpClient
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

        //handles the client's requests. Creates a thread when a client executes a request and destroys it after the request is satisfied
        private static async void HandleClientRequest(TcpClient clientSocket)
        {
            //sets two streams
            StreamReader sReader = new StreamReader(clientSocket.GetStream(), Encoding.ASCII);
            String sData = null;

            //while the client is connected
            while (clientSocket.Client.Connected)
            {
                try
                {
                    //reads from stream
                    //the await operator is part of the async-await pattern in order to make a function asynchronous
                    //in this way the server can accept clients and execute multiple actions without having to wait an input by the client
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
                        //create a thread with parameters
                        ThreadWithState tws = new ThreadWithState(Pr.ActionId, Pr.ObjId, clientSocket);
                        //set the thread's entry
                        Thread oThread = new Thread(new ThreadStart(tws.DBAction));
                        //start the thread
                        oThread.Start();
                        //join main thread when finished
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
