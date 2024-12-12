﻿using System;
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
            GameDisplay.DisplayColourMessage("\n\tChoose an option:\n", ConsoleColor.Yellow);
            Console.WriteLine("\t1. Start a new game");
            Console.WriteLine("\t2. Load a game"); // TODO; This requires us to save game states in a file
            Console.WriteLine("\t3. Help / Instructions on how to play the game");
            GameDisplay.DisplayColourMessage("\t4. Quit (Why would you ever want to do that..?)\n", ConsoleColor.Red);

            var entryMenuInput = Console.ReadLine();
            switch (entryMenuInput)
            {
                case "1":
                    StartNewGame();
                    break;
                case "2":
                    LoadGame();
                    break;
                case "3":
                    Instructions();
                    break;
                case "4":
                    GameDisplay.DisplayColourMessage("\n\tGoodbye! Evil Mage Marcus will come and haunt you forever!", ConsoleColor.Red);
                    break;
                default:
                    GameDisplay.DisplayColourMessage("Invalid input. Please try again.", ConsoleColor.Red);
                    EntryMenu();
                    break;
            }
        }

        internal static void StartNewGame()
        {
            // TODO; Start a new game
            Console.WriteLine("To be implemented. Returning to the EntryMenu, press any key:");
            Console.ReadKey();
            EntryMenu();
        }

        internal static void LoadGame()
        {
            // TODO; Load a game
            Console.WriteLine("To be implemented. Returning to the EntryMenu, press any key:");
            Console.ReadKey();
            EntryMenu();
        }

        internal static void Instructions()
        {
            // TODO; Basic instructions on how to play the game
            Console.WriteLine("Basic instructions will be here.");
            Console.WriteLine("To be implemented. Returning to the EntryMenu, press any key:");
            Console.ReadKey();
            EntryMenu();
        }
        
        // TODO; Then we also need to create menu handlers for navigation and interactions between- and in the various rooms and objects
    }
}