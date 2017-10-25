using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientconsole
{
    public class ErrMsgObjClient
    {
        private ENUM.ERRORS errCode;
        private ENUM.ACTIONS actionid;
        private string id;
        private string data;

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

        public ENUM.ACTIONS Actionid
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

        public ErrMsgObjClient(ENUM.ERRORS e_in, ENUM.ACTIONS a_in, string i_in, string s_in)
        {
            errCode = e_in;
            actionid = a_in;
            id = i_in;
            data = s_in;
        }
    }
}
