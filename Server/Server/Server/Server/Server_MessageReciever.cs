using Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// The class responsible for recieving and handling messages from the server
/// </summary>
public class Server_MessageReciever
{
    private Server_Connection connection;
    private IMessageHandler messageHandler;

    public Server_MessageReciever(Server_Connection connection, IMessageHandler messageHandler)
    {
        this.connection = connection;
        this.messageHandler = messageHandler;
    }

    /// <summary>
    /// Check for client request messages and process them
    /// </summary>
    public void CheckForClientRequestMessages()
    {
        for (int i = 0; i < connection.GetClientManager().GetClients().Count; i++)
        {
            Server_ServerClient serverClient = connection.GetClientManager().GetClients()[i];
            NetworkStream networkStream = serverClient.tcp.GetStream();
            if (networkStream.DataAvailable)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var data = formatter.Deserialize(networkStream);
                Console.WriteLine("Data recieved");

                if (data != null)
                {
                    OnIncomingData(serverClient, data);
                }
            }
        }
    }

    /// <summary>
    /// Method called when recieving data
    /// </summary>
    /// <param name="client">Client issuing the call</param>
    /// <param name="data">Object of data send</param>
    private void OnIncomingData(Server_ServerClient client, object data)
    {
        messageHandler.Handle(data, client);
    }
}