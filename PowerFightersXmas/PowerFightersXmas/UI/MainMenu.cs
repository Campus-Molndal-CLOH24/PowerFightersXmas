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
            Console.Write("Enter your player name: ");
            string playerName = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("Player name cannot be empty.");
                return;
            }

            if (DatabaseManager.PlayerExists(playerName))
            {
                Console.WriteLine("Player already exists. Loading game...");
                var gameState = GameState.LoadGameState(playerName);
                if (gameState != null) PlayGame(gameState);
            }
            else
            {
                var player = new Player(playerName);
                var gameState = new GameState(player);
                PlayGame(gameState);
            }
        }

        internal static void LoadGame()
        {
            Console.Write("Enter your player name to load: ");
            string playerName = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(playerName))
            {
                Console.WriteLine("Player name cannot be empty.");
                return;
            }

            if (!DatabaseManager.PlayerExists(playerName))
            {
                Console.WriteLine("Player does not exist. Returning to main menu.");
                EntryMenu();
                return;
            }

            var gameState = GameState.LoadGameState(playerName);
            if (gameState != null)
            {
                PlayGame(gameState);
            }
            else
            {
                Console.WriteLine("Failed to load game. Returning to main menu.");
                EntryMenu();
            }
        }

        private static void PlayGame(GameState gameState)
        {
            var commandHandler = new CommandHandler(gameState);
            var gameEngine = new GameEngine(gameState, commandHandler);
            gameEngine.Run();

            Console.Write("Do you want to save the game? (Y/N): ");
            if (Console.ReadLine()?.ToUpper() == "Y")
            {
                gameState.SaveGameState();
                Console.WriteLine("Game saved!");
            }
        }

        internal static void GameWon()
        {
            _gameDisplay.DisplayColourMessage("\n\t🎉 You have saved Christmas and the World from the Evil Mage Marcus! 🎉", ConsoleColor.Yellow);
            _gameDisplay.DisplayColourMessage("\tGreat job! Rudolph walks up to you and happily licks your face..", ConsoleColor.Green);
            _gameDisplay.DisplayColourMessage("\tPress any key to return to the main menu..", ConsoleColor.Yellow);
            Console.ReadKey();
            EntryPoint();
        }

        internal static void Instructions(bool isInstructionsMenu)
        {
            _gameDisplay.DisplayColourMessage("\n\t ______________________________________________________", ConsoleColor.Green);
            _gameDisplay.DisplayColourMessage("\t |                                                    |", ConsoleColor.Green);
            _gameDisplay.DisplayColourMessage("\t |        Instructions on how to play the game        |", ConsoleColor.Green);
            _gameDisplay.DisplayColourMessage("\t |____________________________________________________|", ConsoleColor.Green);

            _gameDisplay.DisplayColourMessage("\n\tGo + direction (north, south, east, west, up, down) will help you move between rooms.", ConsoleColor.Cyan);
            _gameDisplay.DisplayColourMessage("\tLook will show you the current room and its contents.", ConsoleColor.Cyan);
            _gameDisplay.DisplayColourMessage("\tTake + item name will help you pick up items in the room.", ConsoleColor.Cyan);
            _gameDisplay.DisplayColourMessage("\tQuit will exit the game.", ConsoleColor.Cyan);
            _gameDisplay.DisplayColourMessage("\tType info while in the game to display these instructions again.", ConsoleColor.Cyan);

            _gameDisplay.DisplayColourMessage("\n\tPress any key to continue..", ConsoleColor.Yellow);
            Console.ReadKey();

            // Try which path the user visited the instructions menu from and return to that path
            if (isInstructionsMenu)
            {
                EntryMenu();
            }
            else
            {
                _gameDisplay.DisplayColourMessage("\n\tReturning to the game.", ConsoleColor.Yellow);
            }
        }

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