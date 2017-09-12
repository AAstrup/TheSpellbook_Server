using Match;
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
        private Server_MessageSender sender;
        private ILogger logger;

        public MatchMakerCore(ILogger logger,Server_MessageSender sender)
        {
            matchThreads = new List<Thread>();
            nextport = AppConfig.FirstPortOfMatches;
            this.sender = sender;
            this.logger = logger;
        }

        /// <summary>
        /// TEMPLATE IMPLEMENTATION
        /// Creates a match if two or more people are queued
        /// </summary>
        /// <param name="list"></param>
        public void Update(List<Server_ServerClient> list)
        {
            Console.WriteLine("Clients " + list.Count);
            while(list.Count >= 2)
            {
                var p1 = list[0];
                list.RemoveAt(0);
                var p2 = list[0];
                list.RemoveAt(0);

                MakeMatch(p1, p2);
            }
        }

        /// <summary>
        /// Creates a thread which will run a new match for the two clinets
        /// Will tell the clients to join the new match
        /// </summary>
        /// <param name="p1">Player 1</param>
        /// <param name="p2">Player 2</param>
        private void MakeMatch(Server_ServerClient p1, Server_ServerClient p2)
        {
            MatchThread matchInfo = new MatchThread(p1,p2, nextport, logger);
            Thread matchThread = new Thread(new ThreadStart(matchInfo.ThreadStart));
            matchThreads.Add(matchThread);
            matchThread.Start();

            Message_Updates_MatchFound update = new Message_Updates_MatchFound()
            {
                ip = AppConfig.IpOfMatch,
                port = nextport
            };
            logger.Log("Creating match at port " + nextport);
            nextport++;
            sender.Send(update,p1);
            sender.Send(update, p2);
        }
    }
}
