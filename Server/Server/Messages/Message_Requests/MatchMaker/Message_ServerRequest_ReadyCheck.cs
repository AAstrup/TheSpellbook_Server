using System;

/// <summary>
/// Send to client to check if they are ready for the match
/// </summary>
[Serializable]
public class Message_ServerRequest_ReadyCheck
{
    private static int readyCheckGUID;

    public int msgReadyCheckGUID;
    public int duration;

    public Message_ServerRequest_ReadyCheck()
    {
        msgReadyCheckGUID = GetGUID();
        duration = 10;
    }

    /// <summary>
    /// Read GUID of a msg
    /// </summary>
    /// <returns></returns>
    public int ReadGUID() { return msgReadyCheckGUID; }

    /// <summary>
    /// Get a guid for the msg
    /// </summary>
    /// <returns></returns>
    public static int GetGUID()
    {
        return readyCheckGUID++;
    }
}
