using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.UI
{
    internal class GameDisplay
    {
        internal static string GetNonNullInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(input))
            {
                DisplayColourMessage("Input field cannot be empty. Please write something!", ConsoleColor.Red);
                Console.Write(prompt);
                input = Console.ReadLine(); // Be very mindful to TEST this properly!
            }
            return input;
        }

        internal static void DisplayColourMessage(string message, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        internal static void PrintCenteredText(string text, ConsoleColor colour)
        {
            Console.ForegroundColor = colour;
            // Checks the width of the console window and centers the text accordingly
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
