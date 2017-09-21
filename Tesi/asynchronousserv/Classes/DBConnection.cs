using System;
using System.Data;
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

        //opens connection
        public void OpenConn()
        {
            if (objConn.State != ConnectionState.Open)
            {
                objConn.Open();
            }
        }

        //closes connection
        public void CloseConn()
        {
            objConn.Close();
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
                    OpenConn();
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
                    CloseConn();
                }
                catch
                {
                    Console.WriteLine("Database unreachable");
                }
                return ris;
            }
        }

        //Shows available functions for a device selected by the Id passed by the client
        public string ShowDeviceFunctions(string st)
        {
            //tries to connect to the DB 
            using (objConn)
            {
                Console.WriteLine("Accessing DB...");
                string ris = null;
                try
                {
                    OpenConn();
                    string strSQL = "SELECT DISTINCT f.* FROM Funzioni f JOIN Funzioni_ammissibili fa ON fa.id_funzione = f.id JOIN Tipi_dispositivi td ON fa.id_tipo_dispositivo = td.id JOIN Dispositivi d ON d.tipo = td.id WHERE d.id = @stringaid";
                    SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                    //create a parameter
                    SqlParameter sid = objCmd.Parameters.Add("@stringaid", System.Data.SqlDbType.NVarChar, 15);
                    //assign a value to that
                    sid.Value = st;
                    //execute SQL
                    SqlDataReader objDR = objCmd.ExecuteReader();
                    //if the SQL has results
                    if (objDR.HasRows)
                    {
                        //take all rows
                        while (objDR.Read())
                        {
                            ris = ris + Convert.ToString(objDR["id"]) + " (" + Convert.ToString(objDR["descrizione"]) + "), ";
                        }

                        //delete unuseful last ", "
                        ris = ris.Substring(0, ris.Length - 2);
                    }
                    else
                    {
                        ris = "No result";
                        Console.WriteLine("No result");
                    }

                    Console.WriteLine("Query result: " + ris);
                    //close connection and result object
                    objDR.Close();
                    CloseConn();
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
