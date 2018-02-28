using System;

namespace Match
{
    public class MatchGameEventContainer
    {
        public delegate void GameStartedEvent(ServerCore server);
        public event GameStartedEvent GameStarted;

        public void GameStarted_Invoke(ServerCore server)
        {
            if(GameStarted != null)
                GameStarted.Invoke(server);
        }
    }
}