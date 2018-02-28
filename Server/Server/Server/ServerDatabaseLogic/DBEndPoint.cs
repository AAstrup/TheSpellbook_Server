using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace DatabaseConnector
{
    public class DBEndPoint
    {
        /// <summary>
        /// Get the connection to the server
        /// This is obviously temporary login information and implementation
        /// </summary>
        /// <returns></returns>
        public static SqlConnection GetConnection()
        {
            return new SqlConnection("user id=guest;" +
                           "server=DESKTOP-VOQGIC8\\SQLEXPRESS;" +
                           "Trusted_Connection=yes;" +
                           "database=SpellBookPact; " +
                           "connection timeout=30");
        }

        /// <summary>
        /// Executes sql represented as strings
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>Returns whether or not it succeded, and possible error messages</returns>
        public static KeyValuePair<bool,string> ExecuteSQL(string sql)
        {
            SqlConnection connection = DBEndPoint.GetConnection();
            SqlCommand command;

            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                command.ExecuteNonQuery();
                command.Dispose();
                connection.Close();
                return new KeyValuePair<bool, string>(true,"Command succesful");
            }
            catch (Exception ex)
            {
                return new KeyValuePair<bool, string>(false, ex.ToString());
            }
        }

        /// <summary>
        /// Gets all columns from a single row
        /// </summary>
        /// <param name="sql">Sql to execute</param>
        /// <param name="listToPopulate">Column name and value found</param>
        /// <returns>If a row was found</returns>
        public static bool GetSingleRowSqlSearch(string sql, Dictionary<string, object> listToPopulate)
        {
            SqlConnection connection = DBEndPoint.GetConnection();

            SqlCommand command;
            SqlDataReader dataReader;
            bool succes = false;
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                dataReader = command.ExecuteReader();
                if (dataReader.Read())
                {
                    for (int i = 0; i < dataReader.VisibleFieldCount; i++)
                    {
                        listToPopulate.Add(dataReader.GetName(i).Trim(), dataReader.GetValue(i));
                    }
                    succes = true;
                }
                dataReader.Close();
                command.Dispose();
                connection.Close();
            }
            catch (Exception ex)
            {
                string msg = "Exception caught in SQL " + ex.ToString();
                Console.WriteLine(msg);
            }
            return succes;
        }
    }
}
