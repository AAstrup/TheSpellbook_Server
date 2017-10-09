using ClientServerSharedGameObjectMessages;
using Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGameObjectExtension.Handlers
{
    public class MessageHandler_ClientRequest_CreateSpell : IMessageHandlerCommand
    {
        public int SpellGUID;
        private ServerCore core;

        public MessageHandler_ClientRequest_CreateSpell(ServerCore core)
        {
            SpellGUID = 1;
            this.core = core;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ClientRequest_CreateSpell);
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            var data = (Message_ClientRequest_CreateSpell)objData;
            Message_ServerResponse_CreateSpell response = new Message_ServerResponse_CreateSpell()
            {
                request = data,
                spellGmjID = GenerateGUID()
            };
            core.messageSender.SendToAll(response, core.clientManager.GetClients());
        }

        private int GenerateGUID()
        {
            return SpellGUID++;
        }
    }
}
