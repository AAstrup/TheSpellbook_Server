using System;

namespace ServerGameObjectExtension
{
    internal class MovementLibrary
    {
        public MovementLibrary()
        {
        }

        internal static object Move(float currentXPos, float currentZPos, float moveTargetXPos, float moveTargetZPos, double time,float moveSpeed)
        {
            VectorXZ pos = new VectorXZ(moveTargetXPos-currentXPos,moveTargetZPos-currentZPos);
            pos.Normalise();
            apply speed and return
        }
    }
}