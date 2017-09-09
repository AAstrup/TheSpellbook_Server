using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Class that is responsible for getting a serialized object 
/// and giving it to the correct handler
/// </summary>
public class Server_MessageHandler :IMessageHandler {

    MessengeHandler_Request_JoinQueue handler_Message_Request_JoinQueue;

    public void Setup(ServerCore server)
    {
        handler_Message_Request_JoinQueue = new MessengeHandler_Request_JoinQueue(server);
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
        else
            throw new Exception("Data type UKNOWN! Type: " + data.ToString());
    }
}