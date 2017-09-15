using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace asynchronousserv
{
    class Parser
    {
        public object ParseClientRequest(string sData)
        {

            return new ParserReturn(1, sData);
        }
    }
}
