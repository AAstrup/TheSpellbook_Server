using System;
using System.Collections.Generic;

[Serializable]
public class Message_Response_GameState
{
    public Shared_PlayerInfo requestingPlayer;
    public List<Shared_PlayerInfo> AllPlayers;

    public Message_Response_GameState(Shared_PlayerInfo requestingPlayer, List<Shared_PlayerInfo> AllPlayers)
    {
        this.requestingPlayer = requestingPlayer;
        this.AllPlayers = AllPlayers;
    }
}
 