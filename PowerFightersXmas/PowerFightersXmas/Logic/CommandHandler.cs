﻿using PowerFightersXmas.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Logic
{
    public class CommandHandler : ICommandHandler
    {
        private readonly IGameState _gameState;

        public CommandHandler(IGameState gameState)
        {
            _gameState = gameState;
        }

        public bool ProcessCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
            {
                Console.WriteLine("You need to enter a command!");
                return false; // Fortsätt spelet
            }

            var parts = command.ToLower().Split(' ');
            var action = parts[0];

            switch (action)
            {
                case "go":
                    HandleGoCommand(parts);
                    break;

                case "look":
                    _gameState.ShowState();
                    break;

                case "take":
                    HandleTakeCommand(parts);
                    break;

                case "quit":
                    Console.WriteLine("🎅 Quitting the game...");
                    return true; // Signalera att spelet ska avslutas

                default:
                    Console.WriteLine("Invalid command. Available commands are: 'go', 'look', 'take', 'quit'.");
                    break;
            }

            return false; // Fortsätt spelet
        }

        private void HandleGoCommand(string[] parts)
        {
            if (parts.Length > 1)
                Console.WriteLine(_gameState.MovePlayer(parts[1]));
            else
                Console.WriteLine("Please specify a direction, e.g., 'go north'.");
        }

        private void HandleTakeCommand(string[] parts)
        {
            if (parts.Length > 1)
            {
                var itemName = string.Join(" ", parts[1..]);
                var item = _gameState.GetCurrentRoomItems()
                    .FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

                if (item != null)
                    Console.WriteLine(_gameState.AddItemToPlayerInventory(item));
                else
                    Console.WriteLine($"There is no '{itemName}' here.");
            }
            else
            {
                Console.WriteLine("Please specify the item you want to take, e.g., 'take hammer'.");
            }
        }
    }
}
