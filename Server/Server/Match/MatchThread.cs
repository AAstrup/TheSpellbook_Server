using Server;
using System;
using System.Collections.Generic;

namespace Match
{
    /// <summary>
    /// This class is created for the new thread that handles the match
    /// The threadstart is the start method of the new thread
    /// </summary>
    public class MatchThread 
    {
        private ILogger logger;
        private GameEngineSender_MatchSender sender;
        private Dictionary<int, Shared_PlayerInfo> GUIDToPlayerClient;
        private int port;
        private ServerCore server;
        private Shared_PlayerInfo info_p1;
        private Shared_PlayerInfo info_p2;
        private bool gameHasEnded;

        public MatchThread(Shared_PlayerInfo info_p1, Shared_PlayerInfo info_p2, int port, ILogger logger)
        {
            this.info_p1 = info_p1;
            this.info_p2 = info_p2;
            this.port = port;
            this.logger = logger;
            var GUIDToPlayerClient = new Dictionary<int, Shared_PlayerInfo>();
            GUIDToPlayerClient.Add(info_p1.GUID, this.info_p1);
            GUIDToPlayerClient.Add(info_p2.GUID, this.info_p2);
            sender = new GameEngineSender_MatchSender(GUIDToPlayerClient);
        }

        public void ThreadStart()
        {
            GameEngine gameEngine = new GameEngine(sender,info_p1, info_p2);
            MatchGameMessageHandler matchGameHandler = new MatchGameMessageHandler(logger,gameEngine);
            server = new ServerCore(matchGameHandler,new ConnectionInfo(port));
            sender.Init(server);
            matchGameHandler.Init(sender);

            while (!gameHasEnded)
            {
                server.Update();
            }

            logger.Log("Match ended");
        }

        public ServerCore GetServer() { return server; }
    }
}