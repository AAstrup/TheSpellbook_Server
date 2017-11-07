using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ClientRequest_CreateSpellInStaticPosition
    {
        public SpellType spellType;
        public float spellXPos;
        public float spellZPos;
        public float playerXPos;
        public float playerZPos;
        public int ownerGUID;
        /// <summary>
        /// Time of the clock on which the spell were requested to be casted
        /// </summary>
        public double TimeStartedCasting;
    }
}