using MatchMaker;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that is responsible for getting a serialized object 
/// and giving it to the correct handler
/// </summary>
public class Server_MessageHandler :IMessageHandler {
    private MatchMakerCore matchMakerCore;
    private ServerCore server;
    MessageHandler_Request_JoinQueue handler_Message_Request_JoinQueue;
    MessageHandler_Request_LeaveQueue handler_Request_LeaveQueue;
    MessageHandler_Response_ReadyCheck hander_Response_ReadyCheck;

    public void Setup(ServerCore server, MatchMakerCore matchMakerCore)
    {
        this.matchMakerCore = matchMakerCore;
        this.server = server;
        handler_Message_Request_JoinQueue = new MessageHandler_Request_JoinQueue(server,matchMakerCore);
        handler_Request_LeaveQueue = new MessageHandler_Request_LeaveQueue();
        hander_Response_ReadyCheck = new MessageHandler_Response_ReadyCheck(matchMakerCore);
    }
    /// <summary>
    /// The method responsible for getting a serialized object 
    /// and giving it to the correct handler
    /// </summary>
    /// <param name="data"></param>
    /// <param name="client"></param>
    public void Handle(object data, Server_ServerClient client)
    {
        Console.WriteLine("Data recieved of type " + data.ToString());
        if (data is Message_Request_JoinQueue)
            handler_Message_Request_JoinQueue.Handle((Message_Request_JoinQueue)data, client);
        else if (data is Message_Request_LeaveQueue)
            handler_Request_LeaveQueue.Handle(client, server.clientManager,matchMakerCore);
        else if (data is Message_ClientResponse_ReadyCheck)
            hander_Response_ReadyCheck.Handle((Message_ClientResponse_ReadyCheck)data, client, server);
        else
        {
            Console.WriteLine("Data type UKNOWN! Type: " + data.ToString());
            throw new Exception("Data type UKNOWN! Type: " + data.ToString());
        }
    }

}