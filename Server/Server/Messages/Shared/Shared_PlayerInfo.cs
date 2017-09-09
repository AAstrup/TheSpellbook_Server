using System;

/// <summary>
/// Both player and server saves this class for use.
/// This class represents a single player and his info.
/// This class is send from clients during establishment of the match
/// and saved in the server for future use
/// </summary>
[Serializable]
public class Shared_PlayerInfo
{
    /// <summary>
    /// Name of the player, decided by the player
    /// </summary>
    public string name;
    /// <summary>
    /// Skill raiting of the play
    /// </summary>
    public int skillRaiting;
    /// <summary>
    /// GUID on the match of the player, given by the server
    /// </summary>
    public int GUID;
}