public interface IClient_MessageSender
{
    /// <summary>
    /// Register at the server with a playerinfo
    /// </summary>
    /// <param name="playerInfo"></param>
    void RegisterAtServer(Shared_PlayerInfo playerInfo);
    /// <summary>
    /// Send a serializable message
    /// </summary>
    /// <param name="objData"></param>
    void Send(object objData);
}