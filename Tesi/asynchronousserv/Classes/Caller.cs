using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    class Caller
    {
        public string call(Device device)
        {
            //do things based on device type and actions requested
            string x = reach().ToString();
            string a=temp().ToString();
            string b=nodes();
            string c=time().ToString();
            return x+ a + b + c;
        }

        public bool reach()
        {
            return true;
        }

        public double temp()
        {
            return 12.5;
        }

        //string for test
        public string nodes()
        {
            return "ok,ok,ok";
        }

        public DateTime time()
        {
            return new DateTime();
        }

    }
}
