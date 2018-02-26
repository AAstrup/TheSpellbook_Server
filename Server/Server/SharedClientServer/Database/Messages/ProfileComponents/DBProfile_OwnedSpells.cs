using System;

namespace DatabaseConnector
{
    [Serializable]
    public class DBProfile_OwnedSpells
    {
        public int hasTeleport;

        public DBProfile_OwnedSpells(int hasTeleport)
        {
            this.hasTeleport = hasTeleport;
        }
    }
}