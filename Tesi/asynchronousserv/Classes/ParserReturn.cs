
namespace asynchronousserv
{
    //parser return object
    class ParserReturn
    {
        private ENUM.ACTIONS actionId;
        private string devId;
        private string data;

        public ParserReturn(ENUM.ACTIONS action_in, string id_in, string data_in)
        {
            actionId = action_in;
            devId = id_in;
            data = data_in;
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

        public string DevId
        {
            get
            {
                return devId;
            }

            set
            {
                devId = value;
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
