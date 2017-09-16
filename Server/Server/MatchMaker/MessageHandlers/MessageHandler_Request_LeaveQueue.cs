using MatchMaker;
using System;

/// <summary>
/// Handles for players leaving the queue
/// </summary>
public class MessageHandler_Request_LeaveQueue :IMessageHandlerCommand
{
    private Server_ClientManager manager;
    private MatchMakerCore matchMaker;

    public MessageHandler_Request_LeaveQueue(Server_ClientManager manager, MatchMakerCore matchMaker)
    {
        this.manager = manager;
        this.matchMaker = matchMaker;
    }
    public void Handle(object objData, Server_ServerClient client)
    {
        manager.RemoveClient(client);
        matchMaker.RemoveClient(client);
    }
}