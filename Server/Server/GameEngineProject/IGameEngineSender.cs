public interface IGameEngineSender
{
    void Send(int playerID, object msg);
    void Win(int playerGUID);
}
