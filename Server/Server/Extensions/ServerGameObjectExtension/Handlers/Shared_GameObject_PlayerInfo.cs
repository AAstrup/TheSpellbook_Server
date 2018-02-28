using System;

namespace ServerGameObjectExtension
{
    internal class Shared_GameObject_PlayerInfo
    {
        public Shared_GameObject_PlayerInfo()
        {
        }

        /// <summary>
        /// Returns the default movespeed of the players
        /// This is needed on the server to predict position in lag compensation
        /// </summary>
        /// <returns></returns>
        internal static float GetMoveSpeed()
        {
            return 1f;
        }
    }
}