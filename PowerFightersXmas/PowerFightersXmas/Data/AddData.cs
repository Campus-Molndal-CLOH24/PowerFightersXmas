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
            var axe = new Item("An Axe", "A sharp axe that can be used for heavy duties", 1);
            var coal = new Item("A Sock Full of Coal", "Guess someone wasn't a good kid!", 1);
            var key = new Item("A Key", "A golden key... there aren't any locked doors!?", 1);
            var lamp = new Item("A Lamp", "A lamp that can light up the room", 1);

            // Lägg till föremål i listan
            _items.Add(axe);
            _items.Add(coal);
            _items.Add(key);
            _items.Add(lamp);

            
        }

        // Metod för att hämta alla föremål
        public List<Item> GetItems()
        {
            return _items;
        }

        // Metod för att hämta alla lådor
    }
}
