using System;

namespace DatabaseConnector
{
    internal class SQLTestScript
    {
        static void Main(string[] args)
        {
            //string query = "SELECT TOP (1000) [Id] ,[Username] ,[Password] FROM[SpellBookPact].[dbo].[LoginTable]";
            DBLogin.GetPlayer("MyName","MyPassword");
            Console.WriteLine(Console.ReadKey());
            //DBConnector.ExecuteSQL();
        }

    }
}