using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.UI
{
    internal class MapHandler
    {
        public void DisplayMap(string currentRoom)
        {
            string[] asciiMap = {
         "+---------------------------------+",
          "|  Living   |   Kitchen  | Office |",
          "|   Room    |            |        |",
          "|   {0}       |    {1}       |     {2}  |",
          "|           |            |        |",
          "|           +            +        |",
          "|           |            |        |",
          "|           |            |        |",
          "!                 ↑           ----+",
          "!                 N               |",
          "!           !  W ENTER E |   WC   |",
          "!           !     S      !        !",
          "+------------            ----------",
          "             !  Outside !          ",

    };

            string livingRoom = currentRoom == "Living Room" ? "🎅" : " ";
            string kitchen = currentRoom == "Kitchen" ? "🎅" : " ";
            string office = currentRoom == "Office" ? "🎅" : " ";

            foreach (string line in asciiMap)
            {
                Console.WriteLine(string.Format(line, livingRoom, kitchen, office));
            }
        }
    }
}
