using System;

namespace DatabaseConnector
{
    public class MessageHandler_ClientRequest_Register : IMessageHandlerCommand
    {
        private ServerCore server;

        public MessageHandler_ClientRequest_Register(ServerCore server)
        {
            this.server = server;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ClientRequest_Register);
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            var data = (Message_ClientRequest_Register)objData;
            var result = DBLogin.RegisterPlayer(data.name, data.password);
            var msg = new Message_ServerResponse_Register(result);
            server.messageSender.Send(msg, client);
        }
    }
}