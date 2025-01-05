using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerFightersXmas.Data;
using PowerFightersXmas.Interface;
using PowerFightersXmas.Logic;
using System.Collections.Generic;

namespace PowerFightersXmas.Test
{
    [TestClass]
    public class GameStateTests
    {
        private GameState? _gameState;
        private Room? _entrance;
        private Room? _kitchen;
        private Item? _axe;

        [TestInitialize]
        public void Setup()
        {
            DatabaseManager.InitializeDatabase();
            // Setup test rooms and items
            _entrance = new Room("Entrance", "The starting room.");
            _kitchen = new Room("Kitchen", "A messy kitchen.");
            _axe = new Item("An Axe", "A sharp axe.", 1);

            _entrance.Exits.Add("north", _kitchen);
            _entrance.Items.Add(_axe);

            // Setup test player and game state
            var player = new Player("TestPlayer");
            _gameState = new GameState(player) { CurrentRoom = _entrance };
        }

        [TestMethod]
        public void GetCurrentRoomItems_ReturnsCorrectItems()
        {
            // Act
            var items = _gameState?.GetCurrentRoomItems();

            // Assert
            Assert.IsNotNull(items);
            Assert.AreEqual(1, items.Count);
            Assert.AreEqual("An Axe", items[0].Name);
        }

        [TestMethod]
        public void AddItemToPlayerInventory_AddsItemWhenNotFull()
        {
            // Act
            var result = _gameState?.AddItemToPlayerInventory(_axe!);

            // Assert
            Assert.IsNotNull(_gameState); // MSTEST0026: Additional null check
            Assert.AreEqual("You have picked up An Axe.", result);
            Assert.IsNotNull(_gameState?.Player); // MSTEST0026: Additional null check
            Assert.IsTrue(_gameState.Player.Inventory.Contains(_axe!)); // CS8604: Ensure _axe is not null
            Assert.IsFalse(_entrance?.Items.Contains(_axe!)); // Item removed from room
        }

        [TestMethod]
        public void AddItemToPlayerInventory_FailsWhenInventoryFull()
        {
            // Arrange
            for (int i = 0; i < 5; i++)
            {
                _gameState?.Player.Inventory.Add(new Item($"Item{i}", $"Description{i}", 1));
            }

            // Act
            var result = _gameState?.AddItemToPlayerInventory(_axe!);

            // Assert
            Assert.AreEqual("Your inventory is full!", result);
            Assert.IsFalse(_gameState?.Player.Inventory.Contains(_axe!));
        }

        [TestMethod]
        public void MovePlayer_ValidDirection_ChangesCurrentRoom()
        {
            // Act
            var result = _gameState?.MovePlayer("north");

            // Assert
            Assert.AreEqual("You walk north and now find yourself in Kitchen.", result);
            Assert.AreEqual("Kitchen", _gameState?.CurrentRoom.Name);
        }

        [TestMethod]
        public void MovePlayer_InvalidDirection_ReturnsError()
        {
            // Act
            var result = _gameState?.MovePlayer("south");

            // Assert
            Assert.AreEqual("You can't go there.", result);
            Assert.AreEqual("Entrance", _gameState?.CurrentRoom.Name); // Still in the same room
        }

        [TestMethod]
        public void AddAdjacentRoom_CreatesConnectionBetweenRooms()
        {
            // Arrange
            var basement = new Room("Basement", "A dark basement.");

            // Act
            _gameState?.AddAdjacentRoom(_entrance!, "down", basement);

            // Assert
            Assert.AreEqual(basement, _entrance!.Exits["down"]);
        }

        [TestMethod]
        public void SaveGameState_CallsDatabaseManagerWithCorrectParameters()
        {
            // Arrange
            var mockDatabaseManager = new Mock<IDatabaseManager>();
            DatabaseManager.Instance = mockDatabaseManager.Object;

            // Ensure _gameState and its properties are not null
            Assert.IsNotNull(_gameState, "GameState should not be null.");
            Assert.IsNotNull(_gameState.Player, "Player should not be null.");
            Assert.IsNotNull(_gameState.CurrentRoom, "CurrentRoom should not be null.");

            var axe = new Item("An Axe", "A sharp axe.", 1);
            _gameState.Player.Inventory.Add(axe);

            // Act
            _gameState.SaveGameState();

            // Assert
            mockDatabaseManager.Verify(dm =>
                dm.SavePlayer(
                    It.Is<string>(name => name == _gameState.Player.Name),
                    It.Is<string>(inventoryJson => inventoryJson.Contains("An Axe")),
                    It.Is<string>(currentRoom => currentRoom == _gameState.CurrentRoom.Name)
                ),
                Times.Once
            );
        }

        [TestMethod]
        public void LoadGameState_ReturnsCorrectState()
        {
            // Arrange
            var mockDatabaseManager = new Mock<IDatabaseManager>();
            mockDatabaseManager
                .Setup(dm => dm.LoadPlayer("TestPlayer"))
                .Returns(("[{\"Name\":\"An Axe\"}]", "Entrance")); // Simulerad data

            DatabaseManager.Instance = mockDatabaseManager.Object;

            // Act
            var gameState = GameState.LoadGameState("TestPlayer");

            // Assert
            Assert.IsNotNull(gameState, "GameState should not be null.");
            Assert.AreEqual("Entrance", gameState!.CurrentRoom.Name);
            Assert.IsTrue(gameState.Player.Inventory.Any(i => i.Name == "An Axe"));
        }
    }
}
