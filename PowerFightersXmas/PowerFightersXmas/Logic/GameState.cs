using PowerFightersXmas.Data;
using PowerFightersXmas.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Logic
{
    public class GameState : IGameState
    {
        // Central datastructure for the game state
        public Player Player { get; private set; }
        public Room CurrentRoom { get; set; }

        // Constructor to initialize the game state
        public GameState(Player player)
        {
            Player = player;
            CurrentRoom = RoomInformation.InitializeRooms();
        }

        // Showing the current state of the game
        public void ShowState()
        {
            Console.WriteLine($"\n\t 🎅 Player: {Player.Name}");
            Console.WriteLine($"\t 📍 Current room: {CurrentRoom.Name}");
            Console.WriteLine($"\t 🗺️ Description: {CurrentRoom.Description}");
            Console.WriteLine($"\t 🎁 Inventory: {string.Join(", ", Player.Inventory.Select(i => i.Name))}");
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

        // Tar bort ett föremål från spelarens inventarie
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
            return "You can't go there.";
        }

        // Getting a room in a specific direction
        private Room GetRoom(string direction)
        {
            return CurrentRoom.Exits.ContainsKey(direction)
                ? CurrentRoom.Exits[direction] : new Room("Unknown", "You hit an invisible wall.");
            // Returning a default room if the direction is invalid, if we want to be able to return null, see below

            //return CurrentRoom.Exits.ContainsKey(direction)
            //    ? CurrentRoom.Exits[direction] : null;

            // Alternatively, we could throw an exception if the direction is invalid, or have a null check in the MovePlayer method
        }

        // Lägger till angränsande rum från GameState
        public void AddAdjacentRoom(Room fromRoom, string direction, Room toRoom)
        {
            fromRoom.Exits[direction] = toRoom;
        }
    }
}
