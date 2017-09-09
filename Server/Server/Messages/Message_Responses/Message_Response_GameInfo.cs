using System;
using System.Collections.Generic;

/// <summary>
/// Whenever a player enters or leaves the Game Information will be updated by the server and 
/// sent to the clients
/// </summary>
[Serializable]
public class Message_Response_GameInfo
{
    public List<Shared_PlayerInfo> players;

    public Message_Response_GameInfo(List<Shared_PlayerInfo> players)
    {
        this.players = players;
    }
}