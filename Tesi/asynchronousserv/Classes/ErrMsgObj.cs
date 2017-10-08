using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    //0: ok, 1:no result, 2:no device found, 3:no function available, 100: database unreachable
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

        public ErrMsgObj(int i, string s)
        {
            errCode = i;
            data = s;
        }
    }
}
