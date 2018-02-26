using MatchMaker;
using System;

/// <summary>
/// Request to join the queue of the Matchmaker
/// </summary>
public class MessageHandler_Request_JoinQueue : IMessageHandlerCommand
{
    private MatchMakerCore matchMaker;
    private ServerCore server;
    static int GUIDCounter = 100;

    public MessageHandler_Request_JoinQueue(ServerCore server, MatchMakerCore matchMaker)
    {
        this.matchMaker = matchMaker;
        this.server = server;
    }

    public void Handle(object obj, Server_ServerClient client)
    {
        Message_Request_JoinQueue data = (Message_Request_JoinQueue)obj;
        client.info = data.playerInfo;
        client.info.GUID = GUIDCounter++;
        server.clientManager.RegisterClient(client);
        matchMaker.RegisterClient(client);
        var msg = new Message_Response_InQueue("You are now in queue, players in queue " + server.clientManager.GetClients().Count, client.info);
        server.messageSender.Send(msg,client);
    }

    public Type GetMessageTypeSupported()
    {
        return typeof(Message_Request_JoinQueue);
    }
}