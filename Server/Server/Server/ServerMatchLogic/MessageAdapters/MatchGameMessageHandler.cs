using Server;
using ServerExtensionTools;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Match
{
    /// <summary>
    /// Handles messages from clients when they are in a match
    /// </summary>
    public class MatchGameMessageHandler : IMessageHandler
    {
        private MatchThread matchThread;
        private ILogger logger;
        private MessageCommandHandlerServer commandHandler;
        private MatchGameEventContainer matchGameEventWrapper;

        public MatchGameMessageHandler(ILogger logger, MatchThread matchThread)
        {
            this.matchThread = matchThread;
            this.logger = logger;
            commandHandler = new MessageCommandHandlerServer();
            matchGameEventWrapper = new MatchGameEventContainer();
        }

        /// <summary>
        /// Handles messages sent by clients by using a command pattern if possible
        /// Currently defaulting to forwarding messages to remaining clients if no command found
        /// </summary>
        /// <param name="data">Message sent</param>
        /// <param name="client">Client sending</param>
        public void Handle(object data, Server_ServerClient client)
        {
            Console.WriteLine("Data type recieved of type " + data.GetType().ToString());
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
        /// Setup extensions
        /// </summary>
        /// <param name="serverExtensions">Extensions</param>
        /// <param name="matchClock">Clock of the match</param>
        /// <param name="serverCoreEventHandler">Eventhandler of the ServerCore</param>
        internal void Init(List<IServerExtension> serverExtensions,Clock matchClock, MatchServerCoreEventHandler serverCoreEventHandler)
        {
            commandHandler.Add( new MessageHandler_Request_JoinGame(logger,matchThread.GetServer().messageSender,matchThread, serverExtensions, matchGameEventWrapper));
            commandHandler.Add(new MessageHandler_ServerRoundTrip_Ping(logger, matchThread.GetServer().messageSender, matchThread, serverExtensions));
            foreach (var extension in serverExtensions)
            {
                foreach (var msgHandler in extension.CreateMessageHandlers(matchThread.GetServer(), matchThread.pingDeterminer, matchGameEventWrapper, matchClock))
                {
                    commandHandler.Add(msgHandler);
                }
                extension.SetupSubscribers(matchThread.GetServer(), serverCoreEventHandler);
            }
        }
    }
}