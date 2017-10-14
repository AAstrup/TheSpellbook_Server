using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ServerCommand_RoundReset
    {
        public int MapLayoutNr;
    }
}