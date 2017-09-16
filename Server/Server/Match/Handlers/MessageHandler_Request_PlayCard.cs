using System;

namespace Match
{
    internal class MessageHandler_Request_PlayCard : IMessageHandlerCommand
    {
        private GameEngine gameEngine;

        public MessageHandler_Request_PlayCard(GameEngine gameEngine)
        {
            this.gameEngine = gameEngine;
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            Message_Request_PlayCard data = (Message_Request_PlayCard)objData;
            gameEngine.PlayCard(data.CardID, client.info.GUID);
        }
    }
}