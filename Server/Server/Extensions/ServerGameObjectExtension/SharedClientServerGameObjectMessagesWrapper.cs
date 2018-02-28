using ClientServerSharedGameObjectMessages;
using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Match;
using ServerExtensionTools;

namespace ServerGameObjectExtension
{
    public class SharedClientServerGameObjectMessagesWrapper : IServerExtension
    {
        private Factory_ServerCommand_CreateGameObject factoryCreateGameObject;
        private Factory_ServerCommand_StartGame factoryStartGame;

        public SharedClientServerGameObjectMessagesWrapper()
        { 
            factoryCreateGameObject = new Factory_ServerCommand_CreateGameObject();
            factoryStartGame = new Factory_ServerCommand_StartGame();
        }

        List<IMessageHandlerCommand> IServerExtension.CreateMessageHandlers(ServerCore server,PingDeterminer pingDeterminer, MatchGameEventContainer matchGameEventWrapper,Clock matchClock)
        {
            List<IMessageHandlerCommand> msgHandler = new List<IMessageHandlerCommand>();
            SpellGUIDGenerator spellGUIDGenerator = new SpellGUIDGenerator();
            msgHandler.Add(new MessageHandler_ClientRequest_CreateSpellWithDirection(server, spellGUIDGenerator));
            msgHandler.Add(new MessageHandler_ClientRequest_CreateSpellInStaticPosition(server, spellGUIDGenerator));
            msgHandler.Add(new MessageHandler_ClientRequest_PlayerMovementUpdate(server, pingDeterminer));
            msgHandler.Add(new MessageHandler_ClientRequest_RoundEnded(server, matchGameEventWrapper, matchClock,pingDeterminer));

            return msgHandler;
        }

        public List<object> GetMessagesForClientSetup(Server_ServerClient client, Clock clock)
        {
            List<object> setupMessages = new List<object>();
            setupMessages.Add(CreateGameObject(client));
            setupMessages.Add(StartGame(clock.GetTime()));
            return setupMessages;
        }

        /// <summary>
        /// Creates a message for starting the game
        /// </summary>
        /// <param name="client">The client sending to, which ping is used to estimate the time</param>
        /// <returns></returns>
        private Message_ServerComand_StartGame StartGame(double clockTime)
        {
            return factoryStartGame.Create_Message_ServerCommand_StartGame(clockTime);
        }

        /// <summary>
        /// Creates a message for a GameObject creation
        /// </summary>
        /// <param name="client">The client that this GameObject belong to</param>
        /// <returns></returns>
        private Message_ServerCommand_CreateGameObject CreateGameObject(Server_ServerClient client)
        {
            return factoryCreateGameObject.Create_Message_ServerCommand_CreateGameObject(client.info.GUID);
        }

        public void SetupSubscribers(ServerCore server, IServerEventHandler serverCoreEventHandler)
        {
            serverCoreEventHandler.SubScribeClientLeft(new ServerEventHandlerDelegates.ClientLeftEvent (SharedClientServerGameObjectMessagesWrapperEvents.GetClientLeftEvent));
        }
    }
}
