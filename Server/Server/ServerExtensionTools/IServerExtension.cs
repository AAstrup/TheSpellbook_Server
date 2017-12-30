using Match;
using Server;
using System.Collections.Generic;

namespace ServerGameObjectExtension
{
    public interface IServerExtension
    {
        /// <summary>
        /// Get messages required to setup a client
        /// </summary>
        /// <param name="client">Client being generated message for</param>
        /// <param name="clock">Clock of server</param>
        List<object> GetMessagesForClientSetup(Server_ServerClient client, Clock clock);

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