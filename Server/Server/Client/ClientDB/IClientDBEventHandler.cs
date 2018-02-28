namespace ClientDB
{
    public interface IClientDBEventHandler
    {
        void ConnectingSuccesful();
        void ConnectingFailed();
        void ConnectingAttempt(int connectionAttempts);
        void LoginResponse(Message_ServerResponse_Login objData);
        void RegisterAndLoginResponse(Message_ServerResponse_Register objData);
    }
}