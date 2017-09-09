using System.Diagnostics;

namespace MatchMaker
{
    /// <summary>
    /// Handles messages from the client when they are in a match
    /// </summary>
    internal class MatchGameHandler : IMessageHandler
    {
        private ILogger logger;

        public MatchGameHandler(ILogger logger)
        {
            this.logger = logger;
        }

        public void Handle(object data, Server_ServerClient client)
        {
            logger.Log("Data recieved of type " + data.GetType() + " from client " + client.info.name);
        }
    }
}