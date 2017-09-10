using System;

namespace Match
{
    internal class MessageHandler_Request_JoinGame
    {
        ILogger logger;
        private Server_MessageSender sender;

        public MessageHandler_Request_JoinGame(Server_MessageSender sender, ILogger logger)
        {
            this.logger = logger;
            this.sender = sender;
        }

        internal void Handle(Message_Request_JoinGame data,Server_ServerClient client, GameEngine gameEngine)
        {
            Shared_InGame_PlayerInfo me;
            Shared_InGame_PlayerInfo opp;
            if (data.info.GUID == gameEngine.GetP1().GUID) {
                me = gameEngine.GetP1();
                opp = gameEngine.GetP2();
            } else {//(data.info.GUID == gameEngine.GetP2().GUID) {
                me = gameEngine.GetP2();
                opp = gameEngine.GetP1();
            }
            //else
            //    throw new Exception("Player GUID not found for a registered play! p1 guid " + gameEngine.p1.GUID + " p2 guid " + gameEngine.p2.GUID);
            logger.Log("Player GUID not found for a registered play! p1 guid " + gameEngine.p1.GUID + " p2 guid " + gameEngine.p2.GUID);

            Message_Response_GameState gameData = new Message_Response_GameState(me,opp.GetAsHidden());//Make so you don't get your opponents cards!
            sender.Send(gameData, client);
        }

    }
}