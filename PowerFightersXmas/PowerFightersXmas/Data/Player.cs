using PowerFightersXmas.Data;
using PowerFightersXmas.Interface;
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

        public Player(string name)
        {
        Name = name;
        Inventory = new List<Item>();
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
