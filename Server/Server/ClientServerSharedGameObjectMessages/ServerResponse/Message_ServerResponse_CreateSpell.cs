using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ServerResponse_CreateSpell
    {
        public Message_ClientRequest_CreateSpell request;
        public int spellGmjID;
    }
}