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
        /// Time of the clock on which the spell were requested to be casted
        /// </summary>
        public double TimeStartedCasting;
    }
    public enum SpellType { Fireball, Explode }
}