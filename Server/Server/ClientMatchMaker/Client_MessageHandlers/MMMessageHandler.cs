using System;
using System.Runtime.InteropServices;

/// <summary>
/// Responsible for handling responses from the sever as objects and
/// finding a suitable handler for the object
/// </summary>
public class MMMessageHandler : IMessageHandler
{
    private MessageCommandHandlerClient commandHandler;

    //Saved for Initialize
    private Client client;
    private PersistentData persistentData;
    private ILogger logger;
    private MatchMakerClient networkTransmitter;
    private UpdateController updateController;

    //Saved for data gathering later
    private Client_MessageHandler_ReadyCheck handler_ReadyCheck;

    public MMMessageHandler(MatchMakerClient networkTransmitter,UpdateController updateController,ILogger logger,PersistentData persistentData)
    {
        commandHandler = new MessageCommandHandlerClient();
        this.persistentData = persistentData;
        this.logger = logger;
        this.networkTransmitter = networkTransmitter;
        this.updateController = updateController;
    }

    public void Init(Client client)
    {
        this.client = client;

        commandHandler.Add(typeof(Message_Response_InQueue), new Client_MessageHandler_InQueue(logger, persistentData, networkTransmitter.eventHandler));
        commandHandler.Add(typeof(Message_Update_MatchFound), new Client_MessageHandler_MatchFound(networkTransmitter.eventHandler, client, logger, persistentData));

        handler_ReadyCheck = new Client_MessageHandler_ReadyCheck(updateController, client, networkTransmitter);
        commandHandler.Add(typeof(Message_ServerRequest_ReadyCheck), handler_ReadyCheck);
    }

    public void Handle(object data)
    {
        if (commandHandler.Contains(data.GetType()))
            commandHandler.Execute(data.GetType(), data);
        else
        {
            logger.Log("Data type UKNOWN! Type: " + data.GetType().ToString());
        }
    }

    internal void Clean()
    {
        client = null;
    }

    internal int GetReadyCheckGUID()
    {
        return handler_ReadyCheck.readyCheck.msgReadyCheckGUID;
    }
}