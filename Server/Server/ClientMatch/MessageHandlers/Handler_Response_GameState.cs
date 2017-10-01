using System;

internal class Handler_Response_GameState : IMessageHandlerCommandClient
{
    private ILogger logger;
    private IMatchEventHandler matchEventHandler;

    public Handler_Response_GameState(ILogger logger,IMatchEventHandler matchEventHandler)
    {
        this.logger = logger;
        this.matchEventHandler = matchEventHandler;
    }

    public void Handle(object objdata)
    {
        var data = (Message_Response_GameState)objdata;
        foreach (var item in data.AllPlayers)
        {
            logger.Log("Player " + item.name + " is in this game");
        }
        matchEventHandler.JoinedGame(data);
    }
}