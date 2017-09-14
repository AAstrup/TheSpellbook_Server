using MatchMaker;
using System;

/// <summary>
/// Handles for players leaving the queue
/// </summary>
internal class MessageHandler_Request_LeaveQueue
{

    internal void Handle(Server_ServerClient client,Server_ClientManager manager,MatchMakerCore matchMaker)
    {
        manager.RemoveClient(client);
        matchMaker.RemoveClient(client);
    }
}