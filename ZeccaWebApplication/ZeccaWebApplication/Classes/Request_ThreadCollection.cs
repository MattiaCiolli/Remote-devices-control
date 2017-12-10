using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ZeccaWebAPI
{
    public class Request_ThreadCollection
    {
        private RequestThread requestThread;
        private Thread thread;

        public RequestThread RequestThread
        {
            get
            {
                return requestThread;
            }

            set
            {
                requestThread = value;
            }
        }

        public Thread Thread
        {
            get
            {
                return thread;
            }

            set
            {
                thread = value;
            }
        }
    }
}