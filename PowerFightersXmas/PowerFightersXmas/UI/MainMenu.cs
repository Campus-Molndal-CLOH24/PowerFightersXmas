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
            GameDisplay.PrintCenteredText(" ______________________________________________________", ConsoleColor.Green);
            GameDisplay.PrintCenteredText(" |                                                    |", ConsoleColor.Green);
            GameDisplay.PrintCenteredText(" | Welcome to the Power Fighters Christmas adventure! |", ConsoleColor.Green);
            GameDisplay.PrintCenteredText(" |                                                    |", ConsoleColor.Green);
            GameDisplay.PrintCenteredText(" |       Press any key to get on with the show..      |", ConsoleColor.Green);
            GameDisplay.PrintCenteredText(" |____________________________________________________|", ConsoleColor.Green);
            Console.ReadKey();
            EntryMenu();
        }

        internal static void EntryMenu()
        {
            Console.Clear();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Start a new game");
            Console.WriteLine("2. Load a game"); // TODO; This requires us to save game states in a file
            Console.WriteLine("3. Help / Instructions on how to play the game");
            Console.WriteLine("4. Quit (Why would you ever want to do that..? Evil Mage Marcus will come and haunt you forever!)");

            // Then we need to create menu handlers for navigation and interactions between- and in the various rooms and objects
        }
    }
}
