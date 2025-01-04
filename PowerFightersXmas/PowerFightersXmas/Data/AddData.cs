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
        private List<Box> _boxes = new List<Box>();

        public void AddItems()
        {
            // Skapa föremål
            var axe = new Item("An Axe", "A sharp axe that can be used for heavy duties", 1);
            var coal = new Item("A Sock Full of Coal", "Guess someone wasn't a good kid!", 1);
            var key = new Item("A Key", "A golden key... there aren't any locked doors!?", 1);
            var lamp = new Item("A Lamp", "A lamp that can light up the room", 2);

            // Lägg till föremål i listan
            _items.Add(axe);
            _items.Add(coal);
            _items.Add(key);
            _items.Add(lamp);

            // Skapa lådor
            var redBox = new Box("Red Box", "A box painted red with a yellow ribbon", BoxColors.Red);
            var yellowBox = new Box("Yellow Box", "A box covered in glitter with a note saying 'Better luck next year!'", BoxColors.Yellow);

            // Lägg till föremål i lådorna
            redBox.Items.Add(axe);
            yellowBox.Items.Add(coal);

            // Lägg till lådor i listan
            _boxes.Add(redBox);
            _boxes.Add(yellowBox);

            // Visa innehållet i lådorna
            redBox.Open();
            yellowBox.Open();
        }

        // Metod för att hämta alla föremål
        public List<Item> GetItems()
        {
            return _items;
        }

        // Metod för att hämta alla lådor
        public List<Box> GetBoxes()
        {
            return _boxes;
        }
    }
}
