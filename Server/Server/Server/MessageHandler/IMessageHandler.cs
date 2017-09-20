/// <summary>
/// Message Handler responsible for handling messages recieved from the connection
/// </summary>
namespace Server
{
    public interface IMessageHandler
    {
        void Handle(object data, Server_ServerClient client);
    }
}