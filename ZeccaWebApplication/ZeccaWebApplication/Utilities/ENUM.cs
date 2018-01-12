using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZeccaWebAPI
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
            DB_UNREACHABLE = 100,
            TCP_CONNECTION_FAILED=200,
            TCP_STREAM_READ_FAILED=201,
            SERIAL_GSM_CONNECTION_FAILED = 300,
            SERIAL_GSM_READ_FAILED = 301
        }

        public enum ACTIONS
        {
            NO_ACTION = 0,
            CHECK_REACHABILITY = 1,
            CHECK_TIME = 2,
            CHECK_TEMPERATURE = 3,
            CHECK_NODES = 4,
            CHECK_VOLTAGE = 5
        }
    }
}
