using System;
using System.IO;
using System.Net.Sockets;
using System.Text;


namespace asynchronousserv
{
    public class ThreadWithState
    {
        private ENUM.ACTIONS action;
        private string id;
        private string data;
        private TcpClient client;

        public ThreadWithState(ENUM.ACTIONS act, string text, string d, TcpClient cl)
        {
            action = act;
            id = text;
            data = d;
            client = cl;
        }

        public void DeviceAction()
        {
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            State state = null;
            switch (action)
            {
                case ENUM.ACTIONS.DEVICE_INFO:
                    state = new dinfo();
                    break;
                case ENUM.ACTIONS.DEVICE_FUNCTIONS:
                    state = new dfunc();
                    break;
                case ENUM.ACTIONS.CHECK_TEMPERATURE:
                    state = new checkTemp();
                    break;
                case ENUM.ACTIONS.CHECK_NODES:
                    state = new checkNodes();
                    break;
                case ENUM.ACTIONS.CHECK_TIME:
                    state = new checkTime();
                    break;
                case ENUM.ACTIONS.CHECK_REACHABILITY:
                    state = new checkReach();
                    sWriter.WriteLine("Checking " + id + " reachability");
                    break;
                default:
                    break;
            }            
            
            //create an action
            Action a = new Action(state, id);
            //and launch it saving its result in a string
            string ris = a.Request();

            if (ris!=null)
            {
                Console.WriteLine(ris);
                sWriter.WriteLine("Infos from server: " + ris);
                sWriter.Flush();
            }
            else
            {
                Console.WriteLine("error");
                sWriter.WriteLine("error");
                sWriter.Flush();
            }
        }
    }
}
