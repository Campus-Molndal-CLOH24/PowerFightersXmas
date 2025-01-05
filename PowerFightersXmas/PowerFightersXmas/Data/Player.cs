using PowerFightersXmas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Data 
{ 
    public class Player
    {
        public string Name { get; set; }
        public List<Item> Inventory { get; set; }

    //For npc
    //public int ID { get; set; } 

    /*public int Health { get; set; }
    public int Strength { get; set; }
    public int Defense { get; set; }
    */

      public Player(string name)
      {
        Name = name;
        Inventory = new List<Item>();

            // The code below was removed, if we create a player inside the Player class, we will have an infinite loop
            // Player Mainplayer = new Player("Jedi Bob"); - > This will create a new player, which will create a new player, which will create a new player, and so on

            /*Health = maxHealth; 
             Strength = strength;
             Defense = defense;
            */
        }

        public void ShowInventory()
      {
        Console.WriteLine($"{Name}'s Inventory:");
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Inventory is empty.");
        }
        else
        {
            foreach (var item in Inventory)
            {
                Console.WriteLine($"- {item.Name}: {item.Description}");
            }
        }
    }
  }
}
