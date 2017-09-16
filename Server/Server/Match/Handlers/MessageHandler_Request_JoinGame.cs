using System;

namespace Match
{
    internal class MessageHandler_Request_JoinGame : IMessageHandlerCommand
    {
        private GameEngine gameEngine;
        private Server_ClientManager server_ClientManager;
        ILogger logger;
        private Server_MessageSender sender;

        public MessageHandler_Request_JoinGame(Server_MessageSender sender, ILogger logger,Server_ClientManager server_ClientManager, GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
            this.server_ClientManager = server_ClientManager;
            this.logger = logger;
            this.sender = sender;
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            Message_Request_JoinGame data = (Message_Request_JoinGame)objData;
            Shared_InGame_PlayerInfo me = null;
            Shared_InGame_PlayerInfo opp = null;
            if (data.info.GUID == gameEngine.p1.GUID) {
                me = gameEngine.p1;
                client.info = data.info;
                server_ClientManager.RegisterClient(client);
                opp = gameEngine.p2;
            } else if (data.info.GUID == gameEngine.p2.GUID) {
                me = gameEngine.p2;
                client.info = data.info;
                server_ClientManager.RegisterClient(client);
                opp = gameEngine.p1;
            }
            else
                throw new Exception("Player GUID not found for a registered play! p1 guid " + gameEngine.p1.GUID + " p2 guid " + gameEngine.p2.GUID);

            Message_Response_GameState gameData = new Message_Response_GameState(me,opp.GetAsHidden());//Make so you don't get your opponents cards!
            sender.Send(gameData, client);
        }

    }
}