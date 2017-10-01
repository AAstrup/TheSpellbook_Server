public class MatchClient : IUnityComponentResetable
{
    private UpdateController updateController;
    private ILogger logger;
    private PersistentData persistentData;
    private IMatchEventHandler EventHandler;
    private MatchMessageHandler messageHandler;
    private Client client;

    public MatchClient(IMatchEventHandler EventHandler,ILogger logger,PersistentData persistentData)
    {
        this.logger = logger;
        this.persistentData = persistentData;
        this.EventHandler = EventHandler;
        EventHandler.SetUIState_Loading();
        updateController = new UpdateController();
        StartOnlineClient();
    }

    /// <summary>
    /// Starts the client for a match in online mode
    /// </summary>
    private void StartOnlineClient()
    {
        messageHandler = new MatchMessageHandler(logger,EventHandler);
        client = new Client(this, new ClientConnectionInfo(persistentData.port, persistentData.ip), messageHandler, persistentData, logger);
        messageHandler.Init(client);
        Message_Request_JoinGame request = new Message_Request_JoinGame()
        {
            info = persistentData.PlayerInfo
        };
        client.sender.Send(request);
        EventHandler.SetUIState_JoiningGame();
    }

    public void Update(float deltaTime)
    {
        updateController.Update(deltaTime);
        if (client != null)
            client.Update();
    }

    public void Clean()
    {
    }
}