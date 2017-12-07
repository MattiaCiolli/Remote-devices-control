using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ZeccaWebAPI.Models;

namespace ZeccaWebAPI
{

    public class DBConnection
    {
        private string strConnectionString = "Server=(local);Database=asd;Trusted_Connection=True;";
        private SqlConnection objConn;
        private asdEntities3 db = new asdEntities3();

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

        public DbSet<Dispositivi> GetAllDevices()
        {
            return db.Dispositivi;
        }

        public Dispositivi FindDeviceById(string id)
        {
            Dispositivi dispositivo = db.Dispositivi.Find(id);
            return dispositivo;
        }

        //Shows available functions for a device selected by the Id passed by the client
        public List<Function> SelectDeviceFunctions(string id_in)
        {
            List<Function> functions = new List<Function>();
            try
            {
                //creates a new connection
                NewConnObj();
                //opens it
                OpenConn();
                string strSQL = "SELECT DISTINCT f.id AS id, f.descrizione AS descrizione FROM Funzioni f JOIN Funzioni_ammissibili fa ON fa.id_funzione = f.id JOIN Tipi_dispositivi td ON fa.id_tipo_dispositivo = td.id JOIN Dispositivi d ON d.tipo = td.id WHERE d.id = @stringaid";
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
                        Function f = new Function();
                        f.id = (int)objDR["id"];
                        f.descrizione = (string)objDR["descrizione"];
                        functions.Add(f);
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

            return functions;
        }

    }
}