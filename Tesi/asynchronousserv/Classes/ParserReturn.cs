
namespace asynchronousserv
{
    //parser return object
    class ParserReturn
    {
        private int actionId;
        private string objId;
        private string data;

        public ParserReturn(int i, string s, string d)
        {
            actionId = i;
            objId = s;
            data = d;
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
