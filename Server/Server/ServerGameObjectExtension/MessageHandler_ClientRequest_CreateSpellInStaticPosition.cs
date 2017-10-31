using ClientServerSharedGameObjectMessages;
using System;

namespace ServerGameObjectExtension
{
    internal class MessageHandler_ClientRequest_CreateSpellInStaticPosition : IMessageHandlerCommand
    {
        private ServerCore server;
        private SpellGUIDGenerator spellGUIDGenerator;

        public MessageHandler_ClientRequest_CreateSpellInStaticPosition(ServerCore server, SpellGUIDGenerator spellGUIDGenerator)
        {
            this.server = server;
            this.spellGUIDGenerator = spellGUIDGenerator;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ClientRequest_CreateSpellInStaticPosition);
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            var data = (Message_ClientRequest_CreateSpellInStaticPosition)objData;
            Message_ServerResponse_CreateSpellInStaticPosition response = new Message_ServerResponse_CreateSpellInStaticPosition()
            {
                request = data,
                spellID = spellGUIDGenerator.GenerateGUID()
            };
            server.messageSender.SendToAll(response, server.clientManager.GetClients());
        }
    }
}