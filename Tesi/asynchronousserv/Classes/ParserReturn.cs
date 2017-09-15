﻿
namespace asynchronousserv
{
    //parser return object
    class ParserReturn
    {
        private int actionId;
        private string objId;

        public ParserReturn(int i, string s)
        {
            actionId = i;
            objId = s;
        }

        public int ActionId
        {
            get
            {
                return actionId;
            }

            set
            {
                actionId = value;
            }
        }

        public string ObjId
        {
            get
            {
                return objId;
            }

            set
            {
                objId = value;
            }
        }
    }
}
