using System.IO;
using System.Net.Sockets;
using System.Text;


namespace asynchronousserv
{
    public class ThreadWithState
    {
        private int integer;
        private string st;
        private TcpClient client;

        public ThreadWithState(int integ, string text, TcpClient cl)
        {
            integer = integ;
            st = text;
            client = cl;
        }

        public void DBAction()
        {
            //ADO.Net connection pooling. Connections are not thread safe so each thread should have its on connection but ADO.Net will deal with that
            DBConnection DbC = new DBConnection("Server=(local);Database=asd;Trusted_Connection=True;");

            //case 1
            if (integer == 1)
            {
                string ris = DbC.SelectDeviceById(st);
                StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
                if (ris.Length > 0)
                {
                    sWriter.WriteLine("Infos sent to the client: " + ris);
                    sWriter.Flush();
                }
                else
                {
                    sWriter.WriteLine("No result");
                    sWriter.Flush();
                }
            }
        }
    }
}
