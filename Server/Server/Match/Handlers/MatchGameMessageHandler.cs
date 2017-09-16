using System;
using System.Diagnostics;

namespace Match
{
    /// <summary>
    /// Handles messages from the client when they are in a match
    /// </summary>
    internal class MatchGameMessageHandler : IMessageHandler
    {
        private ServerCore server;
        private GameEngine gameEngine;
        private ILogger logger;
        private MessageCommandHandler commandHandler;

        public MatchGameMessageHandler(ILogger logger,GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
            this.logger = logger;
            commandHandler.Add(typeof(Message_Request_PlayCard),new MessageHandler_Request_PlayCard(gameEngine));
        }

        public void Handle(object data, Server_ServerClient client)
        {
            if (commandHandler.Contains(data.GetType()))
                commandHandler.Execute(data.GetType(), data, client);
            else
            {
                Console.WriteLine("Data type UKNOWN! Type: " + data.GetType().ToString());
                throw new Exception("Data type UKNOWN! Type: " + data.GetType().ToString());
            }
        }

        internal void Init(ServerCore server)
        {
            this.server = server;
            commandHandler.Add(typeof(Message_Request_JoinGame), new MessageHandler_Request_JoinGame(server.messageSender, logger, server.clientManager,gameEngine));
        }
    }
}