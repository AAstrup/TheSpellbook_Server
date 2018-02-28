using MatchMaker;
using Server;
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
    private MessageCommandHandlerServer commandHandler;

    public void Setup(ServerCore server, MatchMakerCore matchMakerCore)
    {
        this.matchMakerCore = matchMakerCore;
        this.server = server;
        commandHandler = new MessageCommandHandlerServer();
        commandHandler.Add(new MessageHandler_Request_JoinQueue(server, matchMakerCore));
        commandHandler.Add(new MessageHandler_Request_LeaveQueue(server.clientManager, matchMakerCore));
        commandHandler.Add(new MessageHandler_Response_ReadyCheck(matchMakerCore, server));
    }

    /// <summary>
    /// The method responsible for getting a serialized object 
    /// and giving it to the correct handler
    /// </summary>
    /// <param name="data"></param>
    /// <param name="client"></param>
    public void Handle(object data, Server_ServerClient client)
    {
        Console.WriteLine("Data type recieved of type " + data.GetType().ToString());
        if (commandHandler.Contains(data.GetType()))
            commandHandler.Execute(data.GetType(),data,client);
        else
        {
            Console.WriteLine("Data type UNKNOWN! Type: " + data.GetType().ToString());
            throw new Exception("Data type UNKNOWN! Type: " + data.GetType().ToString());
        }
    }

}