using System;

internal class Client_MessageHandler_InQueue : IMessageHandlerCommandClient
{
    private ILogger logger;
    private PersistentData persistentData;
    private IMMEventHandler guiHandler;

    public Client_MessageHandler_InQueue(ILogger logger, PersistentData persistentData, IMMEventHandler guiHandler)
    {
        this.logger = logger;
        this.persistentData = persistentData;
        this.guiHandler = guiHandler;
    }

    public Type GetMessageTypeSupported()
    {
        return typeof(Message_Response_InQueue);
    }

    public void Handle(object objdata)
    {
        var data = (Message_Response_InQueue)objdata;
        logger.Log("Message_Response_InQueue :" + data.message);
        persistentData.PlayerInfo = data.playerinfoWithGUID;
        guiHandler.InQueue(data);
    }
}