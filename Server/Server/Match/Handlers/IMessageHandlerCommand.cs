/// <summary>
/// Command pattern for message handlers
/// </summary>
public interface IMessageHandlerCommand
{
    void Handle(object objData, Server_ServerClient client);
}