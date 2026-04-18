using InventoryManagementSystem.data;
using InventoryManagementSystem.models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace InventoryManagementSystem.services
{
    public class UserService
    {
        // CREATE
        public void AddUser(User user)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"
                INSERT INTO Users (Name, Department, IsActive)
                VALUES (@name, @dept, 1)";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@dept", user.Department);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // READ (ALL ACTIVE USERS)
        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "SELECT * FROM Users WHERE IsActive = 1";

                using (var cmd = new SQLiteCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {
                            UserID = Convert.ToInt32(reader["UserID"]),
                            Name = reader["Name"].ToString(),
                            Department = reader["Department"].ToString(),
                            IsActive = Convert.ToInt32(reader["IsActive"])
                        });
                    }
                }
            }

            return users;
        }

        // UPDATE
        public void UpdateUser(User user)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = @"
                UPDATE Users 
                SET Name = @name,
                    Department = @dept
                WHERE UserID = @id";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@name", user.Name);
                    cmd.Parameters.AddWithValue("@dept", user.Department);
                    cmd.Parameters.AddWithValue("@id", user.UserID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // DEACTIVATE (NO DELETE)
        public void DeactivateUser(int userId)
        {
            using (var conn = Database.GetConnection())
            {
                conn.Open();

                string query = "UPDATE Users SET IsActive = 0 WHERE UserID = @id";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}