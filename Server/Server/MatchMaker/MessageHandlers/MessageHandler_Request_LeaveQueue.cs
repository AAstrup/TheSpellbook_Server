using System;

/// <summary>
/// Handles for players leaving the queue
/// </summary>
internal class MessageHandler_Request_LeaveQueue
{

    internal void Handle(Message_Request_LeaveQueue data, Server_ServerClient client,Server_ClientManager manager)
    {
        manager.RemoveClient(client);
    }
}