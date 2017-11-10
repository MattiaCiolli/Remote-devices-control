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
        private BlockingCollection<Thread> threadQueue = new BlockingCollection<Thread>();

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

        // function used by the "worker" thread to take all the requests in the threadQueue and run them one before the other
        public void ExecuteQueueThreads()
        {
            while (threadQueue.IsCompleted == false)
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

