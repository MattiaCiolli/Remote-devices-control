using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    public class ThreadWithState
    {
        // State information used in the task.
        private string st;
        private TcpClient client;

        // The constructor obtains the state information.
        public ThreadWithState(string text, TcpClient cl)
        {
            st = text;
            client = cl;
        }

        // This method that will be called when the thread is started
        public void Query()
        {
            /*db connection*/
            string strConnectionString = "Server=(local);Database=asd;Trusted_Connection=True;";
            using (SqlConnection objConn = new SqlConnection(strConnectionString))
            {
                Console.WriteLine("Accessing DB...");
                objConn.Open();
                string strSQL = "SELECT * FROM Dispositivi WHERE id = @stringaid";
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                SqlParameter sid = objCmd.Parameters.Add("@stringaid", System.Data.SqlDbType.NVarChar, 15);
                sid.Value = st;
                SqlDataReader objDR = objCmd.ExecuteReader();
                StreamWriter sWriter = new StreamWriter(client.GetStream(), Encoding.ASCII);
                if (objDR.Read())
                {
                    string id = (string)objDR["id"];
                    string desc = (string)objDR["descrizione"];
                    int tipo = (int)objDR["tipo"];
                    Console.WriteLine("Query result: " + id + "," + desc + "," + tipo);
                    sWriter.WriteLine("Infos sent to the client: " + id + "," + desc + "," + tipo);
                    sWriter.Flush();
                }
                else
                {
                    Console.WriteLine("No result");
                    sWriter.WriteLine("No result");
                    sWriter.Flush();
                }
                objDR.Close();
                objConn.Close();
            }
        }
    }
}
