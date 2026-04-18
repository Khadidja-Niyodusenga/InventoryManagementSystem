using System;
using System.Collections.Generic;
using System.Data.SQLite;
using InventoryManagementSystem.data;
using InventoryManagementSystem.models;

namespace InventoryManagementSystem.services
{
    public class AssignmentService
    {
        // ➕ ASSIGN ASSET
        public void AssignAsset(int assetId, int userId)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                // 1. Check if already assigned
                string checkQuery = @"
                SELECT COUNT(*) 
                FROM Assignments 
                WHERE AssetID = @assetId AND IsActive = 1";

                using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@assetId", assetId);
                    long count = (long)checkCmd.ExecuteScalar();

                    if (count > 0)
                        throw new Exception("This asset is already assigned.");
                }

                // 2. Insert assignment
                string insertQuery = @"
                INSERT INTO Assignments 
                (AssetID, UserID, IssueDate, Status, IsActive)
                VALUES 
                (@assetId, @userId, @date, 'Issued', 1)";

                using (var cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@assetId", assetId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);

                    cmd.ExecuteNonQuery();
                }

                // 3. Update asset status
                string updateAsset = @"
                UPDATE Assets 
                SET Status = 'Assigned' 
                WHERE AssetID = @assetId";

                using (var cmd = new SQLiteCommand(updateAsset, conn))
                {
                    cmd.Parameters.AddWithValue("@assetId", assetId);
                    cmd.ExecuteNonQuery();
                }
                var audit = new AuditLogService();

                audit.Log(
                    "Assignment",
                    assetId,
                    "CREATE",
                    "System",
                    $"Asset {assetId} assigned to user {userId}"
                );
            }
        }

        // 🔄 RETURN ASSET
        public void ReturnAsset(int assetId)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                // 1. Update assignment
                string updateQuery = @"
                UPDATE Assignments
                SET ReturnDate = @date,
                    Status = 'Returned',
                    IsActive = 0
                WHERE AssetID = @assetId AND IsActive = 1";

                using (var cmd = new SQLiteCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@date", DateTime.Now);
                    cmd.Parameters.AddWithValue("@assetId", assetId);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows == 0)
                        throw new Exception("No active assignment found.");
                }

                // 2. Update asset status
                string updateAsset = @"
                UPDATE Assets 
                SET Status = 'Available' 
                WHERE AssetID = @assetId";

                using (var cmd = new SQLiteCommand(updateAsset, conn))
                {
                    cmd.Parameters.AddWithValue("@assetId", assetId);
                    cmd.ExecuteNonQuery();
                }
                var audit = new AuditLogService();

                audit.Log(
                    "Assignment",
                    assetId,
                    "UPDATE",
                    "System",
                    $"Asset {assetId} returned"
                );
            }

        }

        // 🔍 ACTIVE ASSIGNMENTS
        public List<Assignment> GetActiveAssignments()
        {
            var list = new List<Assignment>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Assignments WHERE IsActive = 1";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Assignment
                        {
                            AssignmentID = Convert.ToInt32(reader["AssignmentID"]),
                            AssetID = Convert.ToInt32(reader["AssetID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                            ReturnDate = reader["ReturnDate"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(reader["ReturnDate"]),
                            Status = reader["Status"].ToString(),
                            IsActive = Convert.ToInt32(reader["IsActive"])
                        });
                    }
                }
            }

            return list;
        }

        // 📜 ALL HISTORY
        public List<Assignment> GetAllAssignments()
        {
            var list = new List<Assignment>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Assignments";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Assignment
                        {
                            AssignmentID = Convert.ToInt32(reader["AssignmentID"]),
                            AssetID = Convert.ToInt32(reader["AssetID"]),
                            UserID = Convert.ToInt32(reader["UserID"]),
                            IssueDate = Convert.ToDateTime(reader["IssueDate"]),
                            ReturnDate = reader["ReturnDate"] == DBNull.Value
                                ? (DateTime?)null
                                : Convert.ToDateTime(reader["ReturnDate"]),
                            Status = reader["Status"].ToString(),
                            IsActive = Convert.ToInt32(reader["IsActive"])
                        });
                    }
                }
            }

            return list;
        }
    }
}