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
        public string AnalyzeErrMsgObj(ErrMsgObj emo_in)
        {
            string ris = null;
            if(emo_in.ErrCode== ENUM.ERRORS.NO_ERRORS)
            {
                ris = emo_in.Data;
            }
            else if(emo_in.ErrCode== ENUM.ERRORS.DB_NO_RESULT)
            {
                ris = "No result";
            }
            else if (emo_in.ErrCode == ENUM.ERRORS.DEVICE_NOT_FOUND)
            {
                ris = "Device not found";
            }
            else if (emo_in.ErrCode == ENUM.ERRORS.DEVICE_FUNCTIONALITY_NOT_SUPPORTED)
            {
                ris = "Functionality not supported";
            }
            else if (emo_in.ErrCode == ENUM.ERRORS.DB_UNREACHABLE)
            {
                ris = "Database unreachable";
            }
            else if (emo_in.ErrCode == ENUM.ERRORS.TCP_CONNECTION_FAILED)
            {
                ris = "Connection to remote failed";
            }
            else if (emo_in.ErrCode == ENUM.ERRORS.TCP_STREAM_READ_FAILED)
            {
                ris = "Remote data read failed";
            }

            return ris;
        }
    }
}
