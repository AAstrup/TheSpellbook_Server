using System;

[Serializable]
public class Message_ServerResponse_Login
{
    public DBProfile_Login profile;
    public bool loginSucceded;

    /// <summary>
    /// Player is null in case that the login failed
    /// </summary>
    /// <param name="player"></param>
    public Message_ServerResponse_Login(DBProfile_Login player)
    {
        loginSucceded = player != null;
        if (loginSucceded)
            this.profile = player;
    }
}
