using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    public class DBConnection
    {
        private string strConnectionString;
        private SqlConnection objConn;

        public DBConnection(string str)
        {
            /*db connection*/
            string strConnectionString = str;
            objConn = new SqlConnection(strConnectionString);
        }

        // This method that will be called when the thread is started
        public string SelectDeviceById(string st)
        {           
            using (objConn)
            {
                Console.WriteLine("Accessing DB...");
                objConn.Open();
                string strSQL = "SELECT * FROM Dispositivi WHERE id = @stringaid";
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                SqlParameter sid = objCmd.Parameters.Add("@stringaid", System.Data.SqlDbType.NVarChar, 15);
                sid.Value = st;
                SqlDataReader objDR = objCmd.ExecuteReader();
                string ris = null;
                if (objDR.Read())
                {
                    ris = (string)objDR["id"] + ", "+ (string)objDR["descrizione"] + ", " + (int)objDR["tipo"];
                    Console.WriteLine("Query result: " + ris);
                }
                else
                {
                    ris = "No result";
                    Console.WriteLine("No result");
                }               
                objDR.Close();
                objConn.Close();
                return ris;
            }
        }
    }
}
