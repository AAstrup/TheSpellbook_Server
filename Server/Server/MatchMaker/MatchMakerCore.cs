using Match;
using Server;
using System;
using System.Collections.Generic;
using System.Threading;

namespace MatchMaker
{
    /// <summary>
    /// Responsible for setting up and updating classes used to make matches
    /// Responsible for starting matches as well
    /// </summary>
    public class MatchMakerCore
    {
        private ServerCore serverCore;
        List<Thread> matchThreads;
        private int nextport;
        private Server_MessageSender sender;
        private ILogger logger;
        public event Action<Server_ServerClient, Server_ServerClient, Message_ServerRequest_ReadyCheck> matchReadyCheckInitiated;
        List<Server_ServerClient> registeredClientsQueued;

        public MatchMakerCore(ServerCore serverCore,ILogger logger,Server_MessageSender sender)
        {
            registeredClientsQueued = new List<Server_ServerClient>();
            this.serverCore = serverCore;
            matchThreads = new List<Thread>();
            nextport = AppConfig.FirstPortOfMatches;
            this.sender = sender;
            this.logger = logger;
        }

        /// <summary>
        /// Register a client to the queue
        /// Distinct from the servers collection of client, they must have information here
        /// </summary>
        /// <param name="client"></param>
        internal void RegisterClient(Server_ServerClient client)
        {
            registeredClientsQueued.Add(client);
        }

        /// <summary>
        /// Remove a client from the queue
        /// </summary>
        /// <param name="client"></param>
        internal void RemoveClient(Server_ServerClient client)
        {
            registeredClientsQueued.Remove(client);
        }

        /// <summary>
        /// TEMPLATE IMPLEMENTATION
        /// Creates a match if two or more people are queued
        /// </summary>
        /// <param name="clients">Clients connected to server, all with info are in queue</param>
        public void Update()
        {
            while(registeredClientsQueued.Count > 1)
            {
                var p1 = registeredClientsQueued[0];
                registeredClientsQueued.RemoveAt(0);
                var p2 = registeredClientsQueued[0];
                registeredClientsQueued.RemoveAt(0);
                SendMatchReadyCheck(p1, p2);
            }
            if(matchThreads.Count > 0)
                if (!matchThreads[0].IsAlive)
                    matchThreads.RemoveAt(0);
            Console.WriteLine("Game Threads:" + matchThreads.Count);
        }

        /// <summary>
        /// Will send a ready checkbox to clients for clients to reply
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        private void SendMatchReadyCheck(Server_ServerClient p1, Server_ServerClient p2)
        {
            Message_ServerRequest_ReadyCheck msg = new Message_ServerRequest_ReadyCheck();
            if(matchReadyCheckInitiated != null)
                matchReadyCheckInitiated.Invoke(p1,p2,msg);
            sender.Send(msg,p1);
            sender.Send(msg, p2);
        }

        /// <summary>
        /// Creates a thread which will run a new match for the two clinets
        /// Will tell the clients to join the new match
        /// </summary>
        /// <param name="p1">Player 1</param>
        /// <param name="p2">Player 2</param>
        public void MakeMatch(Server_ServerClient p1, Server_ServerClient p2)
        {
            serverCore.clientManager.GetClients().Remove(p1);
            serverCore.clientManager.GetClients().Remove(p2);
            MatchThread matchInfo = new MatchThread(p1.info,p2.info, nextport, logger);
            Thread matchThread = new Thread(new ThreadStart(matchInfo.ThreadStart));
            matchThreads.Add(matchThread);
            matchThread.Start();

            Message_Update_MatchFound update = new Message_Update_MatchFound()
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
