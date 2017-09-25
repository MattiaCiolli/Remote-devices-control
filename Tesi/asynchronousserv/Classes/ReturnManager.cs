using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    class ReturnManager
    {
        public string AnalyzeErrMsgObj(ErrMsgObj emo)
        {
            string ris = null;
            if(emo.ErrCode==0)
            {
                ris = emo.Data;
            }
            else if(emo.ErrCode==1)
            {
                ris = "No result";
            }
            else if (emo.ErrCode == 2)
            {
                ris = "Device not found";
            }
            else if (emo.ErrCode == 3)
            {
                ris = "Functionality not available";
            }
            else if (emo.ErrCode == 100)
            {
                ris = "Database unreachable";
            }

            return ris;
        }
    }
}
