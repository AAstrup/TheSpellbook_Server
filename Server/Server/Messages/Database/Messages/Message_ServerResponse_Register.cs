using System;
using System.Collections.Generic;

[Serializable]
public class Message_ServerResponse_Register
{
    public bool succeded;
    public DBPlayerProfile profile;
    public string message;

    public Message_ServerResponse_Register(KeyValuePair<DBPlayerProfile, string> result)
    {
        succeded = result.Key != null;
        profile = result.Key;
        message = result.Value;
    }
}
