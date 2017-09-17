using System;
using System.Collections.Generic;

[Serializable]
public class Message_Response_GameState
{
    public Shared_InGame_PlayerInfo me;
    public Shared_InGame_PlayerInfo opp;

    public Message_Response_GameState(Shared_InGame_PlayerInfo me, Shared_InGame_PlayerInfo opp)
    {
        this.me = me;
        this.opp = opp;
    }
}
