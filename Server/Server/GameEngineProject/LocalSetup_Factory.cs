using System;

public class LocalSetup_Factory
{
    public LocalSetup_Factory()
    {
    }

    public GameEngineSender_LocalSender BuildLocalSender(Shared_PlayerInfo player)
    {
        GameEngineSender_LocalSender sender = new GameEngineSender_LocalSender();
        Shared_PlayerInfo bot = BuildBot();
        GameEngine gameEngine = new GameEngine(sender,player,bot);
        return sender;
    }

    /// <summary>
    /// WIP
    /// </summary>
    /// <returns></returns>
    private Shared_PlayerInfo BuildBot()
    {
        return new Shared_PlayerInfo()
        {
            GUID = -1,
            name = "Bot"
        };
    }
}