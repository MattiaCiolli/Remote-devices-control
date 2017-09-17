using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientconsole
{
    //this class checks if a command exists and converts it in a package to be sent to the server
    public class Interpreter
    {
        //converts a command to a package
        public string Command2Package(string command, string cmdconsole)
        {
            int actionid = 0;
            string id = null;
            string data = null;

            //command dinfo
            if (command.Contains("dinfo "))
            {
                actionid = 1;
                try
                {
                    //extract the deviceid after the command and trim spaces
                    id = command.Substring(command.IndexOf(cmdconsole) + cmdconsole.Length).Trim();
                }
                catch
                {
                    Console.WriteLine("Syntax error. Usage dinfo [deviceid]");
                }
            }
            //command dinfo
            else if (command.Contains("dfunc "))
            {
                actionid = 2;
                try
                {
                    //extract the deviceid after the command and trim spaces
                    id = command.Substring(command.IndexOf(cmdconsole) + cmdconsole.Length).Trim();
                }
                catch
                {
                    Console.WriteLine("Syntax error. Usage dfunc [deviceid]");
                }
            }
            else
            {
                Console.WriteLine("Syntax error");
            }

            //return package
            return "ABC" + actionid + "DEF" + id + "GHI" + data + "JKL";
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
                Console.WriteLine("close:  closes connection to server");
            }

            //dinfo command
            else if (command.Contains("dinfo"))
            {
                returnstring = Command2Package(command, "dinfo ");
            }

            //dfunc command
            else if (command.Contains("dfunc"))
            {
                returnstring = Command2Package(command, "dfunc ");
            }

            //close command
            else if (command.Equals("close"))
            {
                returnstring = "close";
            }

            //command doesn't exist
            else
            {
                Console.WriteLine("Unknown command. Type \"help\" for available commands");
            }

            return returnstring;
        }
    }
}
