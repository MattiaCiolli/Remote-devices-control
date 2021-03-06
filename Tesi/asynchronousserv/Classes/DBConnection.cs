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
        public ErrMsgObj SelectDeviceById(string st_in)
        {
            //creates a new connection
            NewConnObj();

            //tries to connect to the DB 
            string ris = null;
            ENUM.ERRORS ecode;
            ENUM.DEVICES devType = ENUM.DEVICES.UNDEFINED;
            string addr=null;
            try
            {
                Console.WriteLine("Accessing DB...");
                OpenConn();
                string strSQL = "SELECT id, descrizione, tipo, indirizzo FROM Dispositivi WHERE id = @stringaid";
                SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                //create a parameter
                SqlParameter sid = objCmd.Parameters.Add("@stringaid", System.Data.SqlDbType.NVarChar, 15);
                //assign a value to that
                sid.Value = st_in;
                //execute SQL
                SqlDataReader objDR = objCmd.ExecuteReader();
                //if the SQL has results
                if (objDR.Read())
                {
                    devType = (ENUM.DEVICES)objDR["tipo"];
                    addr = (string)objDR["indirizzo"];
                    ris = st_in + ", " + (string)objDR["descrizione"] + ", " + (int)devType + ", " + addr;
                    ecode = ENUM.ERRORS.NO_ERRORS;
                }
                else
                {
                    //no result code
                    ecode = ENUM.ERRORS.DB_NO_RESULT;
                }

                //close result object
                objDR.Close();
            }
            catch
            {
                //db unreachable
                ecode = ENUM.ERRORS.DB_UNREACHABLE;
            }
            finally
            {
                //close connection
                CloseConn();
            }

            return new ErrMsgObj(ecode, ris, addr, devType);
        }

        //Shows available functions for a device selected by the Id passed by the client
        public ErrMsgObj ShowDeviceFunctions(string st_in)
        {
            //tries to connect to the DB               
            string ris = null;
            ENUM.ERRORS ecode;
            ErrMsgObj emo = null;
            try
            {
                emo = SelectDeviceById(st_in);
                if (emo.ErrCode == ENUM.ERRORS.NO_ERRORS)
                {
                    Console.WriteLine("Accessing DB...");
                    //creates a new connection
                    NewConnObj();
                    //opens it
                    OpenConn();
                    string strSQL = "SELECT DISTINCT f.id, f.descrizione FROM Funzioni f JOIN Funzioni_ammissibili fa ON fa.id_funzione = f.id JOIN Tipi_dispositivi td ON fa.id_tipo_dispositivo = td.id JOIN Dispositivi d ON d.tipo = td.id WHERE d.id = @stringaid";
                    SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                    //create a parameter
                    SqlParameter sid = objCmd.Parameters.Add("@stringaid", System.Data.SqlDbType.NVarChar, 15);
                    //assign a value to that
                    sid.Value = st_in;
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
                        ecode = ENUM.ERRORS.NO_ERRORS;
                    }
                    else
                    {
                        //no result code
                        ecode = ENUM.ERRORS.DB_NO_RESULT;
                    }

                    //close result object
                    objDR.Close();
                }
                else
                {
                    //no device found code
                    ecode = ENUM.ERRORS.DEVICE_NOT_FOUND;
                }
            }
            catch
            {
                //db unreachable
                ecode = ENUM.ERRORS.DB_UNREACHABLE;
            }
            finally
            {
                //close connection
                CloseConn();
            }

            return new ErrMsgObj(ecode, ris, emo.Address, emo.DeviceType);
        }

        //check if a function is available in the device selected by the Id passed by the client
        public ErrMsgObj FindDeviceFunction(string st_in, int func_in)
        {
            //tries to connect to the DB               
            string ris = null;
            ENUM.ERRORS ecode;
            ErrMsgObj emo = null;
            try
            {
                emo = SelectDeviceById(st_in);
                if (emo.ErrCode == ENUM.ERRORS.NO_ERRORS)
                {
                    Console.WriteLine("Accessing DB...");
                    //creates a new connection
                    NewConnObj();
                    //opens it
                    OpenConn();
                    string strSQL = "SELECT DISTINCT f.id, f.descrizione FROM Funzioni f JOIN Funzioni_ammissibili fa ON fa.id_funzione = f.id JOIN Tipi_dispositivi td ON fa.id_tipo_dispositivo = td.id JOIN Dispositivi d ON d.tipo = td.id WHERE d.id = @stringaid AND f.id = @idfunc";
                    SqlCommand objCmd = new SqlCommand(strSQL, objConn);
                    //create a parameter
                    SqlParameter sid = objCmd.Parameters.Add("@stringaid", System.Data.SqlDbType.NVarChar, 15);
                    SqlParameter sid1 = objCmd.Parameters.Add("@idfunc", System.Data.SqlDbType.Int, 15);
                    //assign a value to that
                    sid.Value = st_in;
                    sid1.Value = func_in;
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
                        ecode = ENUM.ERRORS.NO_ERRORS;
                    }
                    else
                    {
                        //no functionality code
                        ecode = ENUM.ERRORS.DEVICE_FUNCTIONALITY_NOT_SUPPORTED;
                    }

                    //close result object
                    objDR.Close();
                }
                else
                {
                    //no device found code
                    ecode = ENUM.ERRORS.DEVICE_NOT_FOUND;
                }
            }
            catch
            {
                //db unreachable
                ecode = ENUM.ERRORS.DB_UNREACHABLE;
            }
            finally
            {
                //close connection
                CloseConn();
            }

            return new ErrMsgObj(ecode, ris, emo.Address, emo.DeviceType);
        }
    }
}

