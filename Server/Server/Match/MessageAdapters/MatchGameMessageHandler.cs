using Server;
using System;
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

        public MatchGameMessageHandler(ILogger logger, MatchThread matchThread)
        {
            this.matchThread = matchThread;
            this.logger = logger;
            commandHandler = new MessageCommandHandlerServer();
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

        /// <summary>
        /// Setup handlers that requires a sender to reply with
        /// </summary>
        /// <param name="sender"></param>
        internal void Init()
        {
            commandHandler.Add(typeof(Message_Request_JoinGame), new MessageHandler_Request_JoinGame(logger,matchThread.GetServer().messageSender,matchThread));

        }
    }
}