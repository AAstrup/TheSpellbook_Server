using System;

namespace ServerGameObjectExtension
{
    internal class MovementLibrary
    {
        public MovementLibrary()
        {
        }

        internal static VectorXZ Move(float currentXPos, float currentZPos, float moveTargetXPos, float moveTargetZPos, double time,float moveSpeed)
        {
            VectorXZ pos = new VectorXZ(moveTargetXPos-currentXPos,moveTargetZPos-currentZPos);
            pos.Normalise();
            pos.Multiply(moveSpeed * ((float)time)/1000);
            pos.x += currentXPos;
            pos.z += currentZPos;
            return pos;
        }
    }
}