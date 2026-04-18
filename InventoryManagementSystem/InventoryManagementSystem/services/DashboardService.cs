
using InventoryManagementSystem.data;
using System;
using System.Data.SQLite;

namespace inventory_system.services
{
    public class DashboardService
    {
        public int GetTotalAssets()
        {
            return ExecuteCount("SELECT COUNT(*) FROM Assets WHERE IsActive = 1");
        }

        public int GetAssignedAssets()
        {
            return ExecuteCount("SELECT COUNT(*) FROM Assets WHERE Status = 'Assigned' AND IsActive = 1");
        }

        public int GetAvailableAssets()
        {
            return ExecuteCount("SELECT COUNT(*) FROM Assets WHERE Status = 'Available' AND IsActive = 1");
        }

        public int GetDamagedAssets()
        {
            return ExecuteCount(@"
                SELECT COUNT(*) FROM Assets 
                WHERE ConditionStatus IN ('Damaged', 'Needs Maintenance')
                AND IsActive = 1");
        }

        public int GetTotalUsers()
        {
            return ExecuteCount("SELECT COUNT(*) FROM Users WHERE IsActive = 1");
        }

        public int GetActiveAssignments()
        {
            return ExecuteCount("SELECT COUNT(*) FROM Assignments WHERE IsActive = 1");
        }

        private int ExecuteCount(string query)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}