using System;

namespace Match
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
            GameEngine gameEngine = new GameEngine(p1.info,p2.info);
            MatchGameMessageHandler matchGameHandler = new MatchGameMessageHandler(logger,gameEngine);
            ServerCore server = new ServerCore(matchGameHandler,new ConnectionInfo(port));
            matchGameHandler.Init(server.messageSender);
            while (true)
            {
                server.Update();
            }
        }
    }
}