
using System;

namespace asynchronousserv
{
    //accepted package structure: header, select action, separator, select device, separator, miscellaneus data, footer. 
    //all data exept header, footer and separators must be in lowercase. Example: ABC actionid DEF deviceid GHI data JKL
    class Parser
    {
        //gets the client request package and extracts it ABC1DEFasd1GHIdataJKL
        public ParserReturn ParseClientRequest(string sData_in)
        {
            ENUM.ACTIONS action = ENUM.ACTIONS.NO_ACTION;
            string id = null;
            string data = null;
            int l = sData_in.Length;
            if (sData_in.StartsWith("ABC") && sData_in.EndsWith("JKL"))
            {
                try
                {
                    action = (ENUM.ACTIONS)Int32.Parse(Between(sData_in, "ABC", "DEF"));
                    id = Between(sData_in, "DEF", "GHI");
                    data = Between(sData_in, "GHI", "JKL");
                }
                catch
                {
                    Console.WriteLine("Package format error");
                    action = ENUM.ACTIONS.NO_ACTION;
                    id = null;
                    data = null;
                }

            }
            else
            {
                action = ENUM.ACTIONS.NO_ACTION;
                id = null;
                data = null;
            }
            return new ParserReturn(action, id, data);
        }

        //extracts a string between FirstString and LastString excluded.
        //Ex. Between("abc", "a", "c") = b
        public string Between(string Text_in, string FirstString_in, string LastString_in)
        {
            string str = Text_in; ;
            string FinalString;

            int Pos1 = str.IndexOf(FirstString_in) + FirstString_in.Length;
            int Pos2 = str.IndexOf(LastString_in);
            FinalString = str.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }
}
