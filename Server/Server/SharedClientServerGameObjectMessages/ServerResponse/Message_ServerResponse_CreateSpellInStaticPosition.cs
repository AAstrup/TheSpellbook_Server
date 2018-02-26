using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ServerResponse_CreateSpellInStaticPosition
    {
        public Message_ClientRequest_CreateSpellInStaticPosition request;
        public int spellID;
    }
}