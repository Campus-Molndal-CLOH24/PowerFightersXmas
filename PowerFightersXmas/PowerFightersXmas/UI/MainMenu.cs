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
            Console.Clear();

            string[] frame = {
        " _______________________________________________________________",
        " |                                                             |",
        " |                                                             |",
        " |                                                             |",
        " |_____________________________________________________________|"
    };

            string welcomeText = "🎄 Welcome to the Power Fighters Christmas adventure! 🎅";

            // Dynamic centering
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            // Center the frame in the console
            int frameStartX = (consoleWidth - frame[0].Length) / 2;
            int frameStartY = (consoleHeight - frame.Length) / 2;

            // Center the text within the frame
            int textStartX = frameStartX + 1 + (frame[1].Length - 4 - welcomeText.Length) / 2;
            int textStartY = frameStartY + 2;

            // Print the frame row by row
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i < frame.Length; i++)
            {
                Console.SetCursorPosition(frameStartX, frameStartY + i);
                Console.WriteLine(frame[i]);
                System.Threading.Thread.Sleep(100); // Delay between printing different rows in the frame
            }

            // Write the text with a typewriter style, one by one with a slight delay inbetween each letter
            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < welcomeText.Length; i++)
            {
                Console.SetCursorPosition(textStartX + i, textStartY);
                Console.Write(welcomeText[i]);
                System.Threading.Thread.Sleep(50);
            }

            System.Threading.Thread.Sleep(7000); // Waits seven seconds before closing the entry sign
            Console.Clear();
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
            Console.WriteLine("\t2. Load a game");
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

            while (true)
            {
                // Show the current state of the game
                gameState.ShowState();

                // Run the game engine, which will handle the player input
                Console.Write("> "); // Input-prompt
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var shouldStop = commandHandler.ProcessCommand(input);

                    // Stop the game if the command handler returns true
                    if (shouldStop)
                    {
                        Console.WriteLine("🎅 The game has ended. Returning to main menu...");
                        break;
                    }
                }
            }
        }

        internal static void PromptSaveGame(IGameState gameState)
        {
            Console.Write("Do you want to save the game before quitting? (Y/N): ");
            string? response = Console.ReadLine();
            if (response?.ToUpper() == "Y")
            {
                if (gameState is GameState concreteGameState)
                {
                    concreteGameState.SaveGameState(); // Anropa SaveGameState på GameState-objektet
                    Console.WriteLine("Game saved successfully!");
                }
                else
                {
                    Console.WriteLine("Error: Unable to save the game. Invalid game state.");
                }
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
    }
}