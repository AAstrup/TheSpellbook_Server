using System;
using System.Collections.Generic;

namespace DatabaseConnector
{
    public class DBStats
    {
        private static string tableName = "PlayerStats";

        /// <summary>
        /// Adds a win to a players record along with kills and deaths
        /// </summary>
        /// <param name="id">ID of the player, gotten from the LoginTable</param>
        /// <param name="kills"></param>
        /// <param name="deaths"></param>
        public static void IncrementWins(int id,int kills, int deaths)
        {
            Increment(id, "Wins", kills, deaths);
        }

        /// <summary>
        /// Returns the profile of a player
        /// </summary>
        /// <param name="username"></param>
        /// <param name="hashedPassword"></param>
        /// <returns>Returns the profile of a player, returns null if none were found</returns>
        public static DBProfile_Stats GetPlayer(string username, string hashedPassword)
        {
            var player = DBLogin.GetPlayer(username, hashedPassword);
            if (player == null)
                return null;
            string sql = String.Format("SELECT * FROM [dbo].[{0}] WHERE Id = {1}"
                , tableName
                , player.id);
            Dictionary<string, object> results = new Dictionary<string, object>();
            if (DBEndPoint.GetSingleRowSqlSearch(sql, results))
            {
                DBProfile_Stats profile;
                profile = new DBProfile_Stats((int)results["Wins"], (int)results["Losses"], (int)results["Kills"], (int)results["Deaths"]);
                return profile;
            }
            return null;
        }

        /// <summary>
        /// Adds a loss to a players record along with kills and deaths
        /// </summary>
        /// <param name="id">ID of the player, gotten from the LoginTable</param>
        /// <param name="kills"></param>
        /// <param name="deaths"></param>
        public static void IncrementLoss(int id, int kills, int deaths)
        {
            Increment(id, "Losses", kills, deaths);
        }

        /// <summary>
        /// Generates and executes the sql to increment a certain row and updating kills and deaths
        /// </summary>
        /// <param name="id">ID of the player, gotten from the LoginTable</param>
        /// <param name="rowNameToIncrement"></param>
        /// <param name="kills"></param>
        /// <param name="deaths"></param>
        static void Increment(int id, string rowNameToIncrement, int kills, int deaths)
        {
            string sql = String.Format("UPDATE {0} SET Losses = Losses + 1, Kills = Kills + {1}, Deaths = Deaths + {2} WHERE Id = {3}",
            tableName,
            kills,
            deaths,
            id);
            DBEndPoint.ExecuteSQL(sql);
        }

        /// <summary>
        /// Creates an row for a new player on the PlayerAchievements table, with the id matching the id of LoginTable
        /// </summary>
        /// <param name="id">Id of the player on LoginTable</param>
        public static void RegisterNewPlayer(int id)
        {
            string sql = String.Format("INSERT INTO [dbo].[{0}] (Id,Wins,Losses,Kills,Deaths) VALUES({1},0,0,0,0); "
           , tableName
           , id);

            var sqlResult = DBEndPoint.ExecuteSQL(sql);
            if(!sqlResult.Key)
                throw new Exception("Register player failed in DBPlayerAchievements with msg " + sqlResult.Value);
        }
    }
}