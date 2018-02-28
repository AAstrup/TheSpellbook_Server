public interface IServerEventHandler
{
    /// <summary>
    /// Event triggered when player leaves
    /// </summary>
    /// <param name="server_ServerClient">Client that left</param>
    void ClientLeft(Server_ServerClient server_ServerClient, ServerCore serverCore);
    void SubScribeClientLeft(ServerEventHandlerDelegates.ClientLeftEvent delegateEvent);
}