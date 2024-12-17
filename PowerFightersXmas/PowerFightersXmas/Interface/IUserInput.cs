using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Interface
{
    public interface IUserInput
    {
        string? GetInput();
        void WaitForKeyPress(); // Method to mock Console.ReadKey() during testing
    }

    public class ConsoleInput : IUserInput
    {
        public string? GetInput() => Console.ReadLine();

        public void WaitForKeyPress()
        {
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
    }
}