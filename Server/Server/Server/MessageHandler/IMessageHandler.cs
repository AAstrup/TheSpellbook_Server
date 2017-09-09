/// <summary>
/// Message Handler responsible for handling messages recieved from the connection
/// </summary>
public interface IMessageHandler
{
    void Handle(object data, Server_ServerClient client);
}