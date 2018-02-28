using System;
using System.Collections.Generic;

namespace DatabaseConnector
{
    public class DBOwnedSpells
    {
        private static object tableName = "PlayerOwnedSpells";

        public static DBProfile_OwnedSpells GetPlayer(string username, string hashedPassword)
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
                DBProfile_OwnedSpells profile;
                profile = new DBProfile_OwnedSpells((int)results["Teleport"]);
                return profile;
            }
            return null;
        }

        /// <summary>
        /// Register a new player on the PlayerOwnedSpells table with
        /// Id matches the id of the player on the LoginTable
        /// </summary>
        /// <param name="id">Id matches the id of the player on the LoginTable</param>
        public static void RegisterNewPlayer(int id)
        {
            string sql = String.Format("INSERT INTO [dbo].[{0}] (Id,Teleport) VALUES({1},0); "
            , tableName
            , id);

            var sqlResult = DBEndPoint.ExecuteSQL(sql);
            if (!sqlResult.Key)
                throw new Exception("Register player failed in DBPlayer achievements with msg " + sqlResult.Value);
        }

    }
}