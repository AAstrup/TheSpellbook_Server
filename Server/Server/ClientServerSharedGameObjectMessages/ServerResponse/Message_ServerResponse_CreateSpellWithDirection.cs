using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ServerResponse_CreateSpellWithDirection
    {
        public Message_ClientRequest_CreateSpellWithDirection request;
        public int spellID;
    }
}