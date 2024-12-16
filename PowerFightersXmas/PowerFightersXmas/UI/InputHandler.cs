using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.UI
{
    internal class InputHandler
    {
        public static void EntryMenyInput()
        {
            var entryMenuInput = Console.ReadLine();
            switch (entryMenuInput)
            {
                case "1":
                    MainMenu.StartNewGame();
                    break;
                case "2":
                    MainMenu.LoadGame();
                    break;
                case "3":
                    MainMenu.Instructions();
                    break;
                case "4":
                    GameDisplay.DisplayColourMessage("\n\tGoodbye! Evil Mage Marcus will come and haunt you forever!", ConsoleColor.Red);
                    break;
                default:
                    GameDisplay.DisplayColourMessage("Invalid input. Please try again.", ConsoleColor.Red);
                    Console.WriteLine("Press any key to return to the menu.");
                    Console.ReadKey();
                    MainMenu.EntryMenu(false);
                    break;
            }
        }
    }
}
