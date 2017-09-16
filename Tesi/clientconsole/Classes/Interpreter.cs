using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clientconsole
{
    public class Interpreter
    {
        public string Command2Package(string command)
        {
            return "ABC1DEFasd1GHIdataJKL";
        }

        public string CheckCommands(string command)
        {
            string returnstring = null;
            if (command.Equals("help"))
            {
                Console.WriteLine("Available commands:");
                Console.WriteLine("help:  show available commands");
                Console.WriteLine("dinfo [deviceid]:  show infos about the selected device");
                Console.WriteLine("close:  closes connection to server");
            }
            else if (command.Contains("dinfo"))
            {
                returnstring = Command2Package(command);
            }
            else if (command.Equals("close"))
            {
                returnstring = "close";
            }

            return returnstring;
        }
    }
}
