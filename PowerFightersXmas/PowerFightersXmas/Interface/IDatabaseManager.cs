using PowerFightersXmas.Interface;
    
    namespace PowerFightersXmas.Interface
    {
        public interface IDatabaseManager
        {
            void SavePlayer(string name, string inventoryJson, string currentRoom);
            (string inventoryJson, string currentRoom)? LoadPlayer(string playerName);
        }
    }