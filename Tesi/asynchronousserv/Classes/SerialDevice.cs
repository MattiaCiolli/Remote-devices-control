using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    public class SerialDevice : Device
    {
        private string PhoneNumber { get; set; }

        public void CheckAll(string PhoneNumber)
        { }
        public string CheckReachable(string PhoneNumber)
        {
            return "false";
        }
        public double CheckTemperature(string PhoneNumber)
        {
            return 15;
        }
        public DateTime CheckTime(string PhoneNumber)
        {
            return new DateTime().AddDays(2);
        }
        public string CheckNodes(string PhoneNumber)
        {
            return "ok,KO,ok";
        }
    }
}
