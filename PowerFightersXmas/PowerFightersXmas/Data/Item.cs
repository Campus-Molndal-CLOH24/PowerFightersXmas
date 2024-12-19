using System;
using System.Collections.Generic;

namespace PowerFightersXmas.Data
{
    public static class BoxColors
    {
        public const string Red = "Red";
        public const string Yellow = "Yellow";
    }

    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public List<Item> Items { get; set; }= new List<Item>();

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

        public class Box
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Colour { get; set; }
            public List<Item> Items { get; set; } = new List<Item>();

            public Box(string name, string description, string colour)
            {
                Name = name;
                Description = description;
                Colour = colour;
                Items = new List<Item>();  
            }

            public void Open()
            {
                if (Items.Count == 0)
                {
                    Console.WriteLine("It's empty!");
                }
                else
                {
                    Console.WriteLine($"You found the following items inside {Name}:");
                    foreach (var item in Items)
                    {
                        Console.WriteLine($"- {item.Name}: {item.Description}");
                    }
                }
            }
        }
    }
}