using System;

namespace Match
{
    /// <summary>
    /// This class is created for the new thread that handles the match
    /// The threadstart is the start method of the new thread
    /// </summary>
    public class MatchThread 
    {
        private ILogger logger;
        private int port;
        private ServerCore server;
        private Shared_PlayerInfo info_p1;
        private Shared_PlayerInfo info_p2;

        public MatchThread(Shared_PlayerInfo info_p1, Shared_PlayerInfo info_p2, int port, ILogger logger)
        {
            this.info_p1 = info_p1;
            this.info_p2 = info_p2;
            this.port = port;
            this.logger = logger;
        }

        public void ThreadStart()
        {
            GameEngine gameEngine = new GameEngine(this,info_p1, info_p2);
            MatchGameMessageHandler matchGameHandler = new MatchGameMessageHandler(logger,gameEngine);
            server = new ServerCore(matchGameHandler,new ConnectionInfo(port));
            matchGameHandler.Init(server);
            while (true)
            {
                server.Update();
            }
        }

        public void Send(int playerGUID,object msg)
        {
            foreach (var client in server.clientManager.GetClients())
            {
                if (client.info.GUID == playerGUID)
                {
                    server.messageSender.Send(msg, client);
                    return; //Issue everyone wins atm??? Not sure if its unity or what
                }
            }
        }
    }
}