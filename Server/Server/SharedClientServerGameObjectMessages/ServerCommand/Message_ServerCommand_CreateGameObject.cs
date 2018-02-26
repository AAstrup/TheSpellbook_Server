using System;
using System.Collections.Generic;
using System.Text;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ServerCommand_CreateGameObject
    {
        public Message_Common_Transform transform;
        public int OwnerGUID;
        public int GmjGUID;
        public GmjType Type;
        public enum GmjType { Player }
    }
}
