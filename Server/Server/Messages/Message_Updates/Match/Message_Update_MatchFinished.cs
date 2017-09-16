using System;

[Serializable]
public class Message_Update_MatchFinished
{
    public bool won;

    public Message_Update_MatchFinished(bool won)
    {
        this.won = won;
    }
}
