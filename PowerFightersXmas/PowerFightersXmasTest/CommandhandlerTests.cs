using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerFightersXmas.Logic;
using PowerFightersXmas.Interface;
using System.Collections.Generic;
using PowerFightersXmas.Data;

namespace PowerFightersXmas.Test
{
    [TestClass]
    public class CommandHandlerTests
    {
        private Mock<IGameState>? _mockGameState;
        private CommandHandler? _commandHandler;

        [TestInitialize]
        public void Setup()
        {
            _mockGameState = new Mock<IGameState>();
            _commandHandler = new CommandHandler(_mockGameState.Object);
        }

        [TestMethod]
        public void ProcessCommand_EmptyCommand_DisplaysError()
        {
            // Arrange
            var command = "";

            // Act
            var result = _commandHandler!.ProcessCommand(command);

            // Assert
            Assert.IsFalse(result); // Spelet ska fortsätta
            _mockGameState!.VerifyNoOtherCalls(); // Ingen interaktion med GameState
        }

        [TestMethod]
        public void ProcessCommand_InvalidCommand_DisplaysError()
        {
            // Arrange
            var command = "invalid";

            // Act
            var result = _commandHandler!.ProcessCommand(command);

            // Assert
            Assert.IsFalse(result); // Spelet ska fortsätta
            _mockGameState!.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void ProcessCommand_QuitCommand_ReturnsTrue()
        {
            // Arrange
            var command = "quit";
            var input = new StringReader("N"); // Simulerar att användaren matar in "N"
            Console.SetIn(input); // Byt ut Console.ReadLine() till att läsa från StringReader

            // Act
            var result = _commandHandler!.ProcessCommand(command);

            // Assert
            Assert.IsTrue(result); // Spelet ska sluta
        }

        [TestMethod]
        public void HandleGoCommand_ValidDirection_CallsMovePlayer()
        {
            // Arrange
            var command = "go north";
            _mockGameState!.Setup(gs => gs.MovePlayer("north")).Returns("You moved north.");

            // Act
            _commandHandler!.ProcessCommand(command);

            // Assert
            _mockGameState.Verify(gs => gs.MovePlayer("north"), Times.Once);
        }

        [TestMethod]
        public void HandleGoCommand_NoDirection_DisplaysError()
        {
            // Arrange
            var command = "go";

            // Act
            _commandHandler!.ProcessCommand(command);

            // Assert
            _mockGameState!.Verify(gs => gs.MovePlayer(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void HandleTakeCommand_InvalidItem_DisplaysError()
        {
            // Arrange
            var command = "take A Sword";
            _mockGameState!.Setup(gs => gs.GetCurrentRoomItems()).Returns(new List<Item>());

            // Act
            _commandHandler!.ProcessCommand(command);

            // Assert
            _mockGameState.Verify(gs => gs.GetCurrentRoomItems(), Times.Once);
            _mockGameState.Verify(gs => gs.AddItemToPlayerInventory(It.IsAny<Item>()), Times.Never);
        }

        [TestMethod]
        public void HandleTakeCommand_NoItemSpecified_DisplaysError()
        {
            // Arrange
            var command = "take";

            // Act
            _commandHandler!.ProcessCommand(command);

            // Assert
            _mockGameState!.Verify(gs => gs.GetCurrentRoomItems(), Times.Never);
            _mockGameState.Verify(gs => gs.AddItemToPlayerInventory(It.IsAny<Item>()), Times.Never);
        }
    }
}