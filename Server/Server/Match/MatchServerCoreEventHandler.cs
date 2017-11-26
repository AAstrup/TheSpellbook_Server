namespace Match
{
    internal class MatchServerCoreEventHandler : IServerEventHandler
    {
        public event ServerEventHandlerDelegates.ClientLeftEvent clientLeftEvent;
        public void ClientLeft(Server_ServerClient server_ServerClient,ServerCore serverCore)
        {
            clientLeftEvent.Invoke(server_ServerClient, serverCore);
        }

        public void SubScribeClientLeft(ServerEventHandlerDelegates.ClientLeftEvent delegateEvent)
        {
            clientLeftEvent += delegateEvent;
        }
    }
}