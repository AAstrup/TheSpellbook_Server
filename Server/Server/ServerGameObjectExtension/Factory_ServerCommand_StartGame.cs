using ClientServerSharedGameObjectMessages;
using System;

namespace ServerGameObjectExtension
{
    internal class Factory_ServerCommand_StartGame
    {
        static double timeBeforeStart = 15000.0;

        public Message_ServerComand_StartGame Create_Message_ServerCommand_StartGame(double clockTime)
        {
            return new Message_ServerComand_StartGame(timeBeforeStart + clockTime);
        }
    }
}