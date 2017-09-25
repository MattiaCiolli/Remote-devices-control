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

        public ErrMsgObj(int i, string s)
        {
            errCode = i;
            data = s;
        }
    }
}
