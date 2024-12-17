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

        public Room(string name, string description)
        {
            Name = name;
            Description = description;
            Items = new List<Item>();
            Exits = new Dictionary<string, Room>();
        }
    }
}
