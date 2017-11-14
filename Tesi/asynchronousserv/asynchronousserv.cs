using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        //this data structure allows to assign a queue of requests to each device in order to fullfill them one by one when
        // all to the same device. Moreover more requests to multiple devices are fullfilled in the same time.
        private static ConcurrentDictionary<string, QueueThread> threadMap = new ConcurrentDictionary<string, QueueThread>();

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
            serverSocket.BeginAcceptTcpClient(new AsyncCallback(OnClientConnected), null);
        }

        //callback function launched when a client connects
        private static void OnClientConnected(IAsyncResult asyncResult_in)
        {
            try
            {
                TcpClient clientSocket = serverSocket.EndAcceptTcpClient(asyncResult_in);//must be invoked after BeginAcceptTcpClient
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

        //handles the client's requests. Creates a thread when a client executes a request and destroys it after the request is satisfied
        private static async void HandleClientRequest(TcpClient clientSocket_in)
        {
            //sets two streams
            StreamReader sReader = new StreamReader(clientSocket_in.GetStream(), Encoding.ASCII);
            StreamWriter sWriter = new StreamWriter(clientSocket_in.GetStream(), Encoding.ASCII);
            String sData = null;

            //while the client is connected
            while (clientSocket_in.Client.Connected)
            {
                try
                {
                    //reads from stream
                    //the await operator is part of the async-await pattern in order to make a function asynchronous
                    //in this way the server can accept clients and execute multiple actions without having to wait an input by the client
                    sData = await sReader.ReadLineAsync();

                    if (sData.Equals("close"))
                    {
                        Console.WriteLine("Client " + clientSocket_in.Client.RemoteEndPoint.ToString() + " disconnecting");
                        clientSocket_in.GetStream().Close();
                        clientSocket_in.Close();
                    }
                    else
                    {
                        //parse the client's request
                        ParserReturn Pr = (ParserReturn)Par.ParseClientRequest(sData);
                        //shows content on the console
                        Console.WriteLine("Client " + clientSocket_in.Client.RemoteEndPoint.ToString() + ": " + sData);
                        //if valid request
                        if (Pr.ActionId != ENUM.ACTIONS.NO_ACTION && Pr.DevId != null && Pr.Data != null)
                        {
                            //create a thread with parameters wich represents a request
                            RequestThread tws = new RequestThread(Pr.ActionId, Pr.DevId, Pr.Data, clientSocket_in);
                            //set the thread's entry
                            Thread oThread = new Thread(new ThreadStart(tws.DeviceAction));
                            //if simple DB action execute it immediately
                            if (Pr.ActionId == ENUM.ACTIONS.DEVICE_INFO || Pr.ActionId == ENUM.ACTIONS.DEVICE_FUNCTIONS)
                            {
                                //start the thread
                                oThread.Start();
                                //join main thread when finished
                                oThread.Join();
                            }
                            //else put it in a queue for the selected device
                            else
                            {
                                //if thread ended because unused for a long time
                                if (threadMap.ContainsKey(Pr.DevId) == true && threadMap[Pr.DevId].HasThread == false)
                                {
                                    //remove <key,value> from dictionary
                                    QueueThread qt;
                                    threadMap.TryRemove(Pr.DevId, out qt);
                                }
                                //not first request to a device and thread running. Means that there's already a couple, so just add the request to the queue
                                if (threadMap.ContainsKey(Pr.DevId) == true && threadMap[Pr.DevId].HasThread == true)
                                {
                                    //add a request in the queue
                                    threadMap[Pr.DevId].ThreadQueue.Add(oThread);
                                    //wake up the device's thread if sleeping
                                    threadMap[Pr.DevId].Wh.Set();
                                    sWriter.WriteLine("Resource may be busy, please wait");
                                    sWriter.Flush();
                                }
                                //if first request to device
                                if (threadMap.ContainsKey(Pr.DevId) == false)
                                {
                                    //create a couple (idDevice, requestsQueue)
                                    threadMap.AddOrUpdate(Pr.DevId, new QueueThread(), (key, oldValue) => threadMap[Pr.DevId]);
                                    //add a request in the queue
                                    threadMap[Pr.DevId].ThreadQueue.Add(oThread);
                                    //create a thread to fullfill all the requests to a device
                                    Thread workerThread = new Thread(new ThreadStart(threadMap[Pr.DevId].ExecuteQueueThreads));
                                    //tell it has a thread
                                    threadMap[Pr.DevId].HasThread = true;
                                    //start it
                                    workerThread.Start();
                                }
                            }

                        }
                        else
                        {
                            Console.WriteLine("Client " + clientSocket_in.Client.RemoteEndPoint.ToString() + " sent wrong data");
                            sWriter.WriteLine("Infos from server: wrong data from client");
                            sWriter.Flush();
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Client " + clientSocket_in.Client.RemoteEndPoint.ToString() + " not reachable");
                }

            }

            if (!clientSocket_in.Client.Connected)
            {
                Console.WriteLine("Client disconnected");
            }
        }
    }

}
