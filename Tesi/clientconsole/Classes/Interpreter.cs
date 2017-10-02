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
        public string Command2Package(string command, string cmdconsole)
        {
            StateClient sc = null;

            //command dinfo
            if (command.Contains("dinfo "))
            {
                sc = new dinfo();
            }
            //command dfunc
            else if (command.Contains("dfunc "))
            {
                sc = new dfunc();
            }
            //command check
            else if (command.Contains("check "))
            {
                sc = new check();
            }
            else
            {
                Console.WriteLine("Syntax error");
            }

            //create a Command
            Command c = new Command(sc, command, cmdconsole);
            //and launch it saving its result 
            ErrMsgObjClient emoc = c.Request();
            //analayze result
            string ris = Rmc.AnalyzeErrMsgObj(emoc);
            //if error
            if (ris.Equals("no error"))
            {
                ris = "ABC" + emoc.Actionid + "DEF" + emoc.Id + "GHI" + emoc.Data + "JKL";
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
        public string CheckCommands(string command)
        {
            string returnstring = null;

            //help command
            if (command.Equals("help"))
            {
                Console.WriteLine("Available commands:");
                Console.WriteLine("help:  shows available commands");
                Console.WriteLine("dinfo [deviceid]:  shows infos about the selected device");
                Console.WriteLine("dfunc [deviceid]:  shows functions available for the selected device");
                Console.WriteLine("check [*] [deviceid]:  checks the state of the functionality selected by [*] on the selected device");
                Console.WriteLine("                       [*]: \"r\" = reachability, \"t\" = temperature, \"n\" = nodes, \"h\" = time");
                Console.WriteLine("close:  closes connection to server");
            }

            //dinfo command
            else if (command.Contains("dinfo "))
            {
                returnstring = Command2Package(command, "dinfo ");
            }

            //dfunc command
            else if (command.Contains("dfunc "))
            {
                returnstring = Command2Package(command, "dfunc ");
            }

            //check command
            else if (command.Contains("check "))
            {
                returnstring = Command2Package(command, "check ");
            }

            //close command
            else if (command.Equals("close"))
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
