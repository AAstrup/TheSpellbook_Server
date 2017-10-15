using System;
using System.Collections.Generic;

public class MatchClient : IUnityComponentResetable
{
    private UpdateController updateController;
    private ILogger logger;
    private PersistentData persistentData;
    private IMatchEventHandler EventHandler;
    private MatchMessageHandler messageHandler;
    private Client client;

    /// <summary>
    /// Creates a client without adding additional handlers
    /// All default handlers trigger event in the IMatchEventHandler
    /// </summary>
    /// <param name="EventHandler">The events the default message handlers triggers</param>
    /// <param name="logger">Logger to write logs</param>
    /// <param name="persistentData">Persistent data expected to be setup by now</param>
    public MatchClient(IMatchEventHandler EventHandler, ILogger logger, PersistentData persistentData) : 
        this(EventHandler,logger,persistentData,new Dictionary<Type, IMessageHandlerCommandClient>())
    {
    }

    /// <summary>
    /// Creates a match client with additional message handlers
    /// </summary>
    /// <param name="EventHandler">The events the default message handlers triggers</param>
    /// <param name="logger">Logger to write logs</param>
    /// <param name="persistentData">Persistent data expected to be setup by now</param>
    /// <param name="msgTypeToMsgHandler">The message handler provided. Keys are the type of message classes, and values are the handler for the respectable message</param>
    public MatchClient(IMatchEventHandler EventHandler,ILogger logger,PersistentData persistentData, Dictionary<Type, IMessageHandlerCommandClient> msgTypeToMsgHandler)
    {
        this.logger = logger;
        this.persistentData = persistentData;
        this.EventHandler = EventHandler;
        EventHandler.SetUIState_Loading();
        updateController = new UpdateController();
        StartOnlineClient(msgTypeToMsgHandler);
    }

    /// <summary>
    /// Starts the client for a match in online mode
    /// </summary>
    /// <param name="msgTypeToMsgHandler">Message handlers provided from the game</param>
    private void StartOnlineClient(Dictionary<Type, IMessageHandlerCommandClient> msgTypeToMsgHandler)
    {
        messageHandler = new MatchMessageHandler(logger,EventHandler, msgTypeToMsgHandler);
        var connectioninfo = new ClientConnectionInfo(persistentData.port, persistentData.ip);
        client = new Client(this, connectioninfo, messageHandler, persistentData, logger);
        messageHandler.Init(client);
        Message_Request_JoinGame request = new Message_Request_JoinGame()
        {
            info = persistentData.PlayerInfo
        };
        client.sender.Send(request);
        EventHandler.SetUIState_JoiningGame();
    }

    /// <summary>
    /// Updates the client
    /// Sending and recieving messages
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        updateController.Update(deltaTime);
        if (client != null)
            client.Update();
    }

    public void Clean()
    {
    }

    public void Send(object serializableObj)
    {
        client.sender.Send(serializableObj);
    }

    /// <summary>
    /// Handle a message as if recieved from another client
    /// </summary>
    /// <param name="serializableObj"></param>
    public void HandleLocalMessage(object serializableObj)
    {
        messageHandler.Handle(serializableObj);
    }
}