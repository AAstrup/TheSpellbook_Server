using System;

public class Handler_Update_MatchFinished : IMessageHandlerCommandClient
{
    private IMatchEventHandler matchEventHandler;
    private ILogger logger;

    public Handler_Update_MatchFinished(ILogger logger,IMatchEventHandler matchEventHandler)
    {
        this.matchEventHandler = matchEventHandler;
        this.logger = logger;
    }

    public void Handle(object objdata)
    {
        var data = (Message_Update_MatchFinished)objdata;
        logger.Log("data.won " + data.won);
        matchEventHandler.MatchFinished(data);
    }
}