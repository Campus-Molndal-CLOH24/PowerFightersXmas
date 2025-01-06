using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerFightersXmas.Data;
using PowerFightersXmas.Interface;
using PowerFightersXmas.Logic;
using System.Collections.Generic;
using System.IO;

namespace PowerFightersXmas.Test.LogicTests
{
    [TestClass]
    public class CommandHandlerTests
    {
        private Mock<IGameState> _mockGameState;
        private CommandHandler _commandHandler;

        [TestInitialize]
        public void Setup()
        {
            _mockGameState = new Mock<IGameState>();
            _commandHandler = new CommandHandler(_mockGameState.Object);
        }

        [TestMethod]
        public void ProcessCommand_ShouldReturnFalseForInvalidCommand()
        {
            // Arrange
            string invalidCommand = "invalid";

            // Act
            var result = _commandHandler.ProcessCommand(invalidCommand);

            // Assert
            Assert.IsFalse(result);
            _mockGameState.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void ProcessCommand_ShouldCallHandleGoCommandForGoAction()
        {
            // Arrange
            string command = "go north";

            _mockGameState.Setup(gs => gs.MovePlayer("north")).Returns("Moved north.");

            // Act
            var result = _commandHandler.ProcessCommand(command);

            // Assert
            Assert.IsFalse(result);
            _mockGameState.Verify(gs => gs.MovePlayer("north"), Times.Once);
        }

        [TestMethod]
        public void HandleTakeCommand_ShouldReturnMessage_WhenItemDoesNotExist()
        {
            // Arrange
            _mockGameState.Setup(gs => gs.GetCurrentRoomItems()).Returns(new List<Item>());

            // Act
            _commandHandler.ProcessCommand("take hammer");

            // Assert
            _mockGameState.Verify(gs => gs.AddItemToPlayerInventory(It.IsAny<Item>()), Times.Never);
        }

        [TestMethod]
        public void HandleDecorateCommand_ShouldSetMessage_WhenNotInLivingRoom()
        {
            // Arrange
            var mockRoom = new Room("Kitchen", "A kitchen");
            _mockGameState.Setup(gs => gs.CurrentRoom).Returns(mockRoom);
            _mockGameState.SetupProperty(gs => gs.DecorateMessage); // Se till att DecorateMessage kan skrivas till

            // Act
            _commandHandler.ProcessCommand("decorate");

            // Assert
            Assert.AreEqual("\n❌ You must be in the Living Room to decorate the Christmas Tree.",
                _mockGameState.Object.DecorateMessage,
                "Expected message was not set when player is not in the Living Room.");
        }
    }
}
