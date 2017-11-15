using System;

namespace ServerGameObjectExtension
{
    [Serializable]
    public class Message_ServerCommand_RoundReset
    {
        /// <summary>
        /// Time for which the next round starts
        /// </summary>
        public double timeRoundStart;
    }
}