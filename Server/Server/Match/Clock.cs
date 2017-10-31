using System;
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
    }
}