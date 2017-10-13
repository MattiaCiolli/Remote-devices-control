
namespace asynchronousserv
{
    //parser return object
    class ParserReturn
    {
        private ENUM.ACTIONS actionId;
        private string objId;
        private string data;

        public ParserReturn(ENUM.ACTIONS a, string s, string d)
        {
            actionId = a;
            objId = s;
            data = d;
        }

        public ENUM.ACTIONS ActionId
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
    }
}
