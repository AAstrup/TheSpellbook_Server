using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ClientRequest_CreateSpellInStaticPosition
    {
        public SpellType spellType;
        public float xPos;
        public float zPos;
        public int ownerGUID;
        /// <summary>
        /// The time of which the spell should be fired on the synchronized clock
        /// </summary>
        public float fireClockTime;
    }
}