using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DatabaseConnector
{
    public class DBLogin
    {
        private static string tableName = "LoginTable";

        internal static void GetPlayer(object name, object password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Search for a player
        /// Returns null if no player is found
        /// </summary>
        /// <param name="username">Username of the player</param>
        /// <param name="password">Password of the player will be hashed later</param>
        /// <returns>The player profile, null if none were found</returns>
        public static DBPlayerProfile GetPlayer(string username, string password)
        {
            string hash = DBPasswordHash.GetHashString(password);
            string sql = String.Format("SELECT * FROM [dbo].[{0}] WHERE Username = '{1}' and Password = '{2}'"
                , tableName
                , username
                , hash);
            Dictionary<string, object> results = new Dictionary<string, object>();
            if (DBEndPoint.GetSingleRowSqlSearch(sql, results))
            {
                DBPlayerProfile profile;
                profile = new DBPlayerProfile((string)results["Username"]);
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
        public static KeyValuePair<DBPlayerProfile,string> RegisterPlayer(string username, string password)
        {
            if (!PlayerExist(username)) { 
                string hash = DBPasswordHash.GetHashString(password);
                string sql = String.Format("INSERT INTO [dbo].[{0}] (Username,Password) VALUES('{1}', '{2}'); "
                    , tableName
                    , username
                    , hash);

                var sqlResult = DBEndPoint.ExecuteSQL(sql);
                if (sqlResult.Key)
                {
                    return new KeyValuePair<DBPlayerProfile, string>(GetPlayer(username, password),"Succesfully generated");
                }
                else
                {
                    return new KeyValuePair<DBPlayerProfile, string>(null, "Internal issue");
                }
            }
            else
            {
                return new KeyValuePair<DBPlayerProfile, string>(null,"A player with that name already exist");
            }
        }
    }
}