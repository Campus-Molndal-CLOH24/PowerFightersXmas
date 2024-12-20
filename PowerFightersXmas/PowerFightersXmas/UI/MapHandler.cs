using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.UI
{
    internal class MapHandler
    {
        internal void DisplayMap(string currentRoom)
        {
            string[] asciiMap = {
                "+------------------------------------------+",
                "| Living Room    | Kitchen  | Office       |",
                "|      {0}      |    {1}  |      {2}    |",
                "|                |          |              |          N",
                "|                +----------+              |          ↑",
                "| Corridor       | Entrance |   Door -->   |     W <-   -> E",
                "|      {3}       |    {4}   |              |          ↓",
                "+----------------+----------+--------------+          S",
                "| Pool           |",
                "|      {5}       |",
                "+----------------+"
            };

            // Mark the current room with Santa
            string livingRoom = currentRoom == "Living Room" ? "🎅" : "  ";
            string kitchen = currentRoom == "Kitchen" ? "🎅" : "  ";
            string office = currentRoom == "Office" ? "🎅" : "  ";
            string corridor = currentRoom == "Corridor" ? "🎅" : " ";
            string entrance = currentRoom == "Entrance" ? "🎅" : " ";
            string outside = currentRoom == "Outside" ? "🎅" : " ";

            foreach (string line in asciiMap)
            {
                Console.WriteLine(string.Format(line, livingRoom, kitchen, office, corridor, entrance, outside));
            }
        }
    }
}
