using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Interface
{
    public interface IGameDisplay
    {
        void PrintCenteredText(string text, ConsoleColor color);
        void DisplayColourMessage(string message, ConsoleColor color);
    }
}
