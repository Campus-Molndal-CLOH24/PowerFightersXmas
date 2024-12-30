using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Data
{
    using System.Data.SQLite;

    public static class DatabaseManager
    {
        private const string DatabaseFile = "xmasgame.db";
        private static readonly string ConnectionString = $"Data Source={DatabaseFile};Version=3;";

        public static void InitializeDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string createPlayerTable = @"
            CREATE TABLE IF NOT EXISTS Player (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL UNIQUE,
                Inventory TEXT NOT NULL,
                CurrentRoom TEXT NOT NULL
            );";

                using (var command = new SQLiteCommand(createPlayerTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void SavePlayer(string name, string inventoryJson, string currentRoom)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
                    INSERT OR REPLACE INTO Player (Name, Inventory, CurrentRoom)
                    VALUES (@name, @inventory, @currentRoom);";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@inventory", inventoryJson);
                    command.Parameters.AddWithValue("@currentRoom", currentRoom);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static (string inventoryJson, string currentRoom)? LoadPlayer(string name)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT Inventory, CurrentRoom FROM Player WHERE Name = @name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string inventoryJson = reader.GetString(0);
                            string currentRoom = reader.GetString(1);
                            return (inventoryJson, currentRoom);
                        }
                    }
                }
            }

            return null;
        }

        public static bool PlayerExists(string name)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM Player WHERE Name = @name;";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    return Convert.ToInt32(command.ExecuteScalar()) > 0;
                }
            }
        }
    }
}
