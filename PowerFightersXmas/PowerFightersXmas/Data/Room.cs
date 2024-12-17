using PowerFightersXmas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Data
{
    public class Room 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; private set; }
        public Dictionary<string, Room> Exits { get; private set; }
    }

    public Room(string name, string description)
        {
            Name = name;
            Description = description;
            Items = new List<Item>();
            Exits = new Dictionary<string, Room>();

            Room Entrance = new Room("Entrance", "Nice house with a lot of rooms... \n Oh look Santa");

            Room WC = new Room("WC", "This is the WC... Eww so dirty");

            Room Kitchen = new Room("Kitchen", "This is the kitchen, it's a mess");

            Room LivingRoom = new Room("Living Room",
            "This is the living room, it's a cozy place \n you can use the stairs to enter the second Floor");

            Room Pool = new Room("OutDoor Pool", "This is a pool, it's Frozen solid... \n is that an Elf stuck in the ice");

            Room Office = new Room("Office", "Looks like an ordinary office...");

            Room SecondFloor = new Room("Second Floor Entrance", "Wow this place is big");

            Room Bar = new Room("Looks like a bar...", "This is a room with a pool table and a fire place... " +
            "But i need to sart the fire");

            Room Balcony = new Room("Balcony", "❄️❄️This place is so cold❄️❄️... Is that a frozen Elf");

            Room Bedroom = new Room("Bedroom", "This is a bedroom, it's a mess guess some one wasn't a good kid this Xmas");

            Room basement = new Room("Basement",
            "This place is so dark you can't see anything... \n I need to find a lamp");

            /*basement.Items.Add(new Item("Tomteverkstadens nyckel",
             "En nyckel som ser ut att passa i en dörr."));*/

            //Exits.Add("north", basement);
        }

    }

