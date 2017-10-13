using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    class ReturnManager
    {
        //0: ok, 1:no result, 2:no device found, 3:no function available, 100: database unreachable
        public string AnalyzeErrMsgObj(ErrMsgObj emo)
        {
            string ris = null;
            if(emo.ErrCode== ENUM.ERRORS.NO_ERRORS)
            {
                ris = emo.Data;
            }
            else if(emo.ErrCode== ENUM.ERRORS.DB_NO_RESULT)
            {
                ris = "No result";
            }
            else if (emo.ErrCode == ENUM.ERRORS.DEVICE_NOT_FOUND)
            {
                ris = "Device not found";
            }
            else if (emo.ErrCode == ENUM.ERRORS.DEVICE_FUNCTIONALITY_NOT_SUPPORTED)
            {
                ris = "Functionality not supported";
            }
            else if (emo.ErrCode == ENUM.ERRORS.DB_UNREACHABLE)
            {
                ris = "Database unreachable";
            }

            return ris;
        }
    }
}
