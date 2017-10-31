using ClientServerSharedGameObjectMessages;
using Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGameObjectExtension.Handlers
{
    public class MessageHandler_ClientRequest_CreateSpellWithDirection : IMessageHandlerCommand
    {
        private ServerCore server;
        private SpellGUIDGenerator spellGUIDGenerator;

        public MessageHandler_ClientRequest_CreateSpellWithDirection(ServerCore server, SpellGUIDGenerator spellGUIDGenerator)
        {
            this.server = server;
            this.spellGUIDGenerator = spellGUIDGenerator;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ClientRequest_CreateSpellWithDirection);
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            var data = (Message_ClientRequest_CreateSpellWithDirection)objData;
            Message_ServerResponse_CreateSpellWithDirection response = new Message_ServerResponse_CreateSpellWithDirection()
            {
                request = data,
                spellID = spellGUIDGenerator.GenerateGUID()
            };
            server.messageSender.SendToAll(response, server.clientManager.GetClients());
        }
    }
}
