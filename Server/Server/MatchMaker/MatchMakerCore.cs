using Match;
using Server;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        public delegate void readyCheck(List<Server_ServerClient> clients, Message_ServerRequest_ReadyCheck readyCheck);
        public event readyCheck matchReadyCheckInitiated;
        List<Server_ServerClient> registeredClientsQueued;

        public MatchMakerCore(ServerCore serverCore,ILogger logger,Server_MessageSender sender)
        {
            registeredClientsQueued = new List<Server_ServerClient>();
            this.serverCore = serverCore;
            matchThreads = new List<Thread>();
            nextport = ServerConfig.FirstPortOfMatches;
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
            while(registeredClientsQueued.Count >= ServerConfig.GetInt("PlayerCountInAMatch"))
            {
                List<Server_ServerClient> clients = new List<Server_ServerClient>();
                for (int i = 0; i < ServerConfig.GetInt("PlayerCountInAMatch"); i++)
                {
                    var e = registeredClientsQueued[0];
                    registeredClientsQueued.Remove(e);
                    clients.Add(e);
                }
                SendMatchReadyCheck(clients);
            }
            if(matchThreads.Count > 0)
                if (!matchThreads[0].IsAlive)
                    matchThreads.RemoveAt(0);
        }

        /// <summary>
        /// Will send a ready checkbox to clients for clients to reply
        /// </summary>
        /// <param name="clients">Clients to connect to the match</param>
        private void SendMatchReadyCheck(List<Server_ServerClient> clients)
        {
            Message_ServerRequest_ReadyCheck msg = new Message_ServerRequest_ReadyCheck();
            matchReadyCheckInitiated.Invoke(clients,msg);
            foreach (var client in clients)
            {
                sender.Send(msg, client);
            }
        }

        /// <summary>
        /// Creates a thread which will run a new match for the two clinets
        /// Will tell the clients to join the new match
        /// </summary>
        /// <param name="clients">Clients</param>
        public void MakeMatch(List<Server_ServerClient> clients)
        {
            foreach (var client in clients)
            {
                serverCore.clientManager.GetClients().Remove(client);
            }
            MatchThread matchInfo = new MatchThread(clients, nextport, logger);
            Thread matchThread = new Thread(new ThreadStart(matchInfo.ThreadStart));
            matchThreads.Add(matchThread);
            matchThread.Start();

            Message_Update_MatchFound update = new Message_Update_MatchFound()
            {
                ip = ConfigurationManager.AppSettings["IpOfMatch"],
                port = nextport
            };
            logger.Log("Creating match at port " + nextport);
            nextport++;
            foreach (var client in clients)
            {
                sender.Send(update, client);
            }
        }
    }
}
