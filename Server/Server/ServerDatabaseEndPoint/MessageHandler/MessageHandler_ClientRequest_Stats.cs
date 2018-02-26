using System;

namespace DatabaseConnector
{
    internal class MessageHandler_ClientRequest_Stats : IMessageHandlerCommand
    {
        private ServerCore server;

        public MessageHandler_ClientRequest_Stats(ServerCore server)
        {
            this.server = server;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ClientRequest_DBStats);
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            var data = (Message_ClientRequest_DBStats)objData;
            var dbData = DBStats.GetPlayer(data.username,data.hashedPassword);
            var msg = new Message_ServerResponse_DBStats() {
                spells = dbData
            };
            server.messageSender.Send(msg, client);
        }
    }
}