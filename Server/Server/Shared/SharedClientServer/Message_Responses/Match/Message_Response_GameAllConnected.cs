using System;
using System.Collections.Generic;

/// <summary>
/// Message sent to all players when all players are connected to the game server
/// </summary>
[Serializable]
public class Message_Response_GameAllConnected
{   
    public Shared_PlayerInfo requestingPlayer;
    /// <summary>
    /// This has been lag compensated
    /// </summary>
    public double gameClockTime;
    public List<Shared_PlayerInfo> AllPlayers;

    public Message_Response_GameAllConnected(Shared_PlayerInfo requestingPlayer, List<Shared_PlayerInfo> AllPlayers,double gameClockTime)
    {
        this.requestingPlayer = requestingPlayer;
        this.AllPlayers = AllPlayers;
        this.gameClockTime = gameClockTime;
    }
}
 