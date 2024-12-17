using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Test
{
    using PowerFightersXmas.Interface;

    // This is a mock class that queues up a list of strings to simulate user input
    // We use queue to simulate several inputs in a row, it will dequeue the first input in the list and return it

    public class MockUserInput : IUserInput
    {
        private readonly Queue<string> _inputQueue;

        public MockUserInput(IEnumerable<string> inputQueue)
        {
            _inputQueue = new Queue<string>(inputQueue);
        }

        public string? GetInput()
        {
            return _inputQueue.Count > 0 ? _inputQueue.Dequeue() : "4"; // Finish with a valid input to avoid an infinite loop
        }

        public void WaitForKeyPress()
        {
            // Do nothing in the tests, to simulate Console.ReadKey()
        }
    }
}
