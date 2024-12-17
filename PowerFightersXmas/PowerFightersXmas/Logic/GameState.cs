using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Logic
{
    public class GameState
    {
        //// Spelets centrala data
        //public Player Player { get; private set; }
        //public Room CurrentRoom { get; set; }

        //// Konstruktor för att initiera spelets tillstånd
        //public GameState(Player player, Room startingRoom)
        //{
        //    Player = player;
        //    CurrentRoom = startingRoom;
        //}

        //// Visar spelets nuvarande tillstånd
        //public void ShowState()
        //{
        //    Console.WriteLine($"🎅 Player: {Player.Name}");
        //    Console.WriteLine($"📍 Current room: {CurrentRoom.Name}");
        //    Console.WriteLine($"🗺️ Description: {CurrentRoom.Description}");
        //    Console.WriteLine($"🎁 Inventory: {string.Join(", ", Player.Inventory.Select(i => i.Name))}");
        //}

        //// Lägger till ett föremål i spelarens inventarie
        //public string AddItemToPlayerInventory(Item item)
        //{
        //    if (Player.AddItem(item))
        //    {
        //        CurrentRoom.RemoveItem(item);
        //        return $"You have picked up {item.Name}.";
        //    }
        //    return "Your inventory is full!";
        //}

        //// Flyttar spelaren till ett annat rum
        //public string MovePlayer(string direction)
        //{
        //    var nextRoom = CurrentRoom.GetRoom(direction);
        //    if (nextRoom != null)
        //    {
        //        CurrentRoom = nextRoom;
        //        return $"Du går {direction} och befinner dig nu i {CurrentRoom.Name}.";
        //    }
        //    return "You can't go there.";
        //}
    }
}
