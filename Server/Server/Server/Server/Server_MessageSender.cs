using System;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// Responsible for sending response messages to the clients
/// </summary>
public class Server_MessageSender
{
    private Server_ClientManager clientManager;

    public Server_MessageSender(Server_ClientManager clientManager)
    {
        this.clientManager = clientManager;
    }


    /// <summary>
    /// Sends a serializable object to a specific client
    /// </summary>
    /// <param name="serializableObject">The serializable object</param>
    /// <param name="server_ServerClient">The recieving client</param>
    public void Send(object serializableObject, Server_ServerClient server_ServerClient)
    {
        BinaryFormatter form = new BinaryFormatter();
        form.Serialize(server_ServerClient.tcp.GetStream(), serializableObject);
    }
}