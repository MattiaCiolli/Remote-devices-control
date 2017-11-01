using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientconsole
{
    public static class ENUM
    {
        public enum ERRORS
        {
            NO_ERRORS = 0,
            SYNTAX_ERROR = 1
        }

        public enum ACTIONS
        {
            NO_ACTION = 0,
            DEVICE_INFO = 1,
            DEVICE_FUNCTIONS = 2,
            CHECK_TEMPERATURE = 3,
            CHECK_NODES = 4,
            CHECK_TIME = 5,
            CHECK_REACHABILITY = 6,
            CHECK_VOLTAGE = 7
        }
    }
}
