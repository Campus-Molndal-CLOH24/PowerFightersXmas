using PowerFightersXmas.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Interface
{
    public interface IGameState
    {
        Player Player { get; }
        Room CurrentRoom { get; }
        string DecorateMessage { get; set; }

        string MovePlayer(string direction);
        void ShowState();
        List<Item> GetCurrentRoomItems();
        string AddItemToPlayerInventory(Item item);
    }
}
