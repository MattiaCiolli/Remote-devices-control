using System.Threading;
using ZeccaWebAPI.Controllers;

namespace ZeccaWebAPI
{
    public class RequestThread
    {
        private ENUM.ACTIONS action;
        private string id;
        //used to wake or sleep the thread
        private AutoResetEvent wh = new AutoResetEvent(false);

        public RequestThread(ENUM.ACTIONS act_in, string id_in)
        {
            action = act_in;
            id = id_in;
        }

        public AutoResetEvent Wh
        {
            get
            {
                return wh;
            }

            set
            {
                wh = value;
            }
        }

        public void DeviceAction()
        {
            //string returned by the thread
            string result = null;

            if (wh.WaitOne())
            {
                //create an action
                Action a = new Action(id, (ENUM.ACTIONS)action);
                //and launch it saving its result in the result string
                result = a.Request();
                lock (DevicesController.SharedResultLock)
                {
                    DevicesController.SharedResult = result;
                }
            }
        }
    }
}
