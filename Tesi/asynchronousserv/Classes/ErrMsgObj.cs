using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    //object used to get data from queries. It stores also errors and the device type and address in order to instantiate the correct device.
    public class ErrMsgObj
    {
        private int errCode;
        private string data;
        private string addr;
        private int deviceType;

        public int ErrCode
        {
            get
            {
                return errCode;
            }

            set
            {
                errCode = value;
            }
        }

        public string Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        public int DeviceType
        {
            get
            {
                return deviceType;
            }

            set
            {
                deviceType = value;
            }
        }

        public string Address
        {
            get
            {
                return addr;
            }

            set
            {
                addr = value;
            }
        }

        public ErrMsgObj(int i, string s, string a, int d)
        {
            errCode = i;
            data = s;
            deviceType = d;
            addr = a;
        }
    }
}
