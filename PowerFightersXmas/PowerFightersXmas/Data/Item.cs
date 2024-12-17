using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public List<Item> Items { get; set; }


        public Item(string name, string description, int quantity = 1)
        {
            Name = name;
            Description = description;
            Quantity = quantity > 0 ? quantity : 1;

            Item Axe = new Item("An Axe", "A sharp axe that can be used for heavy duties", 1);
            Item Coal = new Item("A sock full of Coal", "Guess someone wasn't a good boy, the might come handy tho", 1);
            Item Lamp = new Item("A Lamp", "A lamp that can light up the room", 2);

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
            public List<Item> Items { get; set; }

            public Box(string name, string description, string colour)
            {
                Name = name;
                Description = description;
                Colour = colour;
                Items = new List<Item>();

                Box Red = new Box("Red Box", "A Box painted in Red with a Yellow ribbon wrapped around it", BoxColors.Red);
                Box Yellow = new Box("Yellow Box",
                "A Box covered in yellow glitter, with a golden note attached on the front of the box \n the note reads: Maybe next year",
                BoxColors.Yellow);
            }

            public void Open()
            {
                if (BoxColors.Red == "Red")
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


                while (BoxColors.Red == "Red")
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
}
    

            
