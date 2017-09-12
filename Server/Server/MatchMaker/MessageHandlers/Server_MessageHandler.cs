using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that is responsible for getting a serialized object 
/// and giving it to the correct handler
/// </summary>
public class Server_MessageHandler :IMessageHandler {
    private ServerCore server;
    MessageHandler_Request_JoinQueue handler_Message_Request_JoinQueue;
    MessageHandler_Request_LeaveQueue handler_Request_LeaveQueue;

    public void Setup(ServerCore server)
    {
        this.server = server;
        handler_Message_Request_JoinQueue = new MessageHandler_Request_JoinQueue(server);
        handler_Request_LeaveQueue = new MessageHandler_Request_LeaveQueue();
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
            handler_Request_LeaveQueue.Handle((Message_Request_LeaveQueue)data, client,server.clientManager);
        else
            throw new Exception("Data type UKNOWN! Type: " + data.ToString());
    }
}