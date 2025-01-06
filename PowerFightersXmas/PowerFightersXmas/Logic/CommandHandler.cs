using PowerFightersXmas.Data;
using PowerFightersXmas.Interface;
using PowerFightersXmas.UI;
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


                case "decorate":
                    HandleDecorateCommand();
                    break;

                case "take":
                    HandleTakeCommand(parts);
                    break;

                case "quit":
                    MainMenu.PromptSaveGame(_gameState);
                    Console.WriteLine("🎅 Quitting the game...");
                    return true; // Signal to GameEngine that the game should stop

                case "info":
                    MainMenu.Instructions(false);
                    break;

                default:
                    Console.WriteLine("Invalid command. Available commands are: 'go', 'look', 'take', 'quit', 'info'.");
                    break;
            }

            return false; // Continue the game
        }

        private void HandleGoCommand(string[] parts)
        {
            if (parts.Length > 1)
                Console.WriteLine(_gameState.MovePlayer(parts[1]));
            else
                Console.WriteLine("Please specify a direction, e.g., 'go north'.");
        }

        
        private void HandleDecorateCommand()
        {
            // Kontrollera att spelaren är i rätt rum
            if (_gameState.CurrentRoom.Name != "Living Room")
            {
                _gameState.DecorateMessage = "\n❌ You must be in the Living Room to decorate the Christmas Tree.";
                return;
            }

            // Lista över föremål som behövs för att dekorera granen
            var requiredItems = new List<string>
    {
        "Glitter",
        "Christmas baubles",
        "Christmas tree lights",
        "Christmas tree star"
    };

            // Kontrollera om spelaren har alla föremål
            bool hasAllItems = requiredItems.All(item => _gameState.Player.Inventory.Any(i => i.Name.Equals(item, StringComparison.OrdinalIgnoreCase)));

            if (hasAllItems)
            {
                Console.Clear();
                MainMenu.GameWon(); // Anropa GameWon istället för att visa meddelandet direkt
            }
            else
            {
                _gameState.DecorateMessage = "\n❌ You need all the ornaments to decorate the tree." +
                                             "\nKeep exploring to find the missing items.";
            }
        }

        private void HandleTakeCommand(string[] parts)
        {
            if (parts.Length > 1)
            {
                var itemName = string.Join(" ", parts[1..]);
                var item = _gameState.GetCurrentRoomItems()
                    .FirstOrDefault(i => i.Name.Equals(itemName, StringComparison.OrdinalIgnoreCase));

                if (item != null)
                {
                    // Lägg till itemet i spelarens inventory
                    Console.WriteLine(_gameState.AddItemToPlayerInventory(item));

                    // Ta bort itemet från rummet (runtime)
                    _gameState.CurrentRoom.Items.Remove(item);

                    // Spara plockat objekt i databasen
                    DatabaseManager.AddPickedUpItem(_gameState.Player.Name, item.Name);

                    Console.WriteLine($"You have taken the {item.Name}.");
                }
                else
                {
                    Console.WriteLine($"There is no '{itemName}' here.");
                }
            }
            else
            {
                Console.WriteLine("Please specify the item you want to take, e.g., 'take hammer'.");
            }
        }
    }
}
