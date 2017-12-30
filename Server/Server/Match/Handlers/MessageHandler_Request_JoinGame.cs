using ServerGameObjectExtension;
using System;
using System.Collections.Generic;

namespace Match
{
    public class MessageHandler_Request_JoinGame : IMessageHandlerCommand, IUpdatable
    {
        private MatchGameEventContainer matchGameEventWrapper;
        private Server_MessageSender sender;
        private MatchThread matchThread;
        ILogger logger;
        private List<IServerExtension> serverExtensions;
        float timeBetweenInitialPings = 1000f;
        public Dictionary<Server_ServerClient, float> lastPingTime;

        public MessageHandler_Request_JoinGame(ILogger logger, Server_MessageSender sender,MatchThread matchThread, List<IServerExtension> serverExtensions, MatchGameEventContainer matchGameEventWrapper)
        {
            this.matchGameEventWrapper = matchGameEventWrapper;
            this.sender = sender;
            this.matchThread = matchThread;
            matchThread.updater.Add(this);
            lastPingTime = new Dictionary<Server_ServerClient, float>();
            this.logger = logger;
            this.serverExtensions = serverExtensions;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_Request_JoinGame);
        }

        /// <summary>
        /// Will register a client to a game
        /// If all are registered the game starts
        /// </summary>
        /// <param name="objData"></param>
        /// <param name="client"></param>
        public void Handle(object objData, Server_ServerClient client)
        {
            Message_Request_JoinGame data = (Message_Request_JoinGame)objData;
            client.info = data.info;
            lastPingTime.Add(client, 0);

            for (int i = 0; i < matchThread.remainingPlayerGUIDsToConnect.Count; i++)
            {
                if (matchThread.remainingPlayerGUIDsToConnect[i] == data.info.GUID)
                {
                    matchThread.RegisterClientInfo(client);
                }
            }
            SendPing(client);
        }

        /// <summary>
        /// On update it checks that all clients has recorded their ping
        /// If ping count is met and all players have connected the game starts
        /// </summary>
        public void Update()
        {
            bool ready = true;
            foreach (var player in matchThread.GetConnectedClients())
            {
                if (!player.Value.HasSufficientPingsRecorded())
                {
                    ready = false;
                    if (matchThread.clock.GetTime() > (lastPingTime[player.Value] + timeBetweenInitialPings))
                    {
                        lastPingTime[player.Value] = matchThread.clock.GetTime();
                        SendPing(player.Value);
                    }
                }
            }
            if (matchThread.GetConnectedClientsInfo().Count != matchThread.PlayerCountExpected)
                ready = false;
            if (ready)
                StartGame();
        }

        /// <summary>
        /// Sends a ping message to a player
        /// </summary>
        /// <param name="player"></param>
        private void SendPing(Server_ServerClient player)
        {
            Message_ServerRoundTrip_Ping msg = new Message_ServerRoundTrip_Ping()
            {
                timeSend = matchThread.clock.GetTime()
            };
            matchThread.GetServer().messageSender.Send(msg, player);
        }

        /// <summary>
        /// Check if the game can be started
        /// Current condition checks if the count of connected players matches the expected amount
        /// </summary>
        private void StartGame()
        {
            matchGameEventWrapper.GameStarted_Invoke(matchThread.GetServer());
            matchThread.pingDeterminer.CalculateMatchPing();
            List<Shared_PlayerInfo> playerInfoes = matchThread.GetConnectedClientsInfo();
            foreach (var client in matchThread.GetConnectedClients())
            {
                Message_Response_GameAllConnected gameData = new Message_Response_GameAllConnected(
                    client.Value.info, playerInfoes, matchThread.clock.GetTimeForClient(client.Value));
                sender.Send(gameData, client.Value);
                foreach (var extension in serverExtensions)
                {
                    var msgList = extension.GetMessagesForClientSetup(client.Value, matchThread.clock);
                    foreach (var msg in msgList)
                    {
                        if (msg != null)
                            matchThread.GetServer().messageSender.SendToAll(msg, matchThread.GetServer().clientManager.GetClients());
                    }
                }
            }
            matchThread.updater.Remove(this);
        }
    }
}