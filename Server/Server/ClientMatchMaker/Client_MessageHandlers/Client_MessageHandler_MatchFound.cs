
using System;

internal class Client_MessageHandler_MatchFound : IMessageHandlerCommandClient
{
    private IMMEventHandler eventHandler;
    private PersistentData persistentData;
    private ILogger logger;
    private Client client;

    public Client_MessageHandler_MatchFound(IMMEventHandler eventHandler,Client client,ILogger logger,PersistentData persistentData)
    {
        this.eventHandler = eventHandler;
        this.persistentData = persistentData;
        this.logger = logger;
        this.client = client;
    }

    public void Handle(object objdata)
    {
        var data = (Message_Update_MatchFound)objdata; 
        logger.Log("Recieved Message_Updates_MatchFound");
        persistentData.ip = data.ip;
        persistentData.port = data.port;
        eventHandler.MatchFound();
    }
}