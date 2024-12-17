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
        public string name { get; set; }
        public string description { get; set; }
        public int quantity { get; set; }
        public list<item> items { get; set; }


        public item(string name, string description, int quantity = 1)
        {
            name = name;
            description = description;
            quantity = quantity > 0 ? quantity : 1;

            item axe = new item("an axe", "a sharp axe that can be used for heavy duties", 1);
            item coal = new item("a sock full of coal", "guess someone wasn't a good boy, the might come handy tho", 1);
            item lamp = new item("a lamp", "a lamp that can light up the room", 2);

        }

        public void displayinfo()
        {
            console.writeline($"name: {name}");
            console.writeline($"description: {description}");
            console.writeline($"quantity: {quantity}");
        }

        public class box
        {
            public string name { get; set; }
            public string description { get; set; }
            public string colour { get; set; }
            public list<item> items { get; set; }

            public box(string name, string description, string colour)
            {
                name = name;
                description = description;
                colour = colour;
                items = new list<item>();

                box red = new box("red box", "a box painted in red with a yellow ribbon wrapped around it", boxcolors.red);
                box yellow = new box("yellow box",
                "a box covered in yellow glitter, with a golden note attached on the front of the box \n the note reads: maybe next year",
                boxcolors.yellow);
            }

            public void open()
            {
                if (boxcolors.red == "red")
                {
                    if (items.count == 0)
                    {
                        console.writeline("it's empty!");
                    }
                    else
                    {
                        console.writeline($"you found the following items inside {name}:");
                        foreach (var item in items)
                        {
                            console.writeline($"- {item.name}: {item.description}");
                        }
                    }
                }


                while (boxcolors.red == "red")
                {
                    if (items.count == 0)
                    {
                        console.writeline("it's empty!");
                    }
                    else
                    {
                        console.writeline($"you found the following items inside {name}:");
                        foreach (var item in items)
                        {
                            console.writeline($"- {item.name}: {item.description}");
                        }
                    }
                }
            }
        }
    }
}
    

            
