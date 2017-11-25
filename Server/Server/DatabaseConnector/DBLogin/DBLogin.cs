using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DatabaseConnector
{
    public class DBLogin
    {
        private static string tableName = "LoginTable";

        /// <summary>
        /// Search for a player
        /// Returns null if no player is found
        /// </summary>
        /// <param name="username">Username of the player</param>
        /// <param name="hashedPassword">Hashed password of the player </param>
        /// <returns>The player profile, null if none were found</returns>
        public static DBProfile_Login GetPlayer(string username, string hashedPassword)
        {
            string sql = String.Format("SELECT * FROM [dbo].[{0}] WHERE Username = '{1}' and Password = '{2}'"
                , tableName
                , username
                , hashedPassword);
            Dictionary<string, object> results = new Dictionary<string, object>();
            if (DBEndPoint.GetSingleRowSqlSearch(sql, results))
            {
                DBProfile_Login profile;
                profile = new DBProfile_Login((int)results["Id"], (string)results["Username"]);
                return profile;
            }
            return null;
        }


        /// <summary>
        /// Search for a player
        /// Returns if a player is found
        /// </summary>
        /// <param name="username">Username of the player</param>
        /// <returns>Returns if a player is found with the usernames</returns>
        static bool PlayerExist(string username)
        {
            string sql = String.Format("SELECT * FROM [dbo].[{0}] WHERE Username = '{1}'"
                , tableName
                , username);
            Dictionary<string, object> results = new Dictionary<string, object>();
            if (DBEndPoint.GetSingleRowSqlSearch(sql, results))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Register a new player
        /// Returns null if it fails
        /// </summary>
        /// <param name="username">Username of the player</param>
        /// <param name="password">Password of the player</param>
        /// <returns>The player or null depending on how it's result</returns>
        public static KeyValuePair<DBProfile_Login,string> RegisterPlayer(string username, string password)
        {
            string hash = DBPasswordHash.GetHashString(password);
            if (!PlayerExist(username)) { 
                string sql = String.Format("INSERT INTO [dbo].[{0}] (Username,Password) VALUES('{1}', '{2}'); "
                    , tableName
                    , username
                    , hash);

                var sqlResult = DBEndPoint.ExecuteSQL(sql);
                if (sqlResult.Key)
                {
                    var player = GetPlayer(username, hash);
                    RegisterPlayerInOtherTables(player.id);
                    return new KeyValuePair<DBProfile_Login, string>(player,"Succesfully generated");
                }
                else
                {
                    return new KeyValuePair<DBProfile_Login, string>(null, "Internal issue");
                }
            }
            else
            {
                return new KeyValuePair<DBProfile_Login, string>(null,"A player with that name already exist");
            }
        }

        /// <summary>
        /// Register player in all tables except LoginTable from which the id were incremented
        /// </summary>
        /// <param name="id">ID of the player</param>
        private static void RegisterPlayerInOtherTables(int id)
        {
            DBStats.RegisterNewPlayer(id);
            DBOwnedSpells.RegisterNewPlayer(id);
        }
    }
}