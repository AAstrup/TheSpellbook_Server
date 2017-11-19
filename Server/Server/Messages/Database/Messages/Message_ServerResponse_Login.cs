using System;

[Serializable]
public class Message_ServerResponse_Login
{
    public DBPlayerProfile player;
    public bool loginSucceded;

    /// <summary>
    /// Player is null in case that the login failed
    /// </summary>
    /// <param name="player"></param>
    public Message_ServerResponse_Login(DBPlayerProfile player)
    {
        loginSucceded = player != null;
        if (loginSucceded)
            this.player = player;
    }
}
