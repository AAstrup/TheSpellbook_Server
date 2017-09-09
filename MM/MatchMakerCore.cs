using System;
using System.Collections.Generic;
using System.Threading;

namespace MatchMaker
{
    /// <summary>
    /// Responsible for setting up and updating classes used to make matches
    /// Responsible for starting matches as well
    /// </summary>
    internal class MatchMakerCore
    {
        List<Thread> matchThreads;
        private int nextport;

        public MatchMakerCore()
        {
            matchThreads = new List<Thread>();
            nextport = AppConfig.Port + 1;
        }

        public void Update(List<Server_ServerClient> list)
        {
            //Test implmentation
            if(list.Count > 2)
            {
                var p1 = list[0];
                list.RemoveAt(0);

                var p2 = list[0];
                list.RemoveAt(0);

                MakeMatch(p1, p2);
            }
        }

        private void MakeMatch(Server_ServerClient p1, Server_ServerClient p2)
        {
            Thread matchThread = new Thread(new ThreadStart(WorkerCreateMatch));
            matchThreads.Add(matchThread);
             
            MatchSetupGameInfo gameInfo = new MatchSetupGameInfo(p1,p2, nextport);
            matchThread.Start(gameInfo);
        }

        void WorkerCreateMatch(Object obj)
        {

        }
    }
}