using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientconsole
{
    public class ErrMsgObjClient
    {
        private int errCode;
        private int actionid;
        private string id;
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

        public int Actionid
        {
            get
            {
                return actionid;
            }

            set
            {
                actionid = value;
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

        public string Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public ErrMsgObjClient(int e, int a, string i, string s)
        {
            errCode = e;
            actionid = a;
            id = i;
            data = s;
        }
    }
}
