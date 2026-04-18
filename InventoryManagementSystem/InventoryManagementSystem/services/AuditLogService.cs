using System;
using System.Collections.Generic;
using System.Data.SQLite;
using InventoryManagementSystem.data;
using InventoryManagementSystem.models;

namespace InventoryManagementSystem.services
{
    public class AuditLogService
    {
        // ➕ CREATE LOG ENTRY
        public void Log(string entityType, int entityId, string action, string performedBy, string description)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"
                INSERT INTO AuditLogs
                (EntityType, EntityID, Action, PerformedBy, Description)
                VALUES
                (@type, @id, @action, @by, @desc)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@type", entityType);
                    cmd.Parameters.AddWithValue("@id", entityId);
                    cmd.Parameters.AddWithValue("@action", action);
                    cmd.Parameters.AddWithValue("@by", performedBy);
                    cmd.Parameters.AddWithValue("@desc", description);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 🔍 VIEW LOGS
        public List<AuditLog> GetAllLogs()
        {
            var logs = new List<AuditLog>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM AuditLogs ORDER BY Timestamp DESC";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new AuditLog
                        {
                            LogID = Convert.ToInt32(reader["LogID"]),
                            EntityType = reader["EntityType"].ToString(),
                            EntityID = Convert.ToInt32(reader["EntityID"]),
                            Action = reader["Action"].ToString(),
                            PerformedBy = reader["PerformedBy"].ToString(),
                            Timestamp = reader["Timestamp"].ToString(),
                            Description = reader["Description"].ToString()
                        });
                    }
                }
            }

            return logs;
        }
    }
}