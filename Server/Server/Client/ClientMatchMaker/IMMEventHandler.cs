public interface IMMEventHandler
{
    void StartMenu();
    void InQueue(Message_Response_InQueue data);
    void QueueReady(Counter counter);
    void MatchFound();
    void StartedConnecting();
    void ConnectingSuccesful();
    void ConnectingFailed();
    void ConnectingAttempt(int connectionAttempts);
}