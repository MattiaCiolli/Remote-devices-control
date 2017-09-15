using System;
using System.Data.SqlClient;

namespace asynchronousserv
{
    public class DBConnection
    {
        private string strConnectionString;
        private SqlConnection objConn;

        public DBConnection(string str)
        {
            string strConnectionString = str;
            //generates the connection object
            objConn = new SqlConnection(strConnectionString);
        }

        //Selects a device by the Id passed by the client
        public string SelectDeviceById(string st)
        {
            //tries to connect to the DB 
            using (objConn)
            {
                Console.WriteLine("Accessing DB...");
                string ris = null;
                try
                {
                    objConn.Open();
                    string strSQL = "SELECT * FROM Dispositivi WHERE id = @stringaid";
                    SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                    //create a parameter
                    SqlParameter sid = objCmd.Parameters.Add("@stringaid", System.Data.SqlDbType.NVarChar, 15);
                    //assign a value to that
                    sid.Value = st;
                    //execute SQL
                    SqlDataReader objDR = objCmd.ExecuteReader();
                    //if the SQL has results
                    if (objDR.Read())
                    {
                        ris = (string)objDR["id"] + ", " + (string)objDR["descrizione"] + ", " + (int)objDR["tipo"];
                        Console.WriteLine("Query result: " + ris);
                    }
                    else
                    {
                        ris = "No result";
                        Console.WriteLine("No result");
                    }
                    //close connection and result object
                    objDR.Close();
                    objConn.Close();
                }
                catch
                {
                    Console.WriteLine("Database unreachable");
                }
                return ris;
            }
        }
    }
}
