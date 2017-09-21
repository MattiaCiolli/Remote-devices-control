using System.IO;
using System.Net.Sockets;
using System.Text;


namespace asynchronousserv
{
    public class ThreadWithState
    {
        private int action;
        private string id;
        private string data;
        private TcpClient client;

        public ThreadWithState(int integ, string text, string d, TcpClient cl)
        {
            action = integ;
            id = text;
            data = d;
            client = cl;
        }

        public void DeviceAction()
        {
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            State state = null;
            //case dinfo
            if (action == 1)
            {
                state = new dinfo();
            }
            //case dfunc
            else if (action == 2)
            {
                state = new dfunc();
            }
            //case check temp
            else if (action == 3)
            {
                state = new checkTemp();
            }
            //case check nodes
            else if (action == 4)
            {
                state = new checkNodes();
            }
            //case check time
            else if (action == 5)
            {
                state = new checkTime();
            }
            //case check reachable
            else if (action == 6)
            {
                state = new checkReach();
            }
            
            //create an action
            Action a = new Action(state, id);
            //and launch it saving its result in a string
            string ris = a.Request();

            if (ris.Length > 0)
            {
                sWriter.WriteLine("Infos from server: " + ris);
                sWriter.Flush();
            }
            else
            {
                sWriter.WriteLine("Functionality not available on the device selected");
                sWriter.Flush();
            }
        }
    }
}
