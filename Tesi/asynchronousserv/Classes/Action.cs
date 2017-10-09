//State pattern implementation
//a state represents an action. When an action is requested the class State will behave differently according to that action

namespace asynchronousserv
{
    // The 'State' abstract class
    abstract class State
    {
        //ADO.Net connection pooling. Connections are not thread safe so each thread should have its on connection but ADO.Net will deal with that
        private DBConnection DbC = new DBConnection();
        private Device dev;
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

        public Device Dev
        {
            get
            {
                return dev;
            }

            set
            {
                dev = value;
            }
        }

        public abstract string HandleCmd(string id);

        //instantiates the correct device type according to the selected device on the DB
        public Device instantiateDeviceByType(int type)
        {
            Device d = null;
            if(type==1)
            {
                d = new IPDevice();
            }
            else if(type==2)
            {
                d = new SerialDevice();
            }else
            {
                //
            }
            return d;
        }
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
            Dev = instantiateDeviceByType(emo.DeviceType);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == 0)
            {
                ris = Dev.CheckTemperature(emo.Address).ToString();
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
            Dev = instantiateDeviceByType(emo.DeviceType);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == 0)
            {
                ris = Dev.CheckNodes(emo.Address).ToString();
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
            Dev = instantiateDeviceByType(emo.DeviceType);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == 0)
            {
                ris = Dev.CheckReachable(emo.Address);
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
            Dev = instantiateDeviceByType(emo.DeviceType);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == 0)
            {
                ris = Dev.CheckTime(emo.Address).ToString();
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

