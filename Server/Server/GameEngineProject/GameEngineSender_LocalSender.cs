using System;
/// <summary>
/// When running local, there is no need to send messages
/// as a result methods are empty
/// </summary>
public class GameEngineSender_LocalSender : IGameEngineSender
{
    public Action<int> playerWon;
    public GameEngineSender_LocalSender()
    {
    }
    public void Send(int playerID, object msg)
    {
    }

    public void Win(int playerGUID)
    {
        playerWon?.Invoke(playerGUID);
    }
}
