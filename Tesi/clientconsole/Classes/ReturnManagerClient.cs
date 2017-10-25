using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientconsole
{
    class ReturnManagerClient
    {
        public string AnalyzeErrMsgObj(ErrMsgObjClient emoc_in)
        {
            string ris = "no error";

            if (emoc_in.ErrCode == ENUM.ERRORS.SYNTAX_ERROR)
            {
                ris = "Syntax error. Usage " + emoc_in.Data;
            }

            return ris;
        }
    }
}
