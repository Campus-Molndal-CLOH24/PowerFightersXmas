using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.UI
{
    using PowerFightersXmas.Interface;

    internal class InputHandler
    {
        private readonly IUserInput _userInput;

        public InputHandler(IUserInput userInput)
        {
            _userInput = userInput ?? throw new ArgumentNullException(nameof(userInput));
        }

        public void EntryMenyInput()
        {
            bool isValidInput = false;
            while (!isValidInput)
            {
                var entryMenuInput = _userInput.GetInput();
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
}
