using ClientServerSharedGameObjectMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerGameObjectExtension.Factory
{
    public class Factory_ServerCommand_CreateGameObject
    {
        //Starts at GUID 1, indicating 0 to be an error, or not set
        public int GmjGUID;

        public Factory_ServerCommand_CreateGameObject()
        {
            GmjGUID = 1;
        }

        public Message_ServerCommand_CreateGameObject Create_Message_ServerCommand_CreateGameObject(int ownerGUID)
        {
            var newTransform = new Message_Common_Transform()
            {
                xPos = 0,
                yPos = 0,
                zPos = 0,
                xRotation = 0,
                yRotation = 0,
                zRotation = 0
            };
            return new Message_ServerCommand_CreateGameObject()
            {
                transform = newTransform,
                GmjGUID = GenerateGMJGUID(),
                OwnerGUID = ownerGUID,
                Type = Message_ServerCommand_CreateGameObject.GmjType.Player
            };
        }

        private int GenerateGMJGUID()
        {
            return GmjGUID++;
        }
    }
}
