using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.UI
{
    using PowerFightersXmas.Interface;

    internal class GameDisplay
    {
        internal class ConsoleGameDisplay : IGameDisplay
        {
            public void PrintCenteredText(string text, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
                Console.WriteLine(text);
                Console.ResetColor();
            }
            public void DisplayColourMessage(string message, ConsoleColor color)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }

        internal static string GetNonNullInput(string prompt, IGameDisplay gameDisplay)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                gameDisplay.DisplayColourMessage("Input field cannot be empty. Please write something!", ConsoleColor.Red);
                Console.Write(prompt);
                input = Console.ReadLine();
            }
            return input;
        }
    }
}