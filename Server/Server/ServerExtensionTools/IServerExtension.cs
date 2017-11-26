using Match;
using Server;
using System.Collections.Generic;

namespace ServerGameObjectExtension
{
    public interface IServerExtension
    {
        /// <summary>
        /// Get messages requried to setup a client
        /// </summary>
        /// <param name="client"></param>
        List<object> GetMessagesForClientSetup(Server_ServerClient client);

        /// <summary>
        /// Get the messagehandlers provided by the extension
        /// Get by returning the messagehandler
        /// </summary>
        List<IMessageHandlerCommand> CreateMessageHandlers(ServerCore server,PingDeterminer pingDeterminer, MatchGameEventContainer matchGameEventWrapper, Clock matchClock);

        /// <summary>
        /// Setup subscribers to the eventhandler
        /// </summary>
        /// <param name="server">The server</param>
        /// <param name="serverCoreEventHandler">Eventhandler of the server</param>
        void SetupSubscribers(ServerCore server, IServerEventHandler serverCoreEventHandler);
    }
}