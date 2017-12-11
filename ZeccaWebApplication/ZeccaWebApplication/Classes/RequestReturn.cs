using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZeccaWebApplication.Classes
{
    public class RequestReturn
    {
        private string requestName;
        private string requestData;

        public RequestReturn (string requestName_in, string requestData_in)
        {
            requestName = requestName_in;
            requestData = requestData_in;
        }

        public string RequestName
        {
            get
            {
                return requestName;
            }

            set
            {
                requestName = value;
            }
        }

        public string RequestData
        {
            get
            {
                return requestData;
            }

            set
            {
                requestData = value;
            }
        }
    }
}