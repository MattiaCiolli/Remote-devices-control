using System;

namespace clientconsole
{
    //this class checks if a command exists and converts it in a package to be sent to the server
    public class Interpreter
    {
        private ReturnManagerClient rmc = new ReturnManagerClient();

        internal ReturnManagerClient Rmc
        {
            get
            {
                return rmc;
            }

            set
            {
                rmc = value;
            }
        }

        //converts a command to a package
        public string Command2Package(string command_in, string cmdconsole_in)
        {
            //create a Command
            Command com = new Command(command_in, cmdconsole_in);
            //and launch it saving its result 
            ErrMsgObjClient emoc = com.Request();
            //analayze result
            string ris = Rmc.AnalyzeErrMsgObj(emoc);
            //if error
            if (ris.Equals("no error"))
            {
                ris = "ABC" + (int)emoc.Actionid + "DEF" + emoc.Id + "GHI" + emoc.Data + "JKL";
            }
            else
            {
                Console.WriteLine(ris);
                ris = "error";
            }
            //return package
            return ris;
        }

        //check if a command exists
        public string CheckCommands(string command_in)
        {
            string returnstring = null;

            //help command
            if (command_in.Equals("help"))
            {
                Console.WriteLine("Available commands:");
                Console.WriteLine("help:  shows available commands");
                Console.WriteLine("dinfo [deviceid]:  shows infos about the selected device");
                Console.WriteLine("dfunc [deviceid]:  shows functions available for the selected device");
                Console.WriteLine("check [*] [deviceid]:  checks the state of the functionality selected by [*] on the selected device");
                Console.WriteLine("                       [*]: \"r\" = reachability, \"t\" = temperature, \"n\" = nodes, \"h\" = time, \"v\" = voltage");
                Console.WriteLine("close:  closes connection to server");
            }

            //dinfo command
            else if (command_in.Contains("dinfo "))
            {
                returnstring = Command2Package(command_in, "dinfo ");
            }

            //dfunc command
            else if (command_in.Contains("dfunc "))
            {
                returnstring = Command2Package(command_in, "dfunc ");
            }

            //check command
            else if (command_in.Contains("check "))
            {
                returnstring = Command2Package(command_in, "check ");
            }

            //close command
            else if (command_in.Equals("close"))
            {
                returnstring = "close";
            }

            //command doesn't exist
            else
            {
                Console.WriteLine("Unknown command. Type \"help\" for available commands or correct syntax.");
            }

            return returnstring;
        }
    }
}
