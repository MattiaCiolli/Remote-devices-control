//State pattern implementation
//a state represents an action. When an action is requested the class State will behave differently according to that action

namespace asynchronousserv
{
    // The 'State' abstract class
    abstract class State
    {
        //ADO.Net connection pooling. Connections are not thread safe so each thread should have its on connection but ADO.Net will deal with that
        private DBConnection DbC = new DBConnection();
        private Caller caller = new Caller();
        private ReturnManager rm = new ReturnManager();

        public DBConnection DBC
        {
            get
            {
                return DbC;
            }

            set
            {
                DbC = value;
            }
        }

        public ReturnManager Rm
        {
            get
            {
                return rm;
            }

            set
            {
                rm = value;
            }
        }

        public Caller Caller
        {
            get
            {
                return caller;
            }

            set
            {
                caller = value;
            }
        }

        public abstract string HandleCmd(string id);
    }

    // A 'ConcreteState' class for dinfo command
    class dinfo : State
    {
        public override string HandleCmd(string id)
        {
            ErrMsgObj emo = DBC.SelectDeviceById(id);
            return Rm.AnalyzeErrMsgObj(emo);
        }
    }

    // A 'ConcreteState' class for dfunc command
    class dfunc : State
    {
        public override string HandleCmd(string id)
        {
            ErrMsgObj emo = DBC.ShowDeviceFunctions(id);
            return Rm.AnalyzeErrMsgObj(emo);
        }
    }

    // A 'ConcreteState' class for check temp command
    class checkTemp : State
    {
        public override string HandleCmd(string id)
        {
            string ris = null;
            ErrMsgObj emo = DBC.FindDeviceFunction(id, 3);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == 0)
            {
                ris = Caller.temp().ToString();
            }
            else
            {
                ris = risT;
            }

            return ris;
        }
    }

    // A 'ConcreteState' class for check nodes command
    class checkNodes : State
    {
        public override string HandleCmd(string id)
        {
            string ris = null;
            ErrMsgObj emo = DBC.FindDeviceFunction(id, 4);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == 0)
            {
                ris = Caller.nodes().ToString();
            }
            else
            {
                ris = risT;
            }

            return ris;
        }
    }

    // A 'ConcreteState' class for check reach command
    class checkReach : State
    {
        public override string HandleCmd(string id)
        {
            string ris = null;
            ErrMsgObj emo = DBC.FindDeviceFunction(id, 1);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == 0)
            {
                ris = Caller.reach().ToString();
            }
            else
            {
                ris = risT;
            }

            return ris;
        }
    }

    // A 'ConcreteState' class for check time command
    class checkTime : State
    {
        public override string HandleCmd(string id)
        {

            string ris = null;
            ErrMsgObj emo = DBC.FindDeviceFunction(id, 2);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == 0)
            {
                ris = Caller.time().ToString();
            }
            else
            {
                ris = risT;
            }

            return ris;
        }
    }

    // A 'ConcreteState' class for check all command
    /*class checkAll : State
    {
        public override string HandleCmd(string id)
        {

            string ris = null;
            if (DbC.ShowDeviceFunctions(id).Contains("orario"))
            {
                ris = caller.time().ToString();
            }
            else
            {
                ris = "Functionality not available on the device selected";
            }
            return ris;
        }
    }*/


    // The 'Context' class
    class Action
    {
        private State state;
        private string id;
        // Constructor
        public Action(State state, string stringa)
        {
            this.State = state;
            this.id = stringa;
        }

        // Gets or sets the state
        public State State
        {
            get { return state; }
            set { state = value; }
        }

        // Gets or sets the id
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Request()
        {
            return state.HandleCmd(id);
        }
    }
}

