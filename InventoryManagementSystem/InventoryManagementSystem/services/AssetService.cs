using InventoryManagementSystem.data;
using InventoryManagementSystem.models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryManagementSystem.services
{
    public class AssetService
    {
        public void AddAsset(Asset asset)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"INSERT INTO Assets 
                (DeviceName, Type, SerialNumber, Specifications, ConditionStatus, Status, Department)
                VALUES (@name, @type, @serial, @specs, @condition, 'Available', @dept)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", asset.DeviceName);
                    cmd.Parameters.AddWithValue("@type", asset.Type);
                    cmd.Parameters.AddWithValue("@serial", asset.SerialNumber);
                    cmd.Parameters.AddWithValue("@specs", asset.Specifications);
                    cmd.Parameters.AddWithValue("@condition", asset.ConditionStatus);
                    cmd.Parameters.AddWithValue("@dept", asset.Department);

                    cmd.ExecuteNonQuery();
                }

                // AUDIT LOG
                var audit = new AuditLogService();

                audit.Log(
                    "Asset",
                    0,
                    "CREATE",
                    "System",
                    $"Asset created: {asset.DeviceName} ({asset.SerialNumber})"
                );
            }
        }

        public List<Asset> GetAllAssets()
        {
            var assets = new List<Asset>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Assets WHERE IsActive = 1";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var asset = new Asset
                        {
                            AssetID = Convert.ToInt32(reader["AssetID"]),
                            DeviceName = reader["DeviceName"].ToString(),
                            Type = reader["Type"].ToString(),
                            SerialNumber = reader["SerialNumber"].ToString(),
                            Specifications = reader["Specifications"].ToString(),
                            ConditionStatus = reader["ConditionStatus"].ToString(),
                            Status = reader["Status"].ToString(),
                            Department = reader["Department"].ToString()
                        };

                        assets.Add(asset);
                    }
                }
            }

            return assets;
        }

        public void UpdateAsset(Asset asset)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"UPDATE Assets SET 
                DeviceName = @name,
                Type = @type,
                SerialNumber = @serial,
                Specifications = @specs,
                ConditionStatus = @condition,
                Status = @status,
                Department = @dept
                 WHERE AssetID = @id";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", asset.DeviceName);
                    cmd.Parameters.AddWithValue("@type", asset.Type);
                    cmd.Parameters.AddWithValue("@serial", asset.SerialNumber);
                    cmd.Parameters.AddWithValue("@specs", asset.Specifications);
                    cmd.Parameters.AddWithValue("@condition", asset.ConditionStatus);
                    cmd.Parameters.AddWithValue("@status", asset.Status);
                    cmd.Parameters.AddWithValue("@dept", asset.Department);
                    cmd.Parameters.AddWithValue("@id", asset.AssetID);

                    cmd.ExecuteNonQuery();
                }
                //AUDIT LOG
                var audit = new AuditLogService();

                audit.Log(
                    "Asset",
                    asset.AssetID,
                    "UPDATE",
                    "System",
                    $"Asset updated: {asset.DeviceName}"
                );
            }
        }

        public void DeactivateAsset(int assetId)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "UPDATE Assets SET IsActive = 0 WHERE AssetID = @id";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", assetId);
                    cmd.ExecuteNonQuery();
                }

                var audit = new AuditLogService();

                audit.Log(
                    "Asset",
                    assetId,
                    "DELETE",
                    "System",
                    $"Asset deactivated (soft delete)"
                );
            }
        }
    }
}