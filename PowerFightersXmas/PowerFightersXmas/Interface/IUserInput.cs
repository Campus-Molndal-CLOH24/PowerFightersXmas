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
    }

    public class ConsoleInput : IUserInput
    {
        public string? GetInput() => Console.ReadLine();
    }
}
