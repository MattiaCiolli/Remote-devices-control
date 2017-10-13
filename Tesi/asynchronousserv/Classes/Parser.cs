﻿
using System;

namespace asynchronousserv
{
    //accepted package structure: header, select action, separator, select device, separator, miscellaneus data, footer. 
    //all data exept header, footer and separators must be in lowercase. Example: ABC actionid DEF deviceid GHI data JKL
    class Parser
    {
        //gets the client request package and extracts it ABC1DEFasd1GHIdataJKL
        public ParserReturn ParseClientRequest(string sData)
        {
            ENUM.ACTIONS action = ENUM.ACTIONS.NO_ACTION;
            string id = null;
            string data = null;
            int l = sData.Length;
            if (sData.StartsWith("ABC") && sData.EndsWith("JKL"))
            {
                try
                {
                    action = (ENUM.ACTIONS)Int32.Parse(Between(sData, "ABC", "DEF"));
                    id = Between(sData, "DEF", "GHI");
                    data = Between(sData, "GHI", "JKL");
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
        public string Between(string Text, string FirstString, string LastString)
        {
            string str = Text; ;
            string FinalString;

            int Pos1 = str.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = str.IndexOf(LastString);
            FinalString = str.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }
}
