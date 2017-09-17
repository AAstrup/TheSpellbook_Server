using System;
using System.Collections.Generic;

namespace Match
{
    /// <summary>
    /// This class is created for the new thread that handles the match
    /// The threadstart is the start method of the new thread
    /// </summary>
    public class MatchThread : IGameEngineSender
    {
        private ILogger logger;
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
            GUIDToPlayerClient = new Dictionary<int, Shared_PlayerInfo>();
            GUIDToPlayerClient.Add(info_p1.GUID, this.info_p1);
            GUIDToPlayerClient.Add(info_p2.GUID, this.info_p2);
        }

        public void ThreadStart()
        {
            GameEngine gameEngine = new GameEngine(this,info_p1, info_p2);
            MatchGameMessageHandler matchGameHandler = new MatchGameMessageHandler(logger,gameEngine);
            server = new ServerCore(matchGameHandler,new ConnectionInfo(port));
            matchGameHandler.Init(server);
            while (!gameHasEnded)
            {
                server.Update();
            }
            logger.Log("Match ended");
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

        public void GameFinished()
        {
            throw new NotImplementedException();
        }

        public void Win(int playerGUID)
        {
            foreach (KeyValuePair<int, Shared_PlayerInfo> item in GUIDToPlayerClient)
            {
                if (item.Key == playerGUID)
                {
                    Message_Update_MatchFinished winnerMsg = new Message_Update_MatchFinished(true);
                    Send(item.Key, winnerMsg);
                }
                else
                {
                    Message_Update_MatchFinished loserMsg = new Message_Update_MatchFinished(false);
                    Send(item.Key, loserMsg);
                }
            }
            gameHasEnded = true;
        }
    }
}