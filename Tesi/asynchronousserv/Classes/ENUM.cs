using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    public static class ENUM
    {
        public enum DEVICES
        {
            UNDEFINED = 0,
            IP = 1,
            SERIAL232_TYPE1 = 2,
            SERIAL485_TYPE2 = 3
        }

        public enum ERRORS
        {
            NO_ERRORS = 0,
            DB_NO_RESULT = 1,
            DEVICE_NOT_FOUND = 2,
            DEVICE_FUNCTIONALITY_NOT_SUPPORTED = 3,
            DB_UNREACHABLE = 100
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
