using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ClientRequest_CreateSpell
    {
        public SpellType spellType;
        public float xPos;
        public float zPos;
        public float xDir;
        public float zDir;
        public int ownerGUID;
    }
    public enum SpellType { Fireball, Explode }
}