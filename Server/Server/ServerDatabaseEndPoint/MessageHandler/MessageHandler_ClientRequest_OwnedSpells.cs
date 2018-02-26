using System;

namespace DatabaseConnector
{
    public class MessageHandler_ClientRequest_OwnedSpells : IMessageHandlerCommand
    {
        private ServerCore server;

        public MessageHandler_ClientRequest_OwnedSpells(ServerCore server)
        {
            this.server = server;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ClientRequest_DBOwnedSpells);
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            var data = (Message_ClientRequest_DBOwnedSpells)objData;
            var dbData = DBOwnedSpells.GetPlayer(data.username, data.hashedPassword);
            Message_ServerResponse_DBOwnedSpells msg = new Message_ServerResponse_DBOwnedSpells()
            {
                ownedSpells = dbData
            };
            server.messageSender.Send(msg, client);
        }
    }
}