using System;

[Serializable]
public class Message_ClientCommand_GameObjectDied
{
    public int DyingPlayerGUID;
    /// <summary>
    /// Null in case of no killer
    /// </summary>
    public int? KillerPlayerGUID;

}