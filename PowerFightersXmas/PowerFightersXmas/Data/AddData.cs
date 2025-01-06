using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static PowerFightersXmas.Data.Item;

namespace PowerFightersXmas.Data
{
    internal class AddData
    {
        // Lista för att lagra alla föremål och lådor
        private List<Item> _items = new List<Item>();

        public void AddItems()
        {
            // Skapa föremål
            var glitter = new Item(name: "Glitter", description: "Shiny glitter that sparkles!", quantity: 1);
            var baubles = new Item(name: "Christmas baubles", description: "Beautiful baubles for decorating the tree.", quantity: 1);
            var lights = new Item(name: "Christmas tree lights", description: "Twinkling lights for a cozy Christmas.", quantity: 1);
            var star = new Item(name: "Christmas tree star", description: "A star to crown the Christmas tree.", quantity: 1);

            // Lägg till föremål i listan
            _items.Add(glitter);
            _items.Add(baubles);
            _items.Add(lights);
            _items.Add(star);
        }

        // Metod för att hämta alla föremål
        public List<Item> GetItems()
        {
            return _items;
        }

        // Metod för att hämta alla lådor
    }
}
