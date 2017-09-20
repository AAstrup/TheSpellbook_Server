/// <summary>
/// The sender used by the game engine to respond to messages
/// Has a local and network implementation
/// </summary>
public interface IGameEngineSender
{
    /// <summary>
    /// Send a message
    /// </summary>
    /// <param name="playerID">GUID of player</param>
    /// <param name="msg">Serializable object</param>
    void Send(int playerID, object msg);

    /// <summary>
    /// Called when one player has won
    /// </summary>
    /// <param name="playerGUID"></param>
    void Event_Win(int playerGUID);
}
