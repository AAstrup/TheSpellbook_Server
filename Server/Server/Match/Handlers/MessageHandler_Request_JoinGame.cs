using ServerGameObjectExtension;
using System;
using System.Collections.Generic;

namespace Match
{
    public class MessageHandler_Request_JoinGame : IMessageHandlerCommand
    {
        private Server_MessageSender sender;
        private MatchThread matchThread;
        ILogger logger;
        private List<IServerExtension> serverExtensions;

        public MessageHandler_Request_JoinGame(ILogger logger, Server_MessageSender sender,MatchThread matchThread, List<IServerExtension> serverExtensions)
        {
            this.sender = sender;
            this.matchThread = matchThread;
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

            for (int i = 0; i < matchThread.remainingPlayerGUIDsToConnect.Count; i++)
            {
                if (matchThread.remainingPlayerGUIDsToConnect[i] == data.info.GUID)
                {
                    matchThread.RegisterClientInfo(client);
                }
            }
            CheckIfTheGameCanBeStarted();
        }

        /// <summary>
        /// Check if the game can be started
        /// Current condition checks if the count of connected players matches the expected amount
        /// </summary>
        private void CheckIfTheGameCanBeStarted()
        {
            if (matchThread.GetConnectedClientsInfo().Count == matchThread.PlayerCountExpected)
            {
                List<Shared_PlayerInfo> playerInfoes = matchThread.GetConnectedClientsInfo();
                foreach (var client in matchThread.GetConnectedClients())
                {
                    Message_Response_GameAllConnected gameData = new Message_Response_GameAllConnected(client.Value.info, playerInfoes);
                    sender.Send(gameData, client.Value);
                    foreach (var extension in serverExtensions)
                    {
                        var msgList = extension.GetMessagesForClientSetup(client.Value);
                        foreach (var msg in msgList)
                        {
                            if (msg != null)
                                matchThread.GetServer().messageSender.SendToAll(msg, matchThread.GetServer().clientManager.GetClients());
                        }
                    }
                }
            }
        }
    }
}