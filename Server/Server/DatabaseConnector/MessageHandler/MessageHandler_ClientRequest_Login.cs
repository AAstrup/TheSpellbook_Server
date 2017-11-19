using System;

namespace DatabaseConnector
{
    public class MessageHandler_ClientRequest_Login : IMessageHandlerCommand
    {
        private ServerCore server;

        public MessageHandler_ClientRequest_Login(ServerCore server)
        {
            this.server = server;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ClientRequest_Login);
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            var data = (Message_ClientRequest_Login)objData;
            var player = DBLogin.GetPlayer(data.name,data.password);
            var msg = new Message_ServerResponse_Login(player);
            server.messageSender.Send(msg, client);
        }
    }
}