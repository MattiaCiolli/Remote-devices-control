//Strategy pattern implementation
//a strategy represents a way an action is executed. When an action is requested, the class ActionStrategy will behave differently according to that action, using one of its sub-classes

using ZeccaWebAPI.Models;
using ZeccaWebApplication.Models;

namespace ZeccaWebAPI
{
    // The 'Strategy' abstract class
    abstract class ActionStrategy
    {
        //ADO.Net connection pooling. Connections are not thread safe so each thread should have its on connection but ADO.Net will deal with that        
        private asdEntities3 db = new asdEntities3();
        private DeviceCaller dev;

        public DeviceCaller Dev
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

        public asdEntities3 Db
        {
            get
            {
                return db;
            }

            set
            {
                db = value;
            }
        }

        public abstract string HandleCmd(string id_in);

        //instantiates the correct device type according to the selected device on the DB
        public DeviceCaller instantiateDeviceByType(ENUM.DEVICES type_in)
        {
            DeviceCaller d = null;
            switch (type_in)
            {
                case ENUM.DEVICES.IP:
                    d = new IPDeviceCaller();
                    break;
                case ENUM.DEVICES.SERIAL232_TYPE1:
                    d = new SerialDeviceCaller();
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
            Dispositivi disp = Db.Dispositivi.Find(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckTemperature(disp.indirizzo, disp.matricola).ToString();
            return ris;
        }
    }

    // A class for check voltage command
    class checkVoltStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            Dispositivi disp = Db.Dispositivi.Find(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckVoltage(disp.indirizzo, disp.matricola).ToString();
            return ris;
        }
    }

    // A class for check nodes command
    class checkNodesStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            Dispositivi disp = Db.Dispositivi.Find(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckNodes(disp.indirizzo, disp.matricola).ToString();
            return ris;
        }
    }

    // A class for check reach command
    class checkReachStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            Dispositivi disp = Db.Dispositivi.Find(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckReachable(disp.indirizzo, disp.matricola);
            return ris;
        }
    }

    // A class for check time command
    class checkTimeStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            Dispositivi disp = Db.Dispositivi.Find(id_in);
            Dev = instantiateDeviceByType((ENUM.DEVICES)disp.tipo);
            ris = Dev.CheckTime(disp.indirizzo, disp.matricola);
            return ris;
        }
    }
}
