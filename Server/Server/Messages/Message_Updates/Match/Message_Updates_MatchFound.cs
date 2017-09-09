using System;

/// <summary>
/// Message send when match between two clients
/// Send only to the selected clients
/// </summary>
[Serializable]
public class Message_Updates_MatchFound
{
    /// <summary>
    /// Port of the match to connect to
    /// </summary>
    public int port;
    /// <summary>
    /// Ip of the match to connect to
    /// </summary>
    public string ip;
}