using Match;
using System;

namespace ServerGameObjectExtension
{
    internal class MessageHandler_ClientRequest_PlayerMovementUpdate : IMessageHandlerCommand
    {
        private PingDeterminer pingDeterminer;
        private ServerCore server;

        public MessageHandler_ClientRequest_PlayerMovementUpdate(ServerCore server,PingDeterminer pingDeterminer)
        {
            this.pingDeterminer = pingDeterminer;
            this.server = server;
        }

        public Type GetMessageTypeSupported()
        {
            return typeof(Message_ClientRequest_PlayerMovementUpdate);
        }

        public void Handle(object objData, Server_ServerClient client)
        {
            var data = (Message_ClientRequest_PlayerMovementUpdate)objData;
            double time = data.TimeStartedMoving + pingDeterminer.GetMatchPingInMiliSeconds();
            var msg = new Message_ServerResponse_PlayerMovementUpdate()
            {
                currentXPos = data.currentXPos,
                currentZPos = data.currentZPos,
                GMJGUID = data.GMJGUID,
                moveTargetXPos = data.moveTargetXPos,
                moveTargetZPos = data.moveTargetZPos,
                TimeStartedMovingWithPing = time
            };
            server.messageSender.SendToAll(msg,server.clientManager.GetClients());
        }
    }
}