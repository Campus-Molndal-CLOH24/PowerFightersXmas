using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PowerFightersXmas.Logic
{
    using Newtonsoft.Json;
    using PowerFightersXmas.Data;
    using PowerFightersXmas.Interface;
    using PowerFightersXmas.UI;

    public class GameState : IGameState
    {
        // Central datastructure for the game state
        public Player Player { get; private set; }
        public Room CurrentRoom { get; set; }

        // Inventory property to get the player's inventory
        public List<Item> Inventory => Player.Inventory;
        private const string SaveFilePath = "./docs/savegame.json";

        // Constructor to initialize the game state
        public GameState(Player player)
        {
            Player = player;
            CurrentRoom = RoomInformation.InitializeRooms();
        }

        // Showing the current state of the game
        public void ShowState()
        {
            
            Console.WriteLine($"\n\t 📍 Current room: {CurrentRoom.Name}");
            Console.WriteLine($"\t 🗺️ Description: {CurrentRoom.Description}");
            Console.WriteLine($"\t 🎁 Inventory: {string.Join(", ", Player.Inventory.Select(i => i.Name))}");

            // Print the map of the room
            var mapHandler = new MapHandler();
            mapHandler.DisplayMap(CurrentRoom.Name);
        }

        public List<Item> GetCurrentRoomItems() => CurrentRoom.Items;

        // Adding an item to the player's inventory
        public string AddItemToPlayerInventory(Item item)
        {
            if (Player.Inventory.Count < 5) // Max inventory size is 5
            {
                Player.Inventory.Add(item);
                CurrentRoom.Items.Remove(item);
                return $"You have picked up {item.Name}.";
            }
            return "Your inventory is full!";
        }

        // Remove an object from the players inventory
        public string RemoveItemFromPlayerInventory(Item item)
        {
            if (Player.Inventory.Contains(item))
            {
                Player.Inventory.Remove(item);
                CurrentRoom.Items.Add(item);
                return $"You have dropped {item.Name}.";
            }
            return "You don't have that item!";
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
                return "\nYou can't go there."; // Uppdaterat felmeddelande
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
            string inventoryJson = JsonConvert.SerializeObject(Player.Inventory);
            DatabaseManager.SavePlayer(Player.Name, inventoryJson, CurrentRoom.Name);
        }

        public static GameState? LoadGameState(string playerName)
        {
            var playerData = DatabaseManager.LoadPlayer(playerName);
            if (playerData == null)
            {
                Console.WriteLine("Player data not found in the database.");
                return null;
            }

            var inventory = JsonConvert.DeserializeObject<List<Item>>(playerData.Value.inventoryJson) ?? new List<Item>();
            var player = new Player(playerName) { Inventory = inventory };

            var room = RoomInformation.FindRoom(playerData.Value.currentRoom);
            if (room == null)
            {
                Console.WriteLine($"Error: Room '{playerData.Value.currentRoom}' not found. Defaulting to Entrance.");
                room = RoomInformation.InitializeRooms(); // Sätt ett standardrum som t.ex. "Entrance".
            }

            return new GameState(player)
            {
                CurrentRoom = room
            };
        }
    }
}
