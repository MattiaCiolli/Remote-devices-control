
namespace asynchronousserv
{
    class Parser
    {
        //gets the client request packet and extracts it
        public object ParseClientRequest(string sData)
        {
            return new ParserReturn(1, sData);
        }
    }
}
