//Strategy pattern implementation
//a strategy represents a way an action is executed. When an action is requested, the class ActionStrategy will behave differently according to that action, using one of its sub-classes

namespace asynchronousserv
{
    // The 'Strategy' abstract class
    abstract class ActionStrategy
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

    // A class for dinfo command
    class dinfoStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            ErrMsgObj emo = DBC.SelectDeviceById(id_in);
            return Rm.AnalyzeErrMsgObj(emo);
        }
    }

    // A class for dfunc command
    class dfuncStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            ErrMsgObj emo = DBC.ShowDeviceFunctions(id_in);
            return Rm.AnalyzeErrMsgObj(emo);
        }
    }

    // A class for check temp command
    class checkTempStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            ErrMsgObj emo = DBC.FindDeviceFunction(id_in, 3);
            Dev = instantiateDeviceByType(emo.DeviceType);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == ENUM.ERRORS.NO_ERRORS)
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

    // A class for check voltage command
    class checkVoltStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            ErrMsgObj emo = DBC.FindDeviceFunction(id_in, 5);
            Dev = instantiateDeviceByType(emo.DeviceType);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == ENUM.ERRORS.NO_ERRORS)
            {
                ris = Dev.CheckVoltage(emo.Address).ToString();
            }
            else
            {
                ris = risT;
            }

            return ris;
        }
    }

    // A class for check nodes command
    class checkNodesStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            ErrMsgObj emo = DBC.FindDeviceFunction(id_in, 4);
            Dev = instantiateDeviceByType(emo.DeviceType);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == ENUM.ERRORS.NO_ERRORS)
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

    // A class for check reach command
    class checkReachStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            ErrMsgObj emo = DBC.FindDeviceFunction(id_in, 1);
            Dev = instantiateDeviceByType(emo.DeviceType);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == ENUM.ERRORS.NO_ERRORS)
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

    // A class for check time command
    class checkTimeStrategy : ActionStrategy
    {
        public override string HandleCmd(string id_in)
        {
            string ris = null;
            ErrMsgObj emo = DBC.FindDeviceFunction(id_in, 2);
            Dev = instantiateDeviceByType(emo.DeviceType);
            string risT = Rm.AnalyzeErrMsgObj(emo);
            if (emo.ErrCode == ENUM.ERRORS.NO_ERRORS)
            {
                ris = Dev.CheckTime(emo.Address);
            }
            else
            {
                ris = risT;
            }

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
