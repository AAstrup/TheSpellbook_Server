namespace Match
{
    /// <summary>
    /// When running local, there is need to send messages
    /// as a result classes are empty
    /// </summary>
    public class GameEngineSender_LocalSender : IGameEngineSender
    {
        public void Send(int playerID, object msg)
        {
        }
    }
}