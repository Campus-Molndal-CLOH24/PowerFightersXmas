using PowerFightersXmas.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Logic
{
    public class GameEngine : IGameEngine
    {
        private readonly IGameState _gameState;
        private readonly ICommandProcessor _commandProcessor;
        private bool _isRunning;

        public GameEngine(IGameState gameState, ICommandProcessor commandProcessor)
        {
            _gameState = gameState;
            _commandProcessor = commandProcessor;
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

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var shouldStop = _commandProcessor.ProcessCommand(input);

                    if (shouldStop)
                    {
                        StopGame(); // GameEngine själv hanterar stoppet
                    }
                }
            }
        }

        public void StopGame()
        {
            _isRunning = false;
            Console.WriteLine("🎅 The game has been stopped. Goodbye!");
        }
    }
}
