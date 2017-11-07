namespace Match
{
    public class PingDeterminer
    {
        private Clock clock;
        private Server_ClientManager clientManager;
        private double matchPing;

        public PingDeterminer(Server_ClientManager clientManager, Clock clock)
        {
            this.clock = clock;
            this.clientManager = clientManager;
        }

        /// <summary>
        /// Calculate the ping of the match by averaging clients pings
        /// </summary>
        public void CalculateMatchPing()
        {
            double ping = 0f;
            foreach (var client in clientManager.GetClients())
            {
                ping += client.GetPingInMiliSeconds();
            }
            matchPing = ping / clientManager.GetClients().Count;
        }

        /// <summary>
        /// Returns the ping of the match
        /// See method CalculateMatchPing on how tis is determined
        /// </summary>
        /// <returns></returns>
        public double GetMatchPingInMiliSeconds()
        {
            return matchPing;
        }

        /// <summary>
        /// Sets the time of message to include the ping determined for the match
        /// Instead of using the time a request were sent, we align the states of the clients more closely
        /// This offset is based on the client with the least ping
        /// </summary>
        /// <returns></returns>
        public double GetTimeForMatchMessage()
        {
            return GetMatchPingInMiliSeconds() + clock.GetTime();
        }
    }
}