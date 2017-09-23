﻿using System;
using System.Data;
using System.Data.SqlClient;

namespace asynchronousserv
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

        //Selects a device by the Id passed by the client
        public string SelectDeviceById(string st)
        {
            //creates a new connection
            NewConnObj();

            //tries to connect to the DB 
            string ris = null;
            try
            {
                Console.WriteLine("Accessing DB...");
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

                //close result object
                objDR.Close();
            }
            catch
            {
                Console.WriteLine("Database unreachable");
            }
            finally
            {
                //close connection
                CloseConn();
            }

            return ris;
        }

        //Shows available functions for a device selected by the Id passed by the client
        public string ShowDeviceFunctions(string st)
        {
            //tries to connect to the DB               
            string ris = null;
            try
            {
                if (SelectDeviceById(st) != null)
                {
                    Console.WriteLine("Accessing DB...");
                    //creates a new connection
                    NewConnObj();
                    //opens it
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
                    //close result object
                    objDR.Close();
                }
                else
                {
                    ris = "No device found";
                    Console.WriteLine("No device found");
                }
            }
            catch
            {
                Console.WriteLine("Database unreachable");
            }
            finally
            {
                //close connection
                CloseConn();
            }

            return ris;
        }
    }
}
