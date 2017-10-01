using System;
using System.IO;
using System.Net.Sockets;

/// <summary>
/// The class that starts everything up for the client
/// Everything is started in its constructor
/// </summary>
public class Client
{
    private IUnityComponentResetable owner;
    ClientConnection connection;
    public Client_MessageSender sender;
    IMessageHandler messageHandler;
    Client_MessageReciever reciever;
    PersistentData data;
    ILogger logger;

    public Client(IUnityComponentResetable owner,ClientConnectionInfo connectionInfo, IMessageHandler messageHandler, PersistentData data,ILogger logger)
    {
        this.owner = owner;
        this.logger = logger;
        connection = new ClientConnection(connectionInfo,logger);
        sender = new Client_MessageSender(connection,logger);
        this.messageHandler = messageHandler;
        reciever = new Client_MessageReciever(connection, messageHandler);
        this.data = data;
    }

    /// <summary>
    /// Sends a register message to server, everything must be set up at this point
    /// </summary>
    public void Register()
    {
        sender.RegisterAtServer(data.PlayerInfo);
    }

    public void Dispose()
    {
        connection.GetSocket().Close();
        owner.Clean();
    }

    /// <summary>
    /// Checks for responses from the server
    /// </summary>
    public void Update()
    {
        reciever.CheckForServerResponseMessages();
    }

    internal void KillConnection()
    {
        connection.GetSocket().Close();
    }
}