using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Match
{
    public class Clock
    {
        private Stopwatch watch;

        public Clock()
        {
            watch = new Stopwatch();
            watch.Start();
        }
        public long GetTime()
        {
            return watch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Used for synchronizing clock at a specific client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public double GetTimeForClient(Server_ServerClient client)
        {
            return GetTime() + client.GetPingInMiliSeconds();
        }

        double lastPrint;
    }
}