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
    public IClient_MessageSender sender;
    IMessageHandler messageHandler;
    Client_MessageReciever reciever;
    PersistentData data;
    private IClientConfig config;
    ILogger logger;
    public delegate void UpdateEvent(double clockTime);
    public event UpdateEvent ClockUpdateEvent;

    public Client(IUnityComponentResetable owner,ClientConnectionInfo connectionInfo, IMessageHandler messageHandler, PersistentData data,ILogger logger, IConnectionResultHandler connectionResultHandler, IClientConfig config)
    {
        this.owner = owner;
        this.logger = logger;
        connection = new ClientConnection(connectionInfo,logger, connectionResultHandler);
        sender = Client_MessageSenderFactory.CreateSender(config,config.GetClock(),connection,logger,this);
        this.messageHandler = messageHandler;
        reciever = new Client_MessageReciever(connection, messageHandler);
        this.data = data;
        this.config = config;
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
    public void Update(float deltaTime)
    {
        connection.Update(deltaTime); //TJEK AT BESKEDEN SENDES HERI!!!
        reciever.CheckForServerResponseMessages();
        if(ClockUpdateEvent != null)
            ClockUpdateEvent.Invoke(config.GetClock().GetTimeInMiliSeconds());
    }

    internal void KillConnection()
    {
        connection.GetSocket().Close();
    }
}