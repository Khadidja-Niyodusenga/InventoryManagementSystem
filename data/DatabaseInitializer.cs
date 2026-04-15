using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace inventory_system.data
{
    public class DatabaseInitializer
    {
        public static void Initializer()
        {
            using (var connection = Database.GetConnection()
              ) {
                connection.Open();

                CreateAssetsTable(connection);
                CreateUsersTable(connection);
                CreateAssignmentsTable(connection);
                CreateAuditLogsTable(connection);
            }
        }

        private static void CreateAssetsTable(SQLiteConnection conn)
        {
            string query = @"
            CREATE TABLE IF NOT EXISTS Assets (
                AssetID INTEGER PRIMARY KEY AUTOINCREMENT,
                DeviceName TEXT NOT NULL,
                Type TEXT NOT NULL,
                SerialNumber TEXT UNIQUE NOT NULL,
                Specifications TEXT,
                ConditionStatus TEXT CHECK(ConditionStatus IN ('Good','Needs Maintenance','Damaged')),
                Status TEXT CHECK(Status IN ('Available','Assigned','Under Repair')) DEFAULT 'Available',
                Department TEXT,
                CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
            );";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static void CreateUsersTable(SQLiteConnection conn)
        {
            string query = @"
            CREATE TABLE IF NOT EXISTS Users (
                UserID INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Department TEXT
            );";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static void CreateAssignmentsTable(SQLiteConnection conn)
        {
            string query = @"
            CREATE TABLE IF NOT EXISTS Assignments (
                AssignmentID INTEGER PRIMARY KEY AUTOINCREMENT,
                AssetID INTEGER NOT NULL,
                UserID INTEGER NOT NULL,
                IssueDate DATETIME NOT NULL,
                ReturnDate DATETIME,
                ConditionBefore TEXT,
                ConditionAfter TEXT,
                FOREIGN KEY (AssetID) REFERENCES Assets(AssetID),
                FOREIGN KEY (UserID) REFERENCES Users(UserID)
            );";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static void CreateAuditLogsTable(SQLiteConnection conn)
        {
            string query = @"
            CREATE TABLE IF NOT EXISTS AuditLogs (
                LogID INTEGER PRIMARY KEY AUTOINCREMENT,
                EntityType TEXT,
                EntityID INTEGER,
                Action TEXT,
                PerformedBy TEXT,
                Timestamp DATETIME DEFAULT CURRENT_TIMESTAMP,
                Description TEXT
            );";

            using (var cmd = new SQLiteCommand(query, conn))
            {
                cmd.ExecuteNonQuery();
            }
        }
    }

}
