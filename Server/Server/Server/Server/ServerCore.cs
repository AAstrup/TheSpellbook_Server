using Server;
/// <summary>
/// The class that starts everything up for the server
/// Everything is started in its constructor
/// </summary>
public class ServerCore
{
    public Server_GameInfo gameInfo;
    public Server_ClientManager clientManager;
    public IMessageHandler messageHandler;
    public Server_Connection connection;
    public Server_MessageReciever messageReciever;
    public Server_MessageSender messageSender;
    private ConnectionInfo connectionInfo;

    public ServerCore(IMessageHandler messageHandler,ConnectionInfo connectionInfo)
    {
        gameInfo = new Server_GameInfo();
        clientManager = new Server_ClientManager(this);
        this.connectionInfo = connectionInfo;
        this.messageHandler = messageHandler;
        connection = new Server_Connection(clientManager, connectionInfo);
        messageReciever = new Server_MessageReciever(connection,messageHandler);
        messageSender = new Server_MessageSender(clientManager);
    }

    /// <summary>
    /// This method checks and handles messages from the client
    /// </summary>
    public void Update()
    {
        messageReciever.CheckForClientRequestMessages();
    }
}