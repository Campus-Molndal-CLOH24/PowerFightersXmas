using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Test
{
    using PowerFightersXmas.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TestSaveAndLoadPlayer
    {
        [TestMethod]
        public void TestingSaveAndLoadPlayer()
        {
            DatabaseManager.InitializeDatabase();
            string playerName = "TestPlayer";
            string inventoryJson = "[{\"Name\":\"Item1\"}]";
            string currentRoom = "Living Room";

            DatabaseManager.SavePlayer(playerName, inventoryJson, currentRoom);
            var playerData = DatabaseManager.LoadPlayer(playerName);

            Assert.IsNotNull(playerData);
            Assert.AreEqual(inventoryJson, playerData.Value.inventoryJson);
            Assert.AreEqual(currentRoom, playerData.Value.currentRoom);
        }
    }
}
