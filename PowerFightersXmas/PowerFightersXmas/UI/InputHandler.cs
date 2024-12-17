using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.UI
{
    using PowerFightersXmas.Interface;

    public class InputHandler
    {
        private readonly IUserInput _userInput;
        private readonly IGameDisplay _gameDisplay;

        public InputHandler(IUserInput userInput, IGameDisplay gameDisplay)
        {
            _userInput = userInput ?? throw new ArgumentNullException(nameof(userInput));
            _gameDisplay= gameDisplay ?? throw new ArgumentNullException(nameof(gameDisplay));
        }

        public void EntryMenuInput(bool restartOnInvalidInput = true, Action? restartMenuAction = null)
        {
            bool isValidInput = false;
            while (!isValidInput)
            {
                var entryMenuInput = _userInput.GetInput();
                switch (entryMenuInput)
                {
                    case "1":
                        MainMenu.StartNewGame();
                        isValidInput = true;
                        break;
                    case "2":
                        MainMenu.LoadGame();
                        isValidInput = true;
                        break;
                    case "3":
                        MainMenu.Instructions();
                        isValidInput = true;
                        break;
                    case "4":
                        _gameDisplay.DisplayColourMessage("\n\tGoodbye! Evil Mage Marcus will come and haunt you forever!", ConsoleColor.Red);
                        isValidInput = true;
                        break;
                    default:
                        _gameDisplay.DisplayColourMessage("Invalid input. Please try again.", ConsoleColor.Red);
                        _userInput.WaitForKeyPress(); // Using mock instead of Console.ReadKey()

                        // Avoid infinite loop in tests
                        if (restartOnInvalidInput)
                        {
                            restartMenuAction?.Invoke(); // Use an action instead of MainMenu.EntryMenu
                        }
                        break;
                }
            }
        }
    }
}
