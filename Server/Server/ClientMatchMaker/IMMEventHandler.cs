public interface IMMEventHandler
{
    void StartMenu();
    void Connecting();
    void InQueue(Message_Response_InQueue data);
    void QueueReady(Counter counter);
    void MatchFound();
}