/// <summary>
/// Empty shell for servercore event handling
/// </summary>
public class ServerCoreShellHandler : IServerEventHandler
{
    public void ClientLeft(Server_ServerClient server_ServerClient, ServerCore serverCore)
    {
    }

    public void SubScribeClientLeft(ServerEventHandlerDelegates.ClientLeftEvent delegateEvent)
    {
    }
}