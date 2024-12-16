using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Logic
{
    using System;

    public class CommandHandler
    {
        private readonly GameEngine _gameEngine;

        public CommandHandler(GameEngine gameEngine)
        {
            _gameEngine = gameEngine;
        }

        public void HandleCommand(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("You need to enter a command!");
                return;
            }

            var parts = input.ToLower().Split(' ');
            var action = parts[0];

            switch (action)
            {
                case "go":
                    HandleGoCommand(parts);
                    break;

                case "look":
                    _gameEngine.ShowState();
                    break;

                case "take":
                    HandleTakeCommand(parts);
                    break;

                case "quit":
                    _gameEngine.StopGame();
                    break;

                default:
                    Console.WriteLine("Invalid command. Available commands are: 'go', 'look', 'take', 'quit'.");
                    break;
            }
        }

        private void HandleGoCommand(string[] parts)
        {
            if (parts.Length > 1)
            {
                var direction = parts[1];
                var result = _gameEngine.MovePlayer(direction);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Please specify a direction, e.g., 'go north'.");
            }
        }

        private void HandleTakeCommand(string[] parts)
        {
            if (parts.Length > 1)
            {
                var itemName = string.Join(" ", parts[1..]); // Combine all parts after "take"
                var result = _gameEngine.TakeItem(itemName);
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine("Please specify the item you want to take, e.g., 'take hammer'.");
            }
        }
    }

}
