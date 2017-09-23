//State pattern implementation
//a state represents an action. When an action is requested the class State will behave differently according to that action

namespace asynchronousserv
{
    // The 'State' abstract class
    abstract class State
    {
        //ADO.Net connection pooling. Connections are not thread safe so each thread should have its on connection but ADO.Net will deal with that
        public DBConnection DbC = new DBConnection();
        public Caller caller = new Caller();
        public abstract string HandleCmd(string id);
    }

    // A 'ConcreteState' class for dinfo command
    class dinfo : State
    {
        public override string HandleCmd(string id)
        {
            string ris = DbC.SelectDeviceById(id);
            return ris;
        }
    }

    // A 'ConcreteState' class for dfunc command
    class dfunc : State
    {
        public override string HandleCmd(string id)
        {
            string ris = DbC.ShowDeviceFunctions(id);
            return ris;
        }
    }

    // A 'ConcreteState' class for check temp command
    class checkTemp : State
    {
        public override string HandleCmd(string id)
        {
            string ris = null;
            if (DbC.ShowDeviceFunctions(id).Contains("temperatura"))
            {
                ris = caller.temp().ToString();
            }
            else
            {
                ris = "Functionality not available on the device selected";
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
            if (DbC.ShowDeviceFunctions(id).Contains("nodi"))
            {
                ris = caller.nodes().ToString();
            }
            else
            {
                ris = "Functionality not available on the device selected";
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

            if (DbC.ShowDeviceFunctions(id).Contains("raggiungibilita"))
            {
                ris = caller.reach().ToString();
            }
            else
            {
                ris = "Functionality not available on the device selected";
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

