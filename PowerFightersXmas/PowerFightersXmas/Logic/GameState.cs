using PowerFightersXmas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Logic
{
    public class GameState
    {
        // Spelets centrala data
        public Player Player { get; private set; }
        public Room CurrentRoom { get; set; }

        // Konstruktor för att initiera spelets tillstånd
        public GameState(Player player, Room startingRoom)
        {
            Player = player;
            CurrentRoom = startingRoom;
        }

        // Visar spelets nuvarande tillstånd
        public void ShowState()
        {
            Console.WriteLine($"🎅 Player: {Player.Name}");
            Console.WriteLine($"📍 Current room: {CurrentRoom.Name}");
            Console.WriteLine($"🗺️ Description: {CurrentRoom.Description}");
            Console.WriteLine($"🎁 Inventory: {string.Join(", ", Player.Inventory.Select(i => i.Name))}");
        }

        // Lägger till ett föremål i spelarens inventarie
        public string AddItemToPlayerInventory(Item item)
        {
            if (Player.Inventory.Count < 5) // Max inventory size är 5
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

        // Hämtar ett rum baserat på riktning
        private Room GetRoom(string direction)
        {
            return CurrentRoom.AdjacentRooms.ContainsKey(direction)
                ? CurrentRoom.AdjacentRooms[direction]
                : null;
        }

        // Lägger till angränsande rum från GameState
        public void AddAdjacentRoom(Room fromRoom, string direction, Room toRoom)
        {
            fromRoom.AdjacentRooms[direction] = toRoom;
        }
    }
}
