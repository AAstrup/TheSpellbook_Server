﻿using MatchMaker;
using System;

/// <summary>
/// Request to join the queue of the Matchmaker
/// </summary>
internal class MessageHandler_Request_JoinQueue
{
    private MatchMakerCore matchMaker;
    private ServerCore server;
    static int GUIDCounter = 100;

    public MessageHandler_Request_JoinQueue(ServerCore server, MatchMakerCore matchMaker)
    {
        this.matchMaker = matchMaker;
        this.server = server;
    }

    internal void Handle(Message_Request_JoinQueue data, Server_ServerClient client)
    {
        client.info = data.playerInfo;
        client.info.GUID = GUIDCounter++;
        server.clientManager.RegisterClient(client);
        matchMaker.RegisterClient(client);
        var msg = new Message_Response_InQueue("You are now in queue, players in queue " + server.clientManager.GetClients().Count, client.info);
        server.messageSender.Send(msg,client);
    }
}