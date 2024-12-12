using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.UI
{
    internal class MainMenu
    {
        internal static void EntryPoint()
        {
            Console.WriteLine("\t\t❄   Welcome to the Power Fighters Christmas adventure!   ❄");
            Console.WriteLine("\n\t\tPress any key to get on with the show..");
            Console.ReadKey();
        }

        internal static void EntryMenu()
        {
            // The inital menu will be giving you options on how to start the game

            Console.Clear();
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Starta nytt spel");
            Console.WriteLine("2. Ladda spel");
            Console.WriteLine("3. Avsluta");

            // Then we need to create menu handlers for navigation and interactions between- and in the various rooms and objects
        }
    }
}
