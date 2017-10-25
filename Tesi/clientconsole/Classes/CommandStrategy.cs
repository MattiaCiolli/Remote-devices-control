//Strategy pattern implementation
//a strategy represents the way a command is handled. When a command is typed, the class CommandStrategy will behave differently according to that command, using one of its subclasses

namespace clientconsole
{
    // The 'Strategy' abstract class
    abstract class CommandStrategy
    {
        public abstract ErrMsgObjClient HandleCmd(string command_in, string cmdconsole_in);
    }

    // A class for dinfo command
    class dinfoStrategy : CommandStrategy
    {
        public override ErrMsgObjClient HandleCmd(string command_in, string cmdconsole_in)
        {
            ENUM.ERRORS errcode = ENUM.ERRORS.NO_ERRORS;
            ENUM.ACTIONS actionid = ENUM.ACTIONS.NO_ACTION;
            string id = null;
            string data = null;
            try
            {
                //extract the deviceid after the command and trim spaces
                id = command_in.Substring(command_in.IndexOf(cmdconsole_in) + cmdconsole_in.Length).Trim();
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

    // A class for dfunc command
    class dfuncStrategy : CommandStrategy
    {
        public override ErrMsgObjClient HandleCmd(string command_in, string cmdconsole_in)
        {
            ENUM.ERRORS errcode = ENUM.ERRORS.NO_ERRORS;
            ENUM.ACTIONS actionid = ENUM.ACTIONS.NO_ACTION;
            string id = null;
            string data = null;
            try
            {
                //extract the deviceid after the command and trim spaces
                id = command_in.Substring(command_in.IndexOf(cmdconsole_in) + cmdconsole_in.Length).Trim();
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

    // A class for check command
    class checkStrategy : CommandStrategy
    {
        public override ErrMsgObjClient HandleCmd(string command_in, string cmdconsole_in)
        {
            ENUM.ERRORS errcode = ENUM.ERRORS.NO_ERRORS;
            ENUM.ACTIONS actionid = ENUM.ACTIONS.NO_ACTION;
            string id = null;
            string data = null;
            string checkId = null;
            try
            {
                //extract the checkid after the command and trim spaces
                checkId = command_in.Substring(command_in.IndexOf(cmdconsole_in) + cmdconsole_in.Length, 2).Trim();

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
                id = command_in.Substring(command_in.IndexOf(cmdconsole_in) + cmdconsole_in.Length + 2).Trim();
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
}