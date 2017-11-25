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

    public Type GetMessageTypeSupported()
    {
        return typeof(Message_Response_GameAllConnected);
    }

    public void Handle(object objdata)
    {
        var data = (Message_Response_GameAllConnected)objdata;
        foreach (var item in data.AllPlayers)
        {
            logger.Log("Player " + item.username + " is in this game");
        }
        matchEventHandler.JoinedGame(data);
    }
}