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

            //var axe = new Item("An Axe", "A sharp axe that can be used for heavy duties", 1);
            //var coal = new Item("A Sock Full of Coal", "Guess someone wasn't a good kid!", 1);
            //var lamp = new Item("A Lamp", "A lamp that can light up the room", 2);
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

                // Skapa exempelboxar
                //var redBox = new Box("Red Box", "A box painted red with a yellow ribbon", BoxColors.Red);
                //var yellowBox = new Box("Yellow Box", "A box covered in glitter with a note saying 'Better luck next year!'", BoxColors.Yellow);
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

                // The code below creates infinite loops so I adjusted it to the above code, let me know if you want me to revert it back to the original code. /NH

                //if (BoxColors.Red == "red")
                //{
                //    if (Items.Count == 0)
                //    {
                //        Console.WriteLine("it's empty!");
                //    }
                //    else
                //    {
                //        Console.WriteLine($"you found the following items inside {Name}:");
                //        foreach (var item in Items)
                //        {
                //            Console.WriteLine($"- {item.Name}: {item.Description}");
                //        }
                //    }
                //}


                //while (BoxColors.Red == "red")
                //{
                //    if (Items.Count == 0)
                //    {
                //        Console.WriteLine("it's empty!");
                //    }
                //    else
                //    {
                //        Console.WriteLine($"you found the following items inside {Name}:");
                //        foreach (var item in Items)
                //        {
                //            Console.WriteLine($"- {item.Name}: {item.Description}");
                //        }
                //    }
                //}
            }
        }
    }
}