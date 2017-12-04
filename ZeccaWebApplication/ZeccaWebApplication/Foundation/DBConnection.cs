using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;

namespace ZeccaWebApplication
{

    public class DBConnection
    {
        private string strConnectionString = "Server=(local);Database=asd;Trusted_Connection=True;";
        private SqlConnection objConn;

        //creates a new connection
        public void NewConnObj()
        {
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

        //Shows available functions for a device selected by the Id passed by the client
        public Dictionary<int, string> SelectDeviceFunctions(string id_in)
        {
            Dictionary<int, string> jsonReturn = new Dictionary<int, string>();
            try
            {
                //creates a new connection
                NewConnObj();
                //opens it
                OpenConn();
                string strSQL = "SELECT DISTINCT f.id, f.descrizione FROM Funzioni f JOIN Funzioni_ammissibili fa ON fa.id_funzione = f.id JOIN Tipi_dispositivi td ON fa.id_tipo_dispositivo = td.id JOIN Dispositivi d ON d.tipo = td.id WHERE d.id = @stringaid";
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //create a parameter
                SqlParameter sid = objCmd.Parameters.Add("@stringaid", System.Data.SqlDbType.NVarChar, 15);
                //assign a value to that
                sid.Value = id_in;
                //execute SQL
                SqlDataReader objDR = objCmd.ExecuteReader();
                //if the SQL has results
                if (objDR.HasRows)
                {
                    //take all rows
                    while (objDR.Read())
                    {
                        jsonReturn.Add((int)objDR["id"], (string)objDR["descrizione"]);
                    }
                }
                else
                {

                }
                //close result object
                objDR.Close();
            }
            catch
            {

            }
            finally
            {
                //close connection
                CloseConn();
            }

            return jsonReturn;
        }

    }
}