using System;
using System.Diagnostics;

namespace Match
{
    /// <summary>
    /// Handles messages from the client when they are in a match
    /// </summary>
    internal class MatchGameMessageHandler : IMessageHandler
    {
        private Server_MessageSender sender;
        private GameEngine gameEngine;
        private ILogger logger;
        MessageHandler_Request_JoinGame messageHandler_Request_JoinGame;

        public MatchGameMessageHandler(ILogger logger,GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
            this.logger = logger;
        }

        public void Handle(object data, Server_ServerClient client)
        {
            if (data is Message_Request_JoinGame)
                messageHandler_Request_JoinGame.Handle((Message_Request_JoinGame)data,client,gameEngine);
        }

        internal void Init(Server_MessageSender sender)
        {
            this.sender = sender;
            messageHandler_Request_JoinGame = new MessageHandler_Request_JoinGame (sender,logger);

        }
    }
}