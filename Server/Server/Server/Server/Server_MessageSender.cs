using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Responsible for sending response messages to the clients
/// </summary>
public class Server_MessageSender
{
    private BinaryFormatter form;
    private ILogger logger;
    private Server_ClientManager clientManager;

    public Server_MessageSender(Server_ClientManager clientManager,ILogger logger)
    {
        form = new BinaryFormatter();
        this.logger = logger;
        this.clientManager = clientManager;
    }


    /// <summary>
    /// Sends a serializable object to a specific client
    /// </summary>
    /// <param name="serializableObject">The serializable object</param>
    /// <param name="server_ServerClient">The recieving client</param>
    public void Send(object serializableObject, Server_ServerClient server_ServerClient)
    {
        try
        {
            form.Serialize(server_ServerClient.tcp.GetStream(), serializableObject);
        }
        catch (Exception e)
        {
            if (e is IOException)
            {
                clientManager.ClientLeft(server_ServerClient);
            }
            else
            {
                logger.Log("Exception on msg send: " + e.ToString());
            }
        }
    }

    /// <summary>
    /// Sends to all but a specific client
    /// </summary>
    /// <param name="msg">Message to send</param>
    /// <param name="clientToAvoid">Client that shall not get the message</param>
    /// <param name="clientList">All clients</param>
    public void SendToAllButSpecific(object msg, Server_ServerClient clientToAvoid, List<Server_ServerClient> clientList)
    {
        foreach (var client in clientList)
        {
            if (client != clientToAvoid)
                Send(msg,client);
        }
    }

    /// <summary>
    /// Sends a message to a list of clients
    /// </summary>
    /// <param name="msg">Message to send</param>
    /// <param name="clientList">List of clients</param>
    public void SendToAll(object msg, List<Server_ServerClient> clientList)
    {
        for (int i = 0; i < clientList.Count; i++)
        {
            Send(msg, clientList[i]);
        }
    }
}