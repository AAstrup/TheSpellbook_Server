using Server;
using ServerGameObjectExtension;
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
        List<IServerExtension> serverExtensions;
        private ILogger logger;
        private int miliSecondPerTick;
        public int PlayerCountExpected;
        private Dictionary<int, Server_ServerClient> GUIDToPlayerClient;
        public List<int> remainingPlayerGUIDsToConnect;
        private int port;
        private ServerCore server;
        private bool gameHasEnded;
        private List<Shared_PlayerInfo> clientsInfo;
        public Updater updater;
        public Clock clock;
        public PingDeterminer pingDeterminer; 

        /// <summary>
        /// Creates a new Match class which can be used for running a server for a match
        /// This has to be started using the ThreadStart method on a new thread to be ran
        /// </summary>
        /// <param name="clients">Clients given by the matchmaker, new connection must be established from them</param>
        /// <param name="port">Port used by the thread</param>
        /// <param name="logger">Logger used to log info</param>
        public MatchThread(List<Server_ServerClient> clients, int port, ILogger logger)
        {
            clock = new Clock();
            remainingPlayerGUIDsToConnect = new List<int>();
            foreach (var item in clients)
            {
                remainingPlayerGUIDsToConnect.Add(item.info.GUID);
            }

            updater = new Updater();
            clientsInfo = new List<Shared_PlayerInfo>();
            GUIDToPlayerClient = new Dictionary<int, Server_ServerClient>();
            PlayerCountExpected = clients.Count;
            serverExtensions = MatchExtensionFactory.CreateExtensions(logger);
            this.port = port;
            this.logger = logger;
            miliSecondPerTick = 1000 / ServerConfig.GetInt("MatchTickRate");
        }

        /// <summary>
        /// Only call this method on an another thread 
        /// Starts the thread running the match
        /// </summary>
        public void ThreadStart()
        {
            MatchGameMessageHandler matchGameHandler = new MatchGameMessageHandler(logger,this);
            server = new ServerCore(matchGameHandler,new ServerConnectionInfo(port));
            pingDeterminer = new PingDeterminer(server.clientManager, clock);
            matchGameHandler.Init(serverExtensions);
            var time = clock.GetTime();

            while (!gameHasEnded)
            {
                clock.PRINT();
                updater.Update();
                server.Update();
                System.Threading.Thread.Sleep(miliSecondPerTick);
            }

            logger.Log("Match ended");
        }

        public void RegisterClientInfo(Server_ServerClient client)
        {
            remainingPlayerGUIDsToConnect.Remove(client.info.GUID);
            GUIDToPlayerClient.Add(client.info.GUID, client);
            clientsInfo.Add(client.info);
        }

        public ServerCore GetServer() { return server; }
        public Dictionary<int, Server_ServerClient> GetConnectedClients() { return GUIDToPlayerClient; }
        public List<Shared_PlayerInfo> GetConnectedClientsInfo() { return clientsInfo; }
        public Server_ServerClient GetClientByGUID(int GUID) { return GUIDToPlayerClient[GUID]; }
    }
}