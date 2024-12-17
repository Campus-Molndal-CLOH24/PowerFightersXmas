using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Logic
{
    using PowerFightersXmas.Interface;

    public class GameEngine : IGameEngine
    {
        private readonly IGameState _gameState;
        private readonly ICommandHandler _commandHandler;
        private bool _isRunning;

        public GameEngine(IGameState gameState, ICommandHandler commandHandler)
        {
            _gameState = gameState;
            _commandHandler = commandHandler;
            _isRunning = true;
        }

        public void Run()
        {  
            // Main game loop
            while (_isRunning)
            {
                Console.Write("> "); // Player input prompt
                var input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    var shouldStop = _commandHandler.ProcessCommand(input);

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
