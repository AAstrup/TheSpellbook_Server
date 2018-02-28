using SharedClientServerGameObjectMessages;
using System;

namespace ServerGameObjectExtension
{
    /// <summary>
    /// Provides the event for the ServerHameObjectExtension when setting up
    /// </summary>
    internal class SharedClientServerGameObjectMessagesWrapperEvents
    {
        internal static void GetClientLeftEvent(Server_ServerClient server_ServerClient, ServerCore server)
        {
            var msg = new Message_ServerCommand_PlayerLeft(server_ServerClient.info.GUID);
            server.messageSender.SendToAll(msg,server.clientManager.GetClients());
        }
    }
}