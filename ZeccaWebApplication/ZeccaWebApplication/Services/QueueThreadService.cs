using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ZeccaWebAPI.Services
{
    public class QueueThreadService
    {
        private static ConcurrentDictionary<string, QueueThread> threadMap = new ConcurrentDictionary<string, QueueThread>();

        public void handleRequest(string idDev_in, int idFunc_in)
        {
                //create a thread with parameters wich represents a request
                RequestThread tws = new RequestThread((ENUM.ACTIONS)idFunc_in, idDev_in);
                //set the thread's entry
                Thread oThread = new Thread(new ThreadStart(tws.DeviceAction));
                    //if thread ended because unused for a long time
                    if (threadMap.ContainsKey(idDev_in) == true && threadMap[idDev_in].HasThread == false)
                    {
                        //remove <key,value> from dictionary
                        QueueThread qt;
                        threadMap.TryRemove(idDev_in, out qt);
                    }
                    //not first request to a device and thread running. Means that there's already a couple, so just add the request to the queue
                    if (threadMap.ContainsKey(idDev_in) == true && threadMap[idDev_in].HasThread == true)
                    {
                        //add a request in the queue
                        threadMap[idDev_in].ThreadQueue.Add(oThread);
                        //wake up the device's thread if sleeping
                        threadMap[idDev_in].Wh.Set();
                    }
                    //if first request to device
                    if (threadMap.ContainsKey(idDev_in) == false)
                    {
                        //create a couple (idDevice, requestsQueue)
                        threadMap.AddOrUpdate(idDev_in, new QueueThread(), (key, oldValue) => threadMap[idDev_in]);
                        //add a request in the queue
                        threadMap[idDev_in].ThreadQueue.Add(oThread);
                        //create a thread to fullfill all the requests to a device
                        Thread workerThread = new Thread(new ThreadStart(threadMap[idDev_in].ExecuteQueueThreads));
                        //tell it has a thread
                        threadMap[idDev_in].HasThread = true;
                        //start it
                        workerThread.Start();
                    }
                }
            }
    }
