using Microsoft.VisualStudio.TestTools.UnitTesting;
using PowerFightersXmas.Logic;
using PowerFightersXmas.Data;
using System.Collections.Generic;

[TestClass]
public class GameStateTests
{
    [TestMethod]
    public void AddItemToPlayerInventory_AddsItemWhenInventoryNotFull()
    {
        // Arrange
        var player = new Player("TestPlayer");
        var gameState = new GameState(player);
        var item = new Item { Name = "TestItem" };

        // Act
        var result = gameState.AddItemToPlayerInventory(item);

        // Assert
        Assert.AreEqual("You have picked up TestItem.", result);
        Assert.IsTrue(player.Inventory.Contains(item));
    }

    [TestMethod]
    public void MovePlayer_ValidDirection_UpdatesCurrentRoom()
    {
        // Arrange
        var player = new Player("TestPlayer");
        var room1 = new Room("Room1", "First Room");
        var room2 = new Room("Room2", "Second Room");
        room1.Exits["north"] = room2;
        var gameState = new GameState(player) { CurrentRoom = room1 };

        // Act
        var result = gameState.MovePlayer("north");

        // Assert
        Assert.AreEqual("You walk north and now find yourself in Room2.", result);
        Assert.AreEqual(room2, gameState.CurrentRoom);
    }
}
