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
            Console.WriteLine($"🎅 Spelare: {Player.Name}");
            Console.WriteLine($"📍 Nuvarande rum: {CurrentRoom.Name}");
            Console.WriteLine($"🗺️ Beskrivning: {CurrentRoom.Description}");
            Console.WriteLine($"🎁 Inventarie: {string.Join(", ", Player.Inventory.Select(i => i.Name))}");
        }

        // Lägger till ett föremål i spelarens inventarie
        public string AddItemToPlayerInventory(Item item)
        {
            if (Player.AddItem(item))
            {
                CurrentRoom.RemoveItem(item);
                return $"Du har plockat upp {item.Name}.";
            }
            return "Din väska är full!";
        }

        // Flyttar spelaren till ett annat rum
        public string MovePlayer(string direction)
        {
            var nextRoom = CurrentRoom.GetRoom(direction);
            if (nextRoom != null)
            {
                CurrentRoom = nextRoom;
                return $"Du går {direction} och befinner dig nu i {CurrentRoom.Name}.";
            }
            return "Du kan inte gå dit.";
        }
    }
}
