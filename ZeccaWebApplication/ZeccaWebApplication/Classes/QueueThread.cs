using System;
using System.Collections.Concurrent;
using System.Threading;

namespace ZeccaWebAPI
{
    class QueueThread
    {
        //used to wake or sleep the thread
        private AutoResetEvent wh = new AutoResetEvent(false);
        //contains all the requests to the device
        private BlockingCollection<Request_ThreadCollection> threadQueue = new BlockingCollection<Request_ThreadCollection>();
        //true if a thread is assigned
        private bool hasThread = false;

        public BlockingCollection<Request_ThreadCollection> ThreadQueue
        {
            get
            {
                return threadQueue;
            }

            set
            {
                threadQueue = value;
            }
        }

        public AutoResetEvent Wh
        {
            get
            {
                return wh;
            }

            set
            {
                wh = value;
            }
        }

        public bool HasThread
        {
            get
            {
                return hasThread;
            }

            set
            {
                hasThread = value;
            }
        }

        //function used by the workerThread to take all the requests in the threadQueue and run them one by one
        public void ExecuteQueueThreads()
        {
            bool goOn = true;
            //while the queue has elements
            while (threadQueue.Count >= 0)
            {
                //if empty, wait for other requests for a while
                if (threadQueue.Count == 0)
                {
                    //wait until requests are put in queue. Woken up by the Set() method in asynchronousserver
                    if (Wh.WaitOne(3000))
                    {
                        if (threadQueue.Count > 0)
                        {
                            goOn = true;
                        }
                        else
                        {
                            goOn = false;
                        }
                    }
                    //if no request are made during the wait, close the thread because unused and free resources
                    else
                    {
                        goOn = false;
                    }
                }
                //if requests in queue
                if (goOn == true)
                {
                    //extract a request (thread) from the queue
                    Request_ThreadCollection t = threadQueue.Take();
                    t.RequestThread.Wh.Set();
                    //join main thread when finished
                    t.Thread.Join();
                }
                //if no request or empty queue
                else if (goOn == false)
                {
                    //tell the queue has no thread
                    HasThread = false;
                    Console.WriteLine("Thread ended");
                    //end gracefully the thread
                    return;
                }
            }
        }
    }
}