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
        /// </summary>
        List<IMessageHandlerCommand> CreateMessageHandlers(ServerCore server,PingDeterminer pingDeterminer);
    }
}