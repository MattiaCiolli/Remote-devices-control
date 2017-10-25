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
        private ENUM.ERRORS errCode;
        private string data;
        private string addr;
        private ENUM.DEVICES deviceType;

        public ENUM.ERRORS ErrCode
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

        public ENUM.DEVICES DeviceType
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

        public ErrMsgObj(ENUM.ERRORS i_in, string s_in, string a_in, ENUM.DEVICES d_in)
        {
            errCode = i_in;
            data = s_in;
            deviceType = d_in;
            addr = a_in;
        }
    }
}
