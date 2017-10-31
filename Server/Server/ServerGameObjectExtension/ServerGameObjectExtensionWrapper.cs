using ClientServerSharedGameObjectMessages;
using ServerGameObjectExtension.Factory;
using System;
using System.Collections.Generic;
using System.Text;
using Server;
using ServerGameObjectExtension.Handlers;

namespace ServerGameObjectExtension
{
    public class ServerGameObjectExtensionWrapper : IServerExtension
    {
        private Factory_ServerCommand_CreateGameObject factoryCreateGameObject;

        public ServerGameObjectExtensionWrapper()
        { 
            factoryCreateGameObject = new Factory_ServerCommand_CreateGameObject();
        }

        List<IMessageHandlerCommand> IServerExtension.CreateMessageHandlers(ServerCore server)
        {
            List<IMessageHandlerCommand> msgHandler = new List<IMessageHandlerCommand>();
            SpellGUIDGenerator spellGUIDGenerator = new SpellGUIDGenerator();
            msgHandler.Add(new MessageHandler_ClientRequest_CreateSpellWithDirection(server, spellGUIDGenerator));
            msgHandler.Add(new MessageHandler_ClientRequest_CreateSpellInStaticPosition(server, spellGUIDGenerator));
            return msgHandler;
        }

        public List<object> GetMessagesForClientSetup(Server_ServerClient client)
        {
            List<object> objs = new List<object>();
            objs.Add(CreateGameObject(client));
            return objs;
        }

        /// <summary>
        /// Creates a message for a GameObject creation
        /// </summary>
        /// <param name="client">The client that this GameObject belong to</param>
        /// <returns></returns>
        private Message_ServerCommand_CreateGameObject CreateGameObject(Server_ServerClient client)
        {
            return factoryCreateGameObject.Create_Message_ServerCommand_CreateGameObject(client.info.GUID);
        }
    }
}
