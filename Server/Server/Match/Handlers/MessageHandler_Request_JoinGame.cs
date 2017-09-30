using System;

namespace Match
{
    public class MessageHandler_Request_JoinGame : IMessageHandlerCommand
    {
        private Server_MessageSender sender;
        private MatchThread matchThread;
        ILogger logger;

        public MessageHandler_Request_JoinGame(ILogger logger, Server_MessageSender sender,MatchThread matchThread)
        {
            this.sender = sender;
            this.matchThread = matchThread;
            this.logger = logger;
        }

        /// <summary>
        /// Will
        /// </summary>
        /// <param name="objData"></param>
        /// <param name="client"></param>
        public void Handle(object objData, Server_ServerClient client)
        {
            Message_Request_JoinGame data = (Message_Request_JoinGame)objData;
            client.info = data.info;

            foreach (var c in matchThread.GetServer().clientManager.GetClients())
            {
                if(c.info.GUID == data.info.GUID)
                {
                    Message_Response_GameState gameData = new Message_Response_GameState(c.info, matchThread.GetClientsInfo());
                    sender.Send(gameData, client);
                    return;
                }
            }
            Console.WriteLine("PLAYER GUID NOT FOUND. WILL NOT REGISTER");
        }
    }
}