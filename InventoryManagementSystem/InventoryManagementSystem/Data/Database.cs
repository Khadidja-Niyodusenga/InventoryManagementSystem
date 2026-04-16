using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace inventory_system.data
{
   public class Database
    {
        private static string connectionString = "Data Source=inventory.db;Version=3";
            
        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

    }
}
