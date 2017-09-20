using Match;
using System.Collections.Generic;

public class GameEngineSender_MatchSender : IGameEngineSender
{
    public Dictionary<int, Shared_PlayerInfo> GUIDToPlayerClient;
    private ServerCore server;
    private bool gameHasEnded;

    public GameEngineSender_MatchSender(Dictionary<int, Shared_PlayerInfo> GUIDToPlayerClient)
    {
        this.GUIDToPlayerClient = GUIDToPlayerClient;
    }

    /// <summary>
    /// Initialize with a server
    /// </summary>
    /// <param name="server"></param>
    public void Init(ServerCore server)
    {
        this.server = server;
    }

    /// <summary>
    /// Tells if the game has ended
    /// </summary>
    /// <returns></returns>
    public bool HasGameEnded() { return gameHasEnded; }

    /// <summary>
    /// Sends a message to a specific player
    /// </summary>
    /// <param name="playerGUID">Player GUID</param>
    /// <param name="msg">Serializable message</param>
    public void Send(int playerGUID, object msg)
    {
        foreach (var client in server.clientManager.GetClients())
        {
            if (client.info.GUID == playerGUID)
            {
                server.messageSender.Send(msg, client);
                return;
            }
        }
    }

    /// <summary>
    /// When a player wins this method is called
    /// </summary>
    /// <param name="playerGUID">GUID of player winning</param>
    public void Event_Win(int playerGUID)
    {
        foreach (KeyValuePair<int, Shared_PlayerInfo> item in GUIDToPlayerClient)
        {
            if (item.Key == playerGUID)
            {
                Message_Update_MatchFinished winnerMsg = new Message_Update_MatchFinished(true);
                Send(item.Key, winnerMsg);
            }
            else
            {
                Message_Update_MatchFinished loserMsg = new Message_Update_MatchFinished(false);
                Send(item.Key, loserMsg);
            }
        }
        gameHasEnded = true;
    }
}