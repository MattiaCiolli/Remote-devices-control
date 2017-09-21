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
        private Caller caller;
        private TcpClient client;

        public ThreadWithState(int integ, string text, string d, TcpClient cl)
        {
            action = integ;
            id = text;
            data = d;
            client = cl;
        }

        public void DBAction()
        {
            //ADO.Net connection pooling. Connections are not thread safe so each thread should have its on connection but ADO.Net will deal with that
            DBConnection DbC = new DBConnection("Server=(local);Database=asd;Trusted_Connection=True;");
            caller = new Caller();
            StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
            //case 1 -> select device by id
            if (action == 1)
            {
                string ris = DbC.SelectDeviceById(id);
                if (ris.Length > 0)
                {
                    sWriter.WriteLine("Infos from server: " + ris);
                    sWriter.Flush();
                }
                else
                {
                    sWriter.WriteLine("No result");
                    sWriter.Flush();
                }
            }
            //case dinfo
            else if (action == 2)
            {
                string ris = DbC.ShowDeviceFunctions(id);
                if (ris.Length > 0)
                {
                    sWriter.WriteLine("Infos from server: " + ris);
                    sWriter.Flush();
                }
                else
                {
                    sWriter.WriteLine("No result");
                    sWriter.Flush();
                }
            }
            //case check temp
            else if (action == 3)
            {
                if (DbC.ShowDeviceFunctions(id).Contains("temperatura"))
                {
                    string ris = caller.temp().ToString();
                    sWriter.WriteLine("Infos from server: " + ris + "°");
                    sWriter.Flush();
                }
                else
                {
                    sWriter.WriteLine("Functionality not available on the device selected");
                    sWriter.Flush();
                }
            }
            //case check nodes
            else if (action == 4)
            {
                if (DbC.ShowDeviceFunctions(id).Contains("nodi"))
                {
                    string ris = caller.nodes().ToString();
                    sWriter.WriteLine("Infos from server: " + ris);
                    sWriter.Flush();
                }
                else
                {
                    sWriter.WriteLine("Functionality not available on the device selected");
                    sWriter.Flush();
                }
            }
            //case check time
            else if (action == 5)
            {
                if (DbC.ShowDeviceFunctions(id).Contains("orario"))
                {
                    string ris = caller.time().ToString();
                    sWriter.WriteLine("Infos from server: " + ris);
                    sWriter.Flush();
                }
                else
                {
                    sWriter.WriteLine("Functionality not available on the device selected");
                    sWriter.Flush();
                }
            }
            //case check reachable
            else if (action == 6)
            {
                if (DbC.ShowDeviceFunctions(id).Contains("raggiungibilita"))
                {
                    string ris = caller.reach().ToString();
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
}
