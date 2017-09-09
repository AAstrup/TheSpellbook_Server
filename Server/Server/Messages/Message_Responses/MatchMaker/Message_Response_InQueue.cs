using System;
using System.Collections.Generic;

/// <summary>
/// Whenever a player enters or leaves the Game Information will be updated by the server and 
/// sent to the clients
/// </summary>
[Serializable]
public class Message_Response_InQueue
{
    public string message;

    public Message_Response_InQueue(string message)
    {
        this.message = message;
    }
}