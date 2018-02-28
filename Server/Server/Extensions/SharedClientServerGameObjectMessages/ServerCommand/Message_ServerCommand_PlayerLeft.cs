using System;

namespace SharedClientServerGameObjectMessages
{
    [Serializable]
    public class Message_ServerCommand_PlayerLeft
    {
        public int playerLeftGUID;

        public Message_ServerCommand_PlayerLeft(int playerLeftGUID)
        {
            this.playerLeftGUID = playerLeftGUID;
        }
    }
}