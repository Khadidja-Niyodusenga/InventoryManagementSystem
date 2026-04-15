using System.Data.SQLite;
using inventory_system.data;

public static class DatabaseMigration
{
    // 🔥 Generic reusable method
    private static void AddColumnIfMissing(string tableName, string columnName, string columnDefinition)
    {
        using (var conn = Database.GetConnection())
        {
            conn.Open();

            string pragmaQuery = $"PRAGMA table_info({tableName});";

            using (var cmd = new SQLiteCommand(pragmaQuery, conn))
            using (var reader = cmd.ExecuteReader())
            {
                bool exists = false;

                while (reader.Read())
                {
                    if (reader["name"].ToString() == columnName)
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    string alterQuery =
                        $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnDefinition};";

                    using (var alterCmd = new SQLiteCommand(alterQuery, conn))
                    {
                        alterCmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }

 //Asset
    public static void EnsureAssetsColumns()
    {
        AddColumnIfMissing("Assets", "IsActive", "INTEGER DEFAULT 1");
    }

    // USERS
    public static void EnsureUsersColumns()
    {
        AddColumnIfMissing("Users", "IsActive", "INTEGER DEFAULT 1");
    }

    //Assignments
    public static void EnsureAssignmentsColumns()
    {
        AddColumnIfMissing("Assignments", "IsActive", "INTEGER DEFAULT 1");

  
        AddColumnIfMissing("Assignments", "Status", "TEXT DEFAULT 'Issued'");
    }
}