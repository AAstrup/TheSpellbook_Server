using System;

/// <summary>
/// A request from client to the server for joining the game
/// This is send after the tcp connection is established!
/// </summary>
[Serializable]
public class Message_Request_JoinQueue
{
    public Shared_PlayerInfo playerInfo;
    public Message_Request_JoinQueue(Shared_PlayerInfo playerInfo)
    {
        this.playerInfo = playerInfo;
    }
}