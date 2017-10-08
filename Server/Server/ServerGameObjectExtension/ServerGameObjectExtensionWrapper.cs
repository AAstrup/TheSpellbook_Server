using ClientServerSharedGameObjectMessages;
using ServerGameObjectExtension.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerGameObjectExtension
{
    public class ServerGameObjectExtensionWrapper : IServerExtension
    {
        private Factory_ServerCommand_CreateGameObject factoryCreateGameObject;

        public ServerGameObjectExtensionWrapper()
        { 
            factoryCreateGameObject = new Factory_ServerCommand_CreateGameObject();
        }

        public object GetMessageForClientSetup(Server_ServerClient client)
        {
            return CreateGameObject(client);
        }

        /// <summary>
        /// Creates a GameObject
        /// </summary>
        /// <param name="client">The client that this has </param>
        /// <returns></returns>
        private Message_ServerCommand_CreateGameObject CreateGameObject(Server_ServerClient client)
        {
            return factoryCreateGameObject.Create_Message_ServerCommand_CreateGameObject(client.info.GUID);
        }
    }
}
