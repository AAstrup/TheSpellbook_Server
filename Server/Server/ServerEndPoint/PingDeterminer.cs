namespace Match
{
    public class PingDeterminer : IUpdatable
    {
        private double miliSecondsPerTick;
        private Server_MessageSender messageSender;
        private Clock clock;
        private Server_ClientManager clientManager;
        private double matchPing;
        int accumilatedUpdates;
        static int updatesNeededToPing = 150;
        static int pingsMissedToDropPlayer = 3;

        public PingDeterminer(Server_ClientManager clientManager, Server_MessageSender sender, double miliSecondsPerTick, Clock clock, Updater updater)
        {
            this.miliSecondsPerTick = miliSecondsPerTick;
            this.messageSender = sender;
            this.clock = clock;
            this.clientManager = clientManager;
            updater.Add(this);
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
        /// See method CalculateMatchPing on how this is determined
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

        /// <summary>
        /// Accumilates up time and then pings clients to verify they are online and calibrate their ping
        /// </summary>
        public void Update()
        {
            accumilatedUpdates++;
            if(accumilatedUpdates >= updatesNeededToPing)
            {
                accumilatedUpdates = 0;

                for (int i = 0; i < clientManager.GetClients().Count; i++)
                {
                    if (clock.GetTime() - clientManager.GetClients()[i].GetLastPingTimeStamp() > (updatesNeededToPing * miliSecondsPerTick * pingsMissedToDropPlayer))
                    {
                        clientManager.ClientLeft(clientManager.GetClients()[i]);
                    }
                    else
                    {
                        SendPing(clientManager.GetClients()[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Sends a ping message to a player, which is reflected to the server from the client
        /// </summary>
        /// <param name="player"></param>
        public void SendPing(Server_ServerClient player)
        {
            Message_ServerRoundTrip_Ping msg = new Message_ServerRoundTrip_Ping()
            {
                timeSend = clock.GetTime()
            };
            messageSender.Send(msg, player);
        }
    }
}