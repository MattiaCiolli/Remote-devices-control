// The 'Context' class, when created selects the correct strategy for the received command
namespace asynchronousserv
{
    class Action
    {
        private ActionStrategy strategy;
        private string id;
        // Constructor, check cmd and chooses the correct strategy
        public Action(string id_in, ENUM.ACTIONS cmd_in)
        {
            this.id = id_in;

            switch (cmd_in)
            {
                case ENUM.ACTIONS.DEVICE_INFO:
                    this.Strategy = new dinfoStrategy();
                    break;
                case ENUM.ACTIONS.DEVICE_FUNCTIONS:
                    this.Strategy = new dfuncStrategy();
                    break;
                case ENUM.ACTIONS.CHECK_TEMPERATURE:
                    this.Strategy = new checkTempStrategy();
                    break;
                case ENUM.ACTIONS.CHECK_NODES:
                    this.Strategy = new checkNodesStrategy();
                    break;
                case ENUM.ACTIONS.CHECK_TIME:
                    this.Strategy = new checkTimeStrategy();
                    break;
                case ENUM.ACTIONS.CHECK_REACHABILITY:
                    this.Strategy = new checkReachStrategy();
                    break;
                case ENUM.ACTIONS.CHECK_VOLTAGE:
                    this.Strategy = new checkVoltStrategy();
                    break;
                default:
                    break;
            }
        }

        // Gets or sets the strategy
        public ActionStrategy Strategy
        {
            get { return strategy; }
            set { strategy = value; }
        }

        // Gets or sets the id
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Request()
        {
            return strategy.HandleCmd(id);
        }
    }
}


