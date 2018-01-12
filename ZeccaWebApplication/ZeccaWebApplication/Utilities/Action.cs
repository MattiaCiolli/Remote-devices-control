// The 'Context' class, when created selects the correct strategy for the received command
using ZeccaWebApplication.Classes;

namespace ZeccaWebAPI
{
    class Action
    {
        private ActionStrategy strategy;
        private string dev_id;
        private string act_name;
        // Constructor, check cmd and chooses the correct strategy
        public Action(string devId_in, ENUM.ACTIONS act_in)
        {
            this.dev_id = devId_in;
            switch (act_in)
            {
                case ENUM.ACTIONS.CHECK_TEMPERATURE:
                    this.strategy = new checkTempStrategy();
                    this.act_name = ("Temperatura");
                    break;
                case ENUM.ACTIONS.CHECK_NODES:
                    this.strategy = new checkNodesStrategy();
                    this.act_name = ("Stato dei nodi");
                    break;
                case ENUM.ACTIONS.CHECK_TIME:
                    this.strategy = new checkTimeStrategy();
                    this.act_name = ("Orario");
                    break;
                case ENUM.ACTIONS.CHECK_REACHABILITY:
                    this.strategy = new checkReachStrategy();
                    this.act_name = ("Raggiungibilità");
                    break;
                case ENUM.ACTIONS.CHECK_VOLTAGE:
                    this.strategy = new checkVoltStrategy();
                    this.act_name = ("Voltaggio");
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
        public string Dev_id
        {
            get { return dev_id; }
            set { dev_id = value; }
        }

        public RequestReturn Request()
        {
            string data = strategy.HandleCmd(dev_id);
            return new RequestReturn(act_name, data);
        }
    }
}


