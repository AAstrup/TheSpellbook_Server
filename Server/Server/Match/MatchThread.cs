using Server;
using System;
using System.Collections.Generic;

namespace Match
{
    /// <summary>
    /// This class is created for the new thread that handles the match
    /// The threadstart is the start method of the new thread
    /// </summary>
    public class MatchThread 
    {
        private ILogger logger;
        private Dictionary<int, Shared_PlayerInfo> GUIDToPlayerClient;
        private int port;
        private ServerCore server;
        private List<Shared_PlayerInfo> clientsInfo;
        private bool gameHasEnded;

        /// <summary>
        /// Creates a new Match class which can be used for running a server for a match
        /// This has to be started using the ThreadStart method on a new thread to be ran
        /// </summary>
        /// <param name="clients">Clients that will be in the match</param>
        /// <param name="port">Port used by the thread</param>
        /// <param name="logger">Logger used to log info</param>
        public MatchThread(List<Server_ServerClient> clients, int port, ILogger logger)
        {
            clientsInfo = new List<Shared_PlayerInfo>();
            foreach (var client in clients)
            {
                clientsInfo.Add(client.info);
            }
            this.port = port;
            this.logger = logger;
            var GUIDToPlayerClient = new Dictionary<int, Shared_PlayerInfo>();
            foreach (var clientInfo in clientsInfo)
            {
                GUIDToPlayerClient.Add(clientInfo.GUID, clientInfo);
            }
        }

        /// <summary>
        /// Always call this method on a new thread
        /// Starts the thread running the match
        /// </summary>
        public void ThreadStart()
        {
            MatchGameMessageHandler matchGameHandler = new MatchGameMessageHandler(logger,this);
            server = new ServerCore(matchGameHandler,new ConnectionInfo(port));
            matchGameHandler.Init();

            while (!gameHasEnded)
            {
                server.Update();
            }

            logger.Log("Match ended");
        }

        public ServerCore GetServer() { return server; }
        public List<Shared_PlayerInfo> GetClientsInfo() { return clientsInfo; }
        public Shared_PlayerInfo GetClientByGUID(int GUID) { return GUIDToPlayerClient[GUID]; }
    }
}