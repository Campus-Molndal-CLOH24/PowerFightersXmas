using System.Numerics;
using System.Reflection;

namespace PowerFightersXmas
{
    using PowerFightersXmas.Data;
    using System.Text;
    using UI;

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8; // Activate UTF-8 so we can use emojis and icons

            Console.WriteLine("Main started. Initializing database...");
            DatabaseManager.InitializeDatabase();
            Console.WriteLine("Database initialization completed.");
            DatabaseManager.PopulateRoomItems();
            MainMenu.EntryPoint();
        }
    }
}