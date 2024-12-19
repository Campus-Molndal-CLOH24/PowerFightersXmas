using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static PowerFightersXmas.Data.Item;

namespace PowerFightersXmas.Data
{
    internal class add_data
    {
        public void Adddata()
        {
            // Created items
            var axe = new Item("An Axe", "A sharp axe that can be used for heavy duties", 1);
            var coal = new Item("A Sock Full of Coal", "Guess someone wasn't a good kid!", 1);
            var key = new Item("A Key", "A golden key... there aren't any locked doors!?", 1);
            var lamp = new Item("A Lamp", "A lamp that can light up the room", 2);

            // Created boxes
            var redBox = new Box("Red Box", "A box painted red with a yellow ribbon", BoxColors.Red);
            var yellowBox = new Box("Yellow Box", "A box covered in glitter with a note saying 'Better luck next year!'", BoxColors.Yellow);

            //added boxes
            redBox.Items.Add(axe);
            yellowBox.Items.Add(coal);

            // display inside of the boxes
            redBox.Open();
            yellowBox.Open();
        }

    }
}
