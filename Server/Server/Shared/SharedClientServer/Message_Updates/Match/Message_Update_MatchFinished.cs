using SharedClientServerGameObjectMessages;
using System;

[Serializable]
public class Message_Update_MatchFinished
{
    public bool won;
    public PlayerScore playerScore;

    public Message_Update_MatchFinished(bool won, PlayerScore playerScore)
    {
        this.won = won;
        this.playerScore = playerScore;
    }
}
