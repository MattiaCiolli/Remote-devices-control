using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace asynchronousserv
{
    class QueueThread
    {
        //used to wake or sleep the thread
        private AutoResetEvent wh = new AutoResetEvent(false);
        //contains all the requests to the device
        private BlockingCollection<Thread> threadQueue = new BlockingCollection<Thread>();

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

        // function used by the workerThread to take all the requests in the threadQueue and run them one by one
        public void ExecuteQueueThreads()
        {
            bool goOn = true;      
            //while the queue has elements
            while (threadQueue.Count >= 0)
            {
                //if empty sleep
                if (threadQueue.Count == 0)
                {
                    goOn = false;
                    //sleep until requests are put in queue. Woken up by the Set() method in asynchronousserver
                    if(Wh.WaitOne(3000))
                    {
                        goOn = true;
                    }
                    else
                    {
                        try
                        {
                            HasThread = false;
                            Thread.CurrentThread.Abort();
                        }
                        catch
                        {
                            Console.WriteLine("Thread ended");
                        }
                        
                    }
                }
                if (goOn == true)
                {
                    //extract a request (thread) from the queue
                    Thread t = threadQueue.Take();
                    //start the thread
                    t.Start();
                    //join main thread when finished
                    t.Join();
                }
            }        
        }
    }
}

