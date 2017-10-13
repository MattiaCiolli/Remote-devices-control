//State pattern implementation
//a state represents a command. When a command is typed the class State will behave differently according to that command

namespace clientconsole
{
    // The 'State' abstract class
    abstract class StateClient
    {
        public abstract ErrMsgObjClient HandleCmd(string command, string cmdconsole);
    }

    // A 'ConcreteState' class for dinfo command
    class dinfo : StateClient
    {
        public override ErrMsgObjClient HandleCmd(string command, string cmdconsole)
        {
            ENUM.ERRORS errcode = ENUM.ERRORS.NO_ERRORS;
            ENUM.ACTIONS actionid = ENUM.ACTIONS.NO_ACTION;
            string id = null;
            string data = null;
            try
            {
                //extract the deviceid after the command and trim spaces
                id = command.Substring(command.IndexOf(cmdconsole) + cmdconsole.Length).Trim();
                actionid = ENUM.ACTIONS.DEVICE_INFO;
                if (id.Length == 0)
                {
                    actionid = ENUM.ACTIONS.NO_ACTION;
                    errcode = ENUM.ERRORS.SYNTAX_ERROR;
                    data = "dinfo [deviceid]";
                }
            }
            catch
            {
                actionid = ENUM.ACTIONS.NO_ACTION;
                errcode = ENUM.ERRORS.SYNTAX_ERROR;
                data = "dinfo [deviceid]";
            }
            return new ErrMsgObjClient(errcode, actionid, id, data);
        }
    }

    // A 'ConcreteState' class for dfunc command
    class dfunc : StateClient
    {
        public override ErrMsgObjClient HandleCmd(string command, string cmdconsole)
        {
            ENUM.ERRORS errcode = ENUM.ERRORS.NO_ERRORS;
            ENUM.ACTIONS actionid = ENUM.ACTIONS.NO_ACTION;
            string id = null;
            string data = null;
            try
            {
                //extract the deviceid after the command and trim spaces
                id = command.Substring(command.IndexOf(cmdconsole) + cmdconsole.Length).Trim();
                actionid = ENUM.ACTIONS.DEVICE_FUNCTIONS;
                if (id.Length == 0)
                {
                    actionid = ENUM.ACTIONS.NO_ACTION;
                    errcode = ENUM.ERRORS.SYNTAX_ERROR;
                    data = "dfunc [deviceid]";
                }
            }
            catch
            {
                actionid = ENUM.ACTIONS.NO_ACTION;
                errcode = ENUM.ERRORS.SYNTAX_ERROR;
                data = "dfunc [deviceid]";
            }
            return new ErrMsgObjClient(errcode, actionid, id, data);
        }
    }

    // A 'ConcreteState' class for check command
    class check : StateClient
    {
        public override ErrMsgObjClient HandleCmd(string command, string cmdconsole)
        {
            ENUM.ERRORS errcode = ENUM.ERRORS.NO_ERRORS;
            ENUM.ACTIONS actionid = ENUM.ACTIONS.NO_ACTION;
            string id = null;
            string data = null;
            string checkId = null;
            try
            {
                //extract the checkid after the command and trim spaces
                checkId = command.Substring(command.IndexOf(cmdconsole) + cmdconsole.Length, 2).Trim();

                if (checkId.Equals("t"))
                {
                    actionid = ENUM.ACTIONS.CHECK_TEMPERATURE;
                }
                else if (checkId.Equals("n"))
                {
                    actionid = ENUM.ACTIONS.CHECK_NODES;
                }
                else if (checkId.Equals("h"))
                {
                    actionid = ENUM.ACTIONS.CHECK_TIME;
                }
                else if (checkId.Equals("r"))
                {
                    actionid = ENUM.ACTIONS.CHECK_REACHABILITY;
                }
                else
                {
                    actionid = ENUM.ACTIONS.NO_ACTION;
                    errcode = ENUM.ERRORS.SYNTAX_ERROR;
                    data = "check [*] [deviceid]. Type \"help\" for available parameters.";
                }

                //extract the deviceid after the command and trim spaces
                id = command.Substring(command.IndexOf(cmdconsole) + cmdconsole.Length + 2).Trim();
                if (id.Length == 0)
                {
                    actionid = ENUM.ACTIONS.NO_ACTION;
                    errcode = ENUM.ERRORS.SYNTAX_ERROR;
                    data = "check [*] [deviceid]";
                }
            }
            catch
            {
                actionid = ENUM.ACTIONS.NO_ACTION;
                errcode = ENUM.ERRORS.SYNTAX_ERROR;
                data = "check [*] [deviceid]";
            }

            return new ErrMsgObjClient(errcode, actionid, id, data);
        }
    }

    // The 'Context' class
    class Command
    {
        private StateClient state;
        private string command;
        private string cmdconsole;
        // Constructor
        public Command(StateClient state, string stringa, string stringa2)
        {
            this.State = state;
            this.command = stringa;
            this.cmdconsole = stringa2;
        }

        // Gets or sets the state
        public StateClient State
        {
            get { return state; }
            set { state = value; }
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
            return state.HandleCmd(command, cmdconsole);
        }
    }
}

