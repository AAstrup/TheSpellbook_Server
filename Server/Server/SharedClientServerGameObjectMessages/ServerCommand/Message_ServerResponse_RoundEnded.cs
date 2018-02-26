using ServerGameObjectExtension;
using System;
using System.Collections.Generic;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ServerResponse_RoundEnded
    {
        public int Winner;
        public List<PlayerScore> Scores;
    }
}