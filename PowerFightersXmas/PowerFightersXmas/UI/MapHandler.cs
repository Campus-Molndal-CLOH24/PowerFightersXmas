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
        "|     {0}         |    {1}     |      {2}       |",
        "|                |          |              |",
        "|                |    N     |     N        |",
        "|                +----------+  \"Choose\"    |",
        "|                |          |   Door -->   |",
        "|                |          |              |",
        "+----------------+----------+--------------+",
        "      N                 |    WC           |",
        "      ↑                 |                 |",
        " W <-   -> E            +-----------------+",
        "      S                     Outside       "
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
