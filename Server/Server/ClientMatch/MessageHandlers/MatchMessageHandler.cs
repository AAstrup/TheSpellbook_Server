
using System;
using System.Collections.Generic;

public class MatchMessageHandler : IMessageHandler
{
    private IMatchEventHandler matchEventHandler;
    private MessageCommandHandlerClient commandHandler;
    ILogger logger;

    public MatchMessageHandler(ILogger logger,IMatchEventHandler matchEventHandler, Dictionary<Type, IMessageHandlerCommandClient> msgHandler)
    {
        this.logger = logger;
        logger.Log("Match messagehandler started!");

        this.matchEventHandler = matchEventHandler;
        commandHandler = new MessageCommandHandlerClient(msgHandler);
        commandHandler.Add( new Handler_Response_GameState(logger,matchEventHandler));
        commandHandler.Add(new Handler_Update_MatchFinished(logger,matchEventHandler));
    }

    public void Handle(object data)
    {
        logger.Log("Match messagehandler got data of type " + data.GetType());
        if (commandHandler.Contains(data.GetType()))
            commandHandler.Execute(data.GetType(), data);
        else
        {
            logger.Log("Data type UKNOWN! Type: " + data.GetType().ToString());
        }
    }

    internal void Init(Client client)
    {
        commandHandler.Add(new Handler_Response_Ping(matchEventHandler,client.sender));
    }
}