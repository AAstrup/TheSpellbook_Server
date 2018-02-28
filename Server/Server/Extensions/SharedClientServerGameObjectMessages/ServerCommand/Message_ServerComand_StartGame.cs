using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ServerComand_StartGame
    {
        public double timeRemainingBeforeStart;

        public Message_ServerComand_StartGame(double timeRemainingBeforeStart)
        {
            this.timeRemainingBeforeStart = timeRemainingBeforeStart;
        }
    }
}