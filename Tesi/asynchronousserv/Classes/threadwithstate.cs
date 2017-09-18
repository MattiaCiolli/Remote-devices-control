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

        public void DBAction()
        {
            //ADO.Net connection pooling. Connections are not thread safe so each thread should have its on connection but ADO.Net will deal with that
            DBConnection DbC = new DBConnection("Server=(local);Database=asd;Trusted_Connection=True;");

            //case 1 -> select device by id
            if (action == 1)
            {
                string ris = DbC.SelectDeviceById(id);
                StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
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
                StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
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
                /*string ris = DbC.ShowDeviceFunctions(id);
                StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
                if (ris.Length > 0)
                {
                    sWriter.WriteLine("Infos from server: " + ris);
                    sWriter.Flush();
                }
                else
                {
                    sWriter.WriteLine("No result");
                    sWriter.Flush();
                }*/
            }
            //case check nodes
            else if (action == 4)
            {
                /*string ris = DbC.ShowDeviceFunctions(id);
                StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
                if (ris.Length > 0)
                {
                    sWriter.WriteLine("Infos from server: " + ris);
                    sWriter.Flush();
                }
                else
                {
                    sWriter.WriteLine("No result");
                    sWriter.Flush();
                }*/
            }
            //case check time
            else if (action == 5)
            {
                /*string ris = DbC.ShowDeviceFunctions(id);
                StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
                if (ris.Length > 0)
                {
                    sWriter.WriteLine("Infos from server: " + ris);
                    sWriter.Flush();
                }
                else
                {
                    sWriter.WriteLine("No result");
                    sWriter.Flush();
                }*/
            }
        }
    }
}
