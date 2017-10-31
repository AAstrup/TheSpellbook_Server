using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ClientRequest_CreateSpellWithDirection
    {
        public SpellType spellType;
        public float xPos;
        public float zPos;
        public float xDir;
        public float zDir;
        public int ownerGUID;
        /// <summary>
        /// The time of which the spell should be fired on the synchronized clock
        /// </summary>
        public float fireClockTime;
    }
    public enum SpellType { Fireball, Explode }
}