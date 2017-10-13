﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientconsole
{
    class ReturnManagerClient
    {
        public string AnalyzeErrMsgObj(ErrMsgObjClient emoc)
        {
            string ris = "no error";

            if (emoc.ErrCode == ENUM.ERRORS.SYNTAX_ERROR)
            {
                ris = "Syntax error. Usage " + emoc.Data;
            }

            return ris;
        }
    }
}
