public interface IMatchEventHandler
{
    void SetUIState_Loading();
    void SetUIState_JoiningGame();
    void JoinedGame(Message_Response_GameAllConnected data);
    void MatchFinished(Message_Update_MatchFinished data);
    void ConnectedSuccesful();
    void ConnectedFailed();
    void ConnectedAttempt(int connectionAttempts);
}