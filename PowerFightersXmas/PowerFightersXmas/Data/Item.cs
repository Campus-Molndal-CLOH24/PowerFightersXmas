using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Data
{
    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; private set; }
        public int Quantity { get; set; }
        public bool IsContainer { get; }
        public string BoxRed { get; set; } = "Red Box"; 
        public string BoxYellow { get; set; } = "Yellow Box"; 

        public void DisplayInfo()
        {
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Description: {Description}");
            Console.WriteLine($"Quantity: {Quantity}");
        }

        public Item(string name, string description, int quantity = 1)
        {
            Name = name;
            Description = description;
            Items = new List<Item>();
            Quantity = quantity > 0 ? quantity : 1;
            //Box.DisplayInfo();


            // the box is in the basement
            Item BoxRed = new Item("A Red Box", " A Box painted in Red with a Yellow ribbon wrapped around it", 1);

            if (BoxRed.Name == "A Red Box")
            {
                BoxRed.Items.Add(new Item("You Gained An Axe!!", "The axe looks strong, guess it can be used for heavy duties", 1));

            }

            // the box is under the bed in the bedroom on the second floor
            Item BoxYellow = new Item("A Yellow Box", "A Box covered in yellow glitter," +
            " with a golden note attached on the front of the box \n the note reads: Maybe next year", 1);

            if (BoxYellow.Name == "A Yellow Box")
            {
                BoxYellow.Items.Add(new Item("A sock full of Coal", "Guess someone wasn't a good boy, the might come handy tho"));
            }

            // the lamp is in the living room and can be used the basement
            Item Lamp = new Item("A Lamp", "A lamp that can light up the room", 2);

            // the key is in the balcony can be taken from the froozen elf.
            // the key is for the basement door under the bed in the bedroom on the first floor
            Item Key = new Item("A golden key", "The key look important but all the doors are unlocked??", 1);
        }

        private void Open(Item BoxRed)
        {

            Console.WriteLine($"Opening {BoxRed}");
            if (Items.Count == 0)
            {
                Console.WriteLine("It's empty!");
            }
            else
            {
                Console.WriteLine($"You have founded this items inside {BoxRed}:");
                foreach (var item in Items)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }
        }
        private void Open1 (Item BoxYellow)
        {
            Console.WriteLine($"Opening {BoxYellow}");
            if (Items.Count == 0)
            {
                Console.WriteLine("It's empty!");
            }
            else
            {
                Console.WriteLine($"You have founded this items inside {BoxYellow}:");
                foreach (var item in Items)
                {
                    Console.WriteLine($"- {item.Name}: {item.Description}");
                }
            }

        }
    }
}
