using System;

namespace ServerGameObjectExtension
{
    internal class MovementLibrary
    {
        public MovementLibrary()
        {
        }

        /// <summary>
        /// Move from a position to another position
        /// The amount moved is calculated by time times movespeed
        /// </summary>
        /// <param name="currentXPos">From x position</param>
        /// <param name="currentZPos">From z position</param>
        /// <param name="moveTargetXPos">To x position</param>
        /// <param name="moveTargetZPos">To z position</param>
        /// <param name="timeInMiliSeconds">Time in miliseconds</param>
        /// <param name="moveSpeed">Movespeed</param>
        /// <returns></returns>
        internal static VectorXZ Move(float currentXPos, float currentZPos, float moveTargetXPos, float moveTargetZPos, double timeInMiliSeconds,float moveSpeed)
        {
            VectorXZ pos = new VectorXZ(moveTargetXPos-currentXPos,moveTargetZPos-currentZPos);
            pos.Normalise();
            pos.Multiply(moveSpeed * ((float)timeInMiliSeconds)/1000);
            pos.x += currentXPos;
            pos.z += currentZPos;
            return pos;
        }
    }
}