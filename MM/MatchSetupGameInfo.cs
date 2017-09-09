namespace MatchMaker
{
    internal class MatchSetupGameInfo
    {
        private Server_ServerClient p1;
        private Server_ServerClient p2;
        private int nextport;

        public MatchSetupGameInfo(Server_ServerClient p1, Server_ServerClient p2, int nextport)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.nextport = nextport;
        }
    }
}