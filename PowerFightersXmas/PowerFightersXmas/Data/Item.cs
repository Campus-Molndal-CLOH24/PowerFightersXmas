using System;
using System.Collections.Generic;

namespace PowerFightersXmas.Data
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public List<Item> Items { get; set; }= new List<Item>();

        public Item()
        {
            Name = string.Empty;
            Description = string.Empty;
        }

        public Item(string name, string description, int quantity = 1)
        {
            Name = name;
            Description = description;
            Quantity = quantity > 0 ? quantity : 1;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Quantity: {Quantity}");
        }
    }
}