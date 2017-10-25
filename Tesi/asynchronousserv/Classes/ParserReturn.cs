
namespace asynchronousserv
{
    //parser return object
    class ParserReturn
    {
        private ENUM.ACTIONS actionId;
        private string objId;
        private string data;

        public ParserReturn(ENUM.ACTIONS a_in, string s_in, string d_in)
        {
            actionId = a_in;
            objId = s_in;
            data = d_in;
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
