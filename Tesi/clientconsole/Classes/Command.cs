// The 'Context' class, when created selects the correct strategy for the received command
namespace clientconsole
{
// The 'Context' class
    class Command
    {
        private CommandStrategy strategy;
        private string command;
        private string cmdconsole;
        // Constructor
        public Command(string command_in, string cmdconsole_in)
        {
            //command dinfo
            if (command_in.Contains("dinfo "))
            {
                this.Strategy = new dinfoStrategy();
            }
            //command dfunc
            else if (command_in.Contains("dfunc "))
            {
                this.Strategy = new dfuncStrategy();
            }
            //command check
            else if (command_in.Contains("check "))
            {
                this.Strategy = new checkStrategy();
            }

            this.command = command_in;
            this.cmdconsole = cmdconsole_in;
        }

        // Gets or sets the strategy
        public CommandStrategy Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

        // Gets or sets the command
        public string Command_
        {
            get { return command; }
            set { command = value; }
        }

        // Gets or sets the cmdconsole
        public string Cmdconsole
        {
            get { return cmdconsole; }
            set { cmdconsole = value; }
        }

        public ErrMsgObjClient Request()
        {
            return strategy.HandleCmd(command, cmdconsole);
        }
    }
}

