using Server;
using ServerGameObjectExtension;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Match
{
    /// <summary>
    /// Handles messages from the client when they are in a match
    /// </summary>
    public class MatchGameMessageHandler : IMessageHandler
    {
        private MatchThread matchThread;
        private ILogger logger;
        private MessageCommandHandlerServer commandHandler;
        private List<IServerExtension> serverExtensions;

        public MatchGameMessageHandler(ILogger logger, MatchThread matchThread, List<IServerExtension> serverExtensions)
        {
            this.matchThread = matchThread;
            this.logger = logger;
            commandHandler = new MessageCommandHandlerServer();
            this.serverExtensions = serverExtensions;
        }

        /// <summary>
        /// Handles messages sent by clients by using a command pattern if possible
        /// Currently defaulting to forwarding messages to remaining clients if no command found
        /// </summary>
        /// <param name="data">Message sent</param>
        /// <param name="client">Client sending</param>
        public void Handle(object data, Server_ServerClient client)
        {
            if (commandHandler.Contains(data.GetType()))
                commandHandler.Execute(data.GetType(), data, client);
            else
            {
                ForwardMessageToOtherClients(data, client);
            }
        }

        /// <summary>
        /// Forwards a message to all clients but the given one
        /// Temporary implementation for handling unknown messages
        /// </summary>
        /// <param name="data">Message to be forwarded</param>
        /// <param name="client">Client that original send it</param>
        private void ForwardMessageToOtherClients(object data, Server_ServerClient client)
        {
            foreach (var otherClient in matchThread.GetServer().clientManager.GetClients())
            {
                if (client != otherClient)
                    matchThread.GetServer().messageSender.Send(data, otherClient);
            }
        }

        /// <summary>
        /// Setup handlers that requires a sender to reply with
        /// </summary>
        /// <param name="sender"></param>
        internal void Init()
        {
            commandHandler.Add(typeof(Message_Request_JoinGame), new MessageHandler_Request_JoinGame(logger,matchThread.GetServer().messageSender,matchThread, serverExtensions));

        }
    }
}