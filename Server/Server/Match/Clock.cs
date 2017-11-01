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

        public double GetTimeForClient(Server_ServerClient client)
        {
            return GetTime() + client.GetPingInMiliSeconds();
        }

        double lastPrint;
        public void PRINT()
        {
            if (Math.Floor(GetTime()/1000.0) > lastPrint){
                lastPrint = Math.Floor(GetTime() / 1000.0);
            }
        }
    }
}