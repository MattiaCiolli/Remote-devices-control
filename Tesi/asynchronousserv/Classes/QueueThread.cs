using System;
using System.Collections.Concurrent;
using System.Threading;

namespace asynchronousserv
{
    class QueueThread
    {
        //used to wake or sleep the thread
        private AutoResetEvent wh = new AutoResetEvent(false);
        //contains all the requests to the device
        private BlockingCollection<Thread> threadQueue = new BlockingCollection<Thread>();
        //true if a thread is assigned
        private bool hasThread = false;

        public BlockingCollection<Thread> ThreadQueue
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
                //if empty, wait for other requests for while
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
                        //tell the device has no thread
                        goOn = false;
                    }
                }
                //if requests in queue
                if (goOn == true)
                {
                    //extract a request (thread) from the queue
                    Thread t = threadQueue.Take();
                    //start the thread
                    t.Start();
                    //join main thread when finished
                    t.Join();
                }
                //if no request or empty queue
                else if (goOn == false)
                {
                    //tell the key has no thread
                    HasThread = false;
                    Console.WriteLine("Thread ended");
                    //end gracefully the thread
                    return;
                }
            }
        }
    }
}