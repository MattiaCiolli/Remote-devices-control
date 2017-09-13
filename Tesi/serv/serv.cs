using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data.SqlClient;
using System.Threading;

public class ThreadWithState
{
    // State information used in the task.
    private string st;
    private Socket soc;

    // The constructor obtains the state information.
    public ThreadWithState(string text, Socket s)
    {
        st = text;
        soc = s;
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
            if (objDR.Read())
            {
                string id = (string)objDR[0];
                string desc = (string)objDR["descrizione"];
                int tipo = (int)objDR["tipo"];
                Console.WriteLine("Query result: " + id + "," + desc + "," + tipo);
                ASCIIEncoding asen1 = new ASCIIEncoding();
                soc.Send(asen1.GetBytes("Infos sent to the client: " + id + "," + desc + "," + tipo));
            }
            objDR.Close();
            objConn.Close();
        }     
    }
}


public class serv
{
    public static void Main()
    {
        try
        {
            IPAddress ipAd = IPAddress.Parse("127.0.0.1");
            // use local m/c IP address, and 
            // use the same in the client

            /* Initializes the Listener */
            TcpListener myList = new TcpListener(ipAd, 8001);

            /* Start Listeneting at the specified port */
            myList.Start();

            Console.WriteLine("The server is running at port 8001...");
            Console.WriteLine("The local End point is  :" +
                              myList.LocalEndpoint);
            Console.WriteLine("Waiting for a connection.....");

            Socket s = myList.AcceptSocket();
            Console.WriteLine("Connection accepted from " + s.RemoteEndPoint);

            byte[] b = new byte[100];
            int k = s.Receive(b);
            Console.WriteLine("Recieved: ");
            string st = "";
            for (int i = 0; i < k; i++)
            {
                Console.Write(Convert.ToChar(b[i]));
                st += Convert.ToChar(b[i]);
            }
            ASCIIEncoding asen = new ASCIIEncoding();
            s.Send(asen.GetBytes("The string was recieved by the server:" + st.ToLower()));
            Console.WriteLine("\nSent Acknowledgement");

            ThreadWithState tws = new ThreadWithState(st, s);

            // Create the thread object
            Thread oThread = new Thread(new ThreadStart(tws.Query));
            // Start the thread
            oThread.Start();
            oThread.Join();
            /* clean up */
            s.Close();
            myList.Stop();

        }
        catch (Exception e)
        {
            Console.WriteLine("Error..... " + e.StackTrace);
        }
    }

}
