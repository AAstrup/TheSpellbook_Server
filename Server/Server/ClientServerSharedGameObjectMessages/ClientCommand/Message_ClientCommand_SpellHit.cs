﻿using System;

namespace ClientServerSharedGameObjectMessages
{
    [Serializable]
    public class Message_ClientCommand_SpellHit
    {
        public int spellHitGmjID;
        public int playerGMJHit;
        public float hitDirectionX;
        public float hitDirectionY;
        public float hitDirectionZ;
    }
}