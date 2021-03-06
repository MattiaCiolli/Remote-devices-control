﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    public class SerialDevice : Device
    {
        private string PhoneNumber { get; set; }

        public void CheckAll(string PhoneNumber_in)
        { }
        public string CheckReachable(string PhoneNumber_in)
        {
            return "false";
        }
        public double CheckTemperature(string PhoneNumber_in)
        {
            return 15;
        }
        public string CheckTime(string PhoneNumber_in)
        {
            return new DateTime().AddDays(2).ToString();
        }
        public string CheckNodes(string PhoneNumber_in)
        {
            return "ok,KO,ok";
        }

        public string CheckVoltage(string PhoneNumber_in)
        {
            return "10,11,10";
        }

    }
}
