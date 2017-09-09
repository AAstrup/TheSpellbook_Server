using System;

namespace MatchMaker
{
    /// <summary>
    /// This class is created for the new thread that handles the match
    /// The threadstart is the start method of the new thread
    /// </summary>
    public class MatchThread
    {
        private Server_ServerClient p1;
        private Server_ServerClient p2;
        private ILogger logger;
        private int port;

        public MatchThread(Server_ServerClient p1, Server_ServerClient p2, int port, ILogger logger)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.port = port;
            this.logger = logger;
        }

        public void ThreadStart()
        {
            IMessageHandler matchGameHandler = new MatchGameHandler(logger);
            ServerCore server = new ServerCore(matchGameHandler,new ConnectionInfo(port));
            while (true)
            {
                server.Update();
                if(server.clientManager.GetClients().Count > 0)
                    logger.Log("Match has gathered " + server.clientManager.GetClients().Count + " players");
            }
        }
    }
}