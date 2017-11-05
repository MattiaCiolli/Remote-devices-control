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

        public ThreadWithState(ENUM.ACTIONS act_in, string text_in, string d_in, TcpClient cl_in)
        {
            action = act_in;
            id = text_in;
            data = d_in;
            client = cl_in;
        }

        public void DeviceAction()
        {
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);

            if (action != ENUM.ACTIONS.NO_ACTION)
            {
                sWriter.WriteLine("Checking " + id + "...");
                sWriter.Flush();
            }

            //create an action
            Action a = new Action(id, action);
            //and launch it saving its result in a string
            string ris = a.Request();

            if (ris != null)
            {
                Console.WriteLine(ris);
                sWriter.WriteLine("Infos from server: " + ris);
                sWriter.Flush();
            }
            else
            {
                Console.WriteLine("Error");
                sWriter.WriteLine("Error");
                sWriter.Flush();
            }
        }
    }
}
