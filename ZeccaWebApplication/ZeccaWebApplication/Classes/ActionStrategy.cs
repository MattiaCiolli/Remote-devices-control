//Strategy pattern implementation
//a strategy represents a way an action is executed. When an action is requested, the class ActionStrategy will behave differently according to that action, using one of its sub-classes

using ZeccaWebAPI.Models;

namespace ZeccaWebAPI
{
    // The 'Strategy' abstract class
    abstract class ActionStrategy
    {
        //ADO.Net connection pooling. Connections are not thread safe so each thread should have its on connection but ADO.Net will deal with that
        private DBConnection DbC = new DBConnection();
        private Device dev;

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

        public abstract string HandleCmd(string id_in);

        //instantiates the correct device type according to the selected device on the DB
        public Device instantiateDeviceByType(ENUM.DEVICES type_in)
        {
            Device d = null;
            switch (type_in)
            {
                case ENUM.DEVICES.IP:
                    d = new IPDevice();
                    break;
                case ENUM.DEVICES.SERIAL232_TYPE1:
                    d = new SerialDevice();
                    break;
                default:
                    break;
            }
            return d;
        }
    }

    // A class for check temp command
    class checkTempStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            Dispositivi disp = DBC.FindDeviceById(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckTemperature(disp.indirizzo).ToString();
            return ris;
        }
    }

    // A class for check voltage command
    class checkVoltStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            Dispositivi disp = DBC.FindDeviceById(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckVoltage(disp.indirizzo).ToString();
            return ris;
        }
    }

    // A class for check nodes command
    class checkNodesStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            Dispositivi disp = DBC.FindDeviceById(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckNodes(disp.indirizzo).ToString();
            return ris;
        }
    }

    // A class for check reach command
    class checkReachStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            Dispositivi disp = DBC.FindDeviceById(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckReachable(disp.indirizzo);
            return ris;
        }
    }

    // A class for check time command
    class checkTimeStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            Dispositivi disp = DBC.FindDeviceById(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckTime(disp.indirizzo);
            return ris;
        }
    }
}

// A class for check all command
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
