using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Logic
{
    internal class GameEngine
    {
        private readonly GameState _gameState;
        private bool _isRunning;

        public GameEngine(GameState gameState)
        {
            _gameState = gameState;
            _isRunning = true;
        }

        public void Run()
        {
            Console.WriteLine("🎄 Welcome to Santa's Code Adventure! 🎄");
            Console.WriteLine($"🎅 Hello, {_gameState.Player.Name}!");
            Console.WriteLine($"📍 You are starting in {_gameState.CurrentRoom.Name}.\n");

            // Main game loop
            while (_isRunning)
            {
                Console.Write("> "); // Player input prompt
                var input = Console.ReadLine();
                ProcessCommand(input);
            }
        }

        private void ProcessCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                Console.WriteLine("You need to enter a command!");
                return;
            }

            var parts = command.ToLower().Split(' ');
            var action = parts[0];

            switch (action)
            {
                case "go":
                    if (parts.Length > 1)
                        Console.WriteLine(_gameState.MovePlayer(parts[1]));
                    else
                        Console.WriteLine("Please specify a direction, e.g., 'go north'.");
                    break;

                case "look":
                    _gameState.ShowState();
                    break;

                case "take":
                    if (parts.Length > 1)
                    {
                        var itemName = string.Join(" ", parts[1..]); // Remaining words as item name
                        var item = _gameState.CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());

                        if (item != null)
                            Console.WriteLine(_gameState.AddItemToPlayerInventory(item));
                        else
                            Console.WriteLine($"There is no '{itemName}' here.");
                    }
                    else
                    {
                        Console.WriteLine("Please specify the item you want to take, e.g., 'take hammer'.");
                    }
                    break;

                case "quit":
                    StopGame();
                    break;

                default:
                    Console.WriteLine("Invalid command. Available commands are: 'go', 'look', 'take', 'quit'.");
                    break;
            }
        }

        private void StopGame()
        {
            _isRunning = false;
            Console.WriteLine("🎅 The game has ended. Thank you for playing!");
        }
    }
}
