using System;

namespace ServerGameObjectExtension
{
    [Serializable]
    public class Message_ServerCommand_RoundEnd
    {
        /// <summary>
        /// Time for which the next round starts
        /// </summary>
        public double timeNextRoundStart;
    }
}