public class TurnManager
{
    private Shared_PlayerInfo p1;
    private Shared_PlayerInfo p2;
    private Shared_PlayerInfo currentPlayersTurn;

    public TurnManager(Shared_PlayerInfo p1, Shared_PlayerInfo p2)
    {
        this.p1 = p1;
        this.p2 = p2;
        currentPlayersTurn = p1;
    }

    public Shared_PlayerInfo GetCurrentPlayersTurn()
    {
        return currentPlayersTurn;
    }

    public void NextTurn()
    {
        if (currentPlayersTurn == p1)
            currentPlayersTurn = p2;
        else
            currentPlayersTurn = p1;
    }
}