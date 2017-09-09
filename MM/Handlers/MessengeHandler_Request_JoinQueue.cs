using System;

internal class MessengeHandler_Request_JoinQueue
{
    private ServerCore server;

    public MessengeHandler_Request_JoinQueue(ServerCore server)
    {
        this.server = server;
    }

    internal void Handle(Message_Request_JoinQueue data, Server_ServerClient client)
    {
        var msg = new Message_Response_InQueue("You are now in queue, players in queue " + server.clientManager.GetClients().Count);
        server.messageSender.Send(msg,client);
    }
}