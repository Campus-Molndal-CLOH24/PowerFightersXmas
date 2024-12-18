using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.UI
{
    using PowerFightersXmas.Data;
    using PowerFightersXmas.Interface;
    using PowerFightersXmas.Logic;

    internal class MainMenu
    {
        private static IGameDisplay _gameDisplay = new GameDisplay.ConsoleGameDisplay();

        internal static void EntryPoint()
        {
            _gameDisplay.PrintCenteredText(" ______________________________________________________", ConsoleColor.Green);
            _gameDisplay.PrintCenteredText(" |                                                    |", ConsoleColor.Green);
            _gameDisplay.PrintCenteredText(" | Welcome to the Power Fighters Christmas adventure! |", ConsoleColor.Green);
            _gameDisplay.PrintCenteredText(" |                                                    |", ConsoleColor.Green);
            _gameDisplay.PrintCenteredText(" |       Press any key to get on with the show..      |", ConsoleColor.Green);
            _gameDisplay.PrintCenteredText(" |____________________________________________________|", ConsoleColor.Green);
            Console.ReadKey();
            EntryMenu();
        }

        internal static void EntryMenu(bool clearConsole = true)
        {
            if (clearConsole && Console.IsOutputRedirected == false) // Avoid Console.Clear() in tests
            {
                Console.Clear();
            }

            _gameDisplay.DisplayColourMessage("\n\tChoose an option:\n", ConsoleColor.Yellow);
            Console.WriteLine("\t1. Start a new game");
            Console.WriteLine("\t2. Load a game"); // TODO; This requires us to save game states in a file
            Console.WriteLine("\t3. Help / Instructions on how to play the game");
            _gameDisplay.DisplayColourMessage("\t4. Quit (Why would you ever want to do that..?)\n", ConsoleColor.Red);

            var inputHandler = new InputHandler(new ConsoleInput(), _gameDisplay);
            inputHandler.EntryMenuInput(restartOnInvalidInput: true, restartMenuAction: () => EntryMenu(false));
        }

        internal static void StartNewGame()
        {
            // Create a new player and the game state, which means the room, items, etc.
            var player = new Player("Jedi Bob"); // TODO; Implement player creation
            var gameState = new GameState(player);
            var commandHandler = new CommandHandler(gameState);

            // Console.Clear(); // TODO: Clear the console before starting the game - keep this commented out while in development
            Console.WriteLine("\n\t🎄 Welcome to the Power Fighters Christmas Adventure! 🎅");
            Console.WriteLine("\t Get ready to explore and have fun...\n");

            // Show the initial state of the game
            gameState.ShowState();

            var gameEngine = new GameEngine(gameState, commandHandler);
            gameEngine.Run();

            SaveOptions(gameState);
        }

        internal static void LoadGame()
        {
            var gameState = GameState.LoadGameState();
            if (gameState != null)
            {
                var commandHandler = new CommandHandler(gameState);
                var gameEngine = new GameEngine(gameState, commandHandler);

                Console.Clear();
                Console.WriteLine("🎄 Welcome back! Let's continue your adventure. 🎮\n");
                gameState.ShowState();

                gameEngine.Run();
            }
            else
            {
                Console.WriteLine("Returning to the main menu.");
                EntryMenu();
            }
        }

        internal static void Instructions()
        {
            // TODO; Basic instructions on how to play the game
            Console.WriteLine("Basic instructions will be here.");
            Console.WriteLine("To be implemented. Returning to the EntryMenu");
            EntryMenu();
        }

        // TODO; Then we also need to create menu handlers for navigation and interactions between- and in the various rooms and objects
        // Those might be implemented in the CommandHandler class?

        private static void SaveOptions(GameState gameState)
        {
            _gameDisplay.DisplayColourMessage("\n\tDo you want to save the game? (Y/N): ", ConsoleColor.Yellow);
            var saveGame = Console.ReadLine();
            if (saveGame?.ToUpper() == "Y")
            {
                gameState.SaveGameState();
            }
        }
    }
}