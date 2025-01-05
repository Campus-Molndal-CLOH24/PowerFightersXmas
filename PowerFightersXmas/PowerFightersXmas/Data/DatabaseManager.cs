using PowerFightersXmas.Interface;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace PowerFightersXmas.Data
{
    public static class DatabaseManager
    {
        private const string DatabaseFile = "xmasgame.db";
        private static readonly string ConnectionString = $"Data Source={DatabaseFile};Version=3;";

        public static IDatabaseManager Instance { get; set; }

        // Hämta rumsföremål, filtrera bort föremål som redan är plockade av spelaren
        public static List<Item> GetRoomItems(string roomName, string playerName)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
                SELECT ri.ItemName
                FROM RoomItems ri
                LEFT JOIN PickedUpItems pi ON ri.ItemName = pi.ItemName AND pi.PlayerName = @playerName
                WHERE ri.RoomName = @roomName AND pi.ItemName IS NULL;";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@roomName", roomName);
                    command.Parameters.AddWithValue("@playerName", playerName);

                    using (var reader = command.ExecuteReader())
                    {
                        var items = new List<Item>();
                        while (reader.Read())
                        {
                            items.Add(new Item { Name = reader.GetString(0) });
                        }
                        return items;
                    }
                }
            }
        }

        // Initialisera databasen och skapa tabeller om de inte finns
        public static void InitializeDatabase()
        {
            Console.WriteLine($"Initializing database at: {Path.GetFullPath(DatabaseFile)}");

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                Console.WriteLine("Database connection opened.");

                // SQL för att skapa Player-tabellen
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

                // SQL för att skapa PickedUpItems-tabellen
                string createPickedUpItemsTable = @"
                CREATE TABLE IF NOT EXISTS PickedUpItems (
                    PlayerName TEXT NOT NULL,
                    ItemName TEXT NOT NULL,
                    PRIMARY KEY (PlayerName, ItemName)
                );";
                using (var command = new SQLiteCommand(createPickedUpItemsTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Skapa RoomItems-tabellen
                string createRoomItemsTable = @"
                CREATE TABLE IF NOT EXISTS RoomItems (
                    RoomName TEXT NOT NULL,
                    ItemName TEXT NOT NULL,
                    PRIMARY KEY (RoomName, ItemName)
                );";
                using (var command = new SQLiteCommand(createRoomItemsTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("Database initialized successfully.");
        }

        public static void PopulateRoomItems()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string insertRoomItems = @"
        INSERT OR IGNORE INTO RoomItems (RoomName, ItemName)
        VALUES
        ('Entrance', 'An Axe'),
        ('Kitchen', 'A Sock Full of Coal'),
        ('Office', 'A Key'),
        ('Basement', 'A Lamp');";

                using (var command = new SQLiteCommand(insertRoomItems, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Room items populated successfully.");
            }
        }

        // Markera ett föremål som upplockat för en spelare
        public static void AddPickedUpItem(string playerName, string itemName)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                string query = @"
                INSERT OR IGNORE INTO PickedUpItems (PlayerName, ItemName)
                VALUES (@playerName, @itemName);";

                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@playerName", playerName);
                    command.Parameters.AddWithValue("@itemName", itemName);
                    command.ExecuteNonQuery();
                }
                Console.WriteLine($"Marked '{itemName}' as picked up for player '{playerName}' in the database.");
            }
        }

        // Spara spelarens tillstånd i databasen
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

                Console.WriteLine($"Player '{name}' saved successfully.");
            }
        }

        // Ladda spelarens tillstånd från databasen
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

                            Console.WriteLine($"Loaded player '{name}': Inventory={inventoryJson}, CurrentRoom={currentRoom}");
                            return (inventoryJson, currentRoom);
                        }
                    }
                }
            }

            Console.WriteLine($"Player '{name}' not found in the database.");
            return null;
        }

        // Kontrollera om en spelare existerar i databasen
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
