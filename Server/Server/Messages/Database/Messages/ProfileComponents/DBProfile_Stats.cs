using System;

namespace DatabaseConnector
{
    [Serializable]
    public class DBProfile_Stats
    {
        public int wins;
        public int losses;
        public int kills;
        public int deaths;

        public DBProfile_Stats(int wins, int losses, int kills, int deaths)
        {
            this.wins = wins;
            this.losses = losses;
            this.kills = kills;
            this.deaths = deaths;
        }
    }
}