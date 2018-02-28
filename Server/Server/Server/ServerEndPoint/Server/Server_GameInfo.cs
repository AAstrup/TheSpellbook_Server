using System;
using System.Collections.Generic;

public class Server_GameInfo
{
    List<Shared_PlayerInfo> players;

    public Server_GameInfo()
    {
        players = new List<Shared_PlayerInfo>();
    }
    public void AddPlayer(Shared_PlayerInfo playerInfo)
    {
        players.Add(playerInfo);
    }

    public List<Shared_PlayerInfo> GetPlayers()
    {
        return players;
    }
}