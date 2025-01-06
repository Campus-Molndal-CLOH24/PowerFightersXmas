using System;
using System.Linq;
using Newtonsoft.Json;
using PowerFightersXmas.Data;
using PowerFightersXmas.Interface;

namespace PowerFightersXmas.Logic
{
    public class GameState : IGameState
    {
        // Central datastructure for the game state
        public Player Player { get; private set; }
        public Room CurrentRoom { get; set; }

        // Inventory property to get the player's inventory
        public List<Item> Inventory => Player.Inventory;
        public string? DecorateMessage { get; set; }
        // Constructor to initialize the game state
        public GameState(Player player)
        {
            Player = player;
            CurrentRoom = RoomInformation.InitializeRooms(player.Name); // Dynamiskt laddning av rum och föremål
        }
        private void DisplayQuestLog()
        {
            // Lista över quest-items
            var questItems = new List<string>
    {
        "Glitter",
        "Christmas baubles",
        "Christmas tree lights",
        "Christmas tree star"
    };

            Console.WriteLine("\n\t 🎄 Quest: Decorate the Christmas Tree!");
            Console.WriteLine("\t - Decorations needed:");

            // Kontrollera vilka föremål som finns i spelarens inventory
            foreach (var item in questItems)
            {
                if (Player.Inventory.Any(i => i.Name == item))
                {
                    Console.WriteLine($"\t ✅ {item} (collected)");
                }
                else
                {
                    Console.WriteLine($"\t ❌ {item}");
                }
            }
        }
        // Showing the current state of the game
        public void ShowState()
        {
            DisplayQuestLog();
            Console.WriteLine($"\n\t 📍 Current room: {CurrentRoom.Name}");
            Console.WriteLine($"\t 🗺️ Description: {CurrentRoom.Description}");
            Console.WriteLine($"\t 🎁 Inventory: {string.Join(", ", Player.Inventory.Select(i => i.Name))}");

            // Dynamiskt hämta synliga föremål från databasen
            var visibleItems = DatabaseManager.GetRoomItems(CurrentRoom.Name, Player.Name);
            if (visibleItems.Any())
            {
                Console.WriteLine($"\t 📦 Items in room:");
                foreach (var item in visibleItems)
                {
                    Console.WriteLine($"\t - {item.Name}");
                }
            }
            else
            {
                Console.WriteLine($"\t 📦 There are no items in this room.");
            }

            // Print the map of the room
            var mapHandler = new MapHandler();
            mapHandler.DisplayMap(CurrentRoom.Name);

            // Skriv ut dekorationsmeddelandet sist
            if (!string.IsNullOrEmpty(DecorateMessage))
            {
                Console.WriteLine(DecorateMessage);
                DecorateMessage = null; // Töm meddelandet för att undvika att det skrivs ut igen
            }
        }

        public List<Item> GetCurrentRoomItems() => CurrentRoom.Items;

        // Adding an item to the player's inventory
        public string AddItemToPlayerInventory(Item item)
        {
            if (Player.Inventory.Count < 5) // Max inventory size is 5
            {
                // Lägg till föremålet i spelarens inventarie
                Player.Inventory.Add(item);

                // Ta bort föremålet från det aktuella rummet
                CurrentRoom.Items.Remove(item);

                // Markera föremålet som upplockat i databasen
                DatabaseManager.AddPickedUpItem(Player.Name, item.Name);

                // Auto-spara spelets tillstånd
                SaveGameState();

                return $"You have picked up {item.Name}.";
            }

            return "Your inventory is full!";
        }

        // Flyttar spelaren till ett annat rum
        public string MovePlayer(string direction)
        {
            var nextRoom = GetRoom(direction);
            if (nextRoom != null)
            {
                CurrentRoom = nextRoom;
                return $"You walk {direction} and now find yourself in {CurrentRoom.Name}.";
            }
            else
            {
                return "You can't go there.";
            }
        }

        // Getting a room in a specific direction
        private Room? GetRoom(string direction)
        {
            // Kontrollera om riktningen finns i Exits, returnera rummet om det finns annars returnera null
            return CurrentRoom.Exits.ContainsKey(direction)
                ? CurrentRoom.Exits[direction]
                : null; // Inga fler "Unknown"-rum skapas
        }

        // Lägger till angränsande rum från GameState
        public void AddAdjacentRoom(Room fromRoom, string direction, Room toRoom)
        {
            fromRoom.Exits[direction] = toRoom;
        }

        public void SaveGameState()
        {
            Console.WriteLine("Saving game state to SQLite database...");

            // Skapa en lista med endast föremålsnamn
            var inventoryNames = Player.Inventory.Select(item => item.Name).ToList();
            string inventoryJson = JsonConvert.SerializeObject(inventoryNames);
            Console.WriteLine($"Serialized inventory (names only): {inventoryJson}");

            DatabaseManager.SavePlayer(Player.Name, inventoryJson, CurrentRoom.Name);
            Console.WriteLine("Game state saved to database.");
        }

        public static GameState? LoadGameState(string playerName)
        {
            Console.WriteLine($"Loading game state for player: {playerName} from SQLite database...");
            var playerData = DatabaseManager.LoadPlayer(playerName);
            if (playerData == null)
            {
                Console.WriteLine("Player data not found in the database.");
                return null;
            }

            // Deserialisera inventariet (föremålsnamn)
            var inventoryNames = JsonConvert.DeserializeObject<List<string>>(playerData.Value.inventoryJson) ?? new List<string>();
            var inventory = inventoryNames.Select(name => new Item { Name = name }).ToList(); // Skapa nya föremål med bara namn

            var player = new Player(playerName) { Inventory = inventory };

            // Dynamiskt hämta aktuellt rum för spelaren
            var room = RoomInformation.FindRoom(playerData.Value.currentRoom, playerName);
            if (room == null)
            {
                Console.WriteLine($"Error: Room '{playerData.Value.currentRoom}' not found. Defaulting to Entrance.");
                room = RoomInformation.InitializeRooms(playerName);
            }

            return new GameState(player) { CurrentRoom = room };
        }
    }
}
