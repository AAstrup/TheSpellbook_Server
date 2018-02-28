using System;

/// <summary>
/// The class responsible for starting a client and connecting to the matchmaker
/// This requires no previous setup
/// This class has to be updated to check for communication
/// </summary>
public class MatchMakerClient : IUnityComponentResetable, IConnectionResultHandler
{
    private MMMessageHandler messageHandler;
    Client client;
    private IClientConfig clientConfig;
    private PersistentData persistentData;
    private ILogger logger;
    internal IMMEventHandler eventHandler;
    private UpdateController updateController;

    public MatchMakerClient(IClientConfig clientConfig,IMMEventHandler eventHandler,ILogger logger, PersistentData persistentData,string name)
    {
        this.clientConfig = clientConfig;
        this.persistentData = persistentData;
        this.logger = logger;
        this.eventHandler = eventHandler;
        eventHandler.StartMenu();
        updateController = new UpdateController();
        StartOnlineClient(name);
    }

    /// <summary>
    /// Starts the client for matchmaking in online mode
    /// </summary>
    private void StartOnlineClient(string inputName)
    {
        persistentData.PlayerInfo = new Shared_PlayerInfo() { username = "NOT CORRECT" };
        messageHandler = new MMMessageHandler(this,updateController,logger,persistentData);
        client = new Client(this, ClientConnectionInfo.MatchMakerConnectionInfo(clientConfig), messageHandler,persistentData,logger,this, clientConfig);
    }

    /// <summary>
    /// Starts as the role of a client
    /// </summary>
    private void StartOfflineClient()
    {
        throw new NotImplementedException();
        //AppConfig.GetPersistentData().PlayerInfo = new Shared_PlayerInfo() { name = AppConfig.GetName() };
        //SceneManager.LoadScene(AppConfig.OfflineSceneName);
    }

    /// <summary>
    /// Update the target role
    /// </summary>
    public void Update(float deltaTime)
    {
        updateController.Update(deltaTime);
        if (client != null)
            client.Update(deltaTime);
    }

    public void LeaveQueue()
    {
        Message_Request_LeaveQueue msg = new Message_Request_LeaveQueue()
        {
            info = persistentData.PlayerInfo
        };
        client.sender.Send(msg);
    }

    public void Clean()
    {
        messageHandler.Clean();
        client = null;
    }

    public void FinishForReadyQueue()
    {
        Message_ClientResponse_ReadyCheck msg = new Message_ClientResponse_ReadyCheck()
        {
            readyCheckGUID_FromServerReadyCheck = messageHandler.GetReadyCheckGUID()
        };
        client.sender.Send(msg);
        logger.Log("Sending " + msg.GetType());
    }

    public void Setup_Succesful()
    {
        messageHandler.Init(client);
        client.Register();
        eventHandler.StartedConnecting();
        eventHandler.ConnectingSuccesful();
    }

    public void Setup_Failed()
    {
        eventHandler.ConnectingFailed();
    }

    public void Setup_ConnectingAttempt(int connectionAttempts)
    {
        eventHandler.ConnectingAttempt(connectionAttempts);
    }
}
