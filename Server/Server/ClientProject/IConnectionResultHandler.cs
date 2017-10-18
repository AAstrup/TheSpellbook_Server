public interface IConnectionResultHandler
{
    void Setup_Succesful();
    void Setup_Failed();
    void Setup_ConnectingAttempt(int connectionAttempts);
}