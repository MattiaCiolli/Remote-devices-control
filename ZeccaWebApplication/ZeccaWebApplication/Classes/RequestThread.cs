namespace ZeccaWebAPI
{
    public class RequestThread
    {
        private ENUM.ACTIONS action;
        private string id;

        public RequestThread(ENUM.ACTIONS act_in, string id_in)
        {
            action = act_in;
            id = id_in;
        }

        public void DeviceAction()
        {
            //create an action
            Action a = new Action(id, action);
            //and launch it saving its result in a string
            string ris = a.Request();
        }
    }
}
