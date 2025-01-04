using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerFightersXmas.Logic;
using PowerFightersXmas.Interface;
using System;
using PowerFightersXmas.Data;

[TestClass]
public class CommandHandlerTests
{
    private Mock<IGameState> _mockGameState;
    private CommandHandler _commandHandler;

    [TestInitialize]
    public void Setup()
    {
        // Skapar en mock av IGameState och en instans av CommandHandler
        _mockGameState = new Mock<IGameState>();
        _commandHandler = new CommandHandler(_mockGameState.Object);
    }

    [TestMethod]
    public void ProcessCommand_InvalidCommand_PrintsErrorAndReturnsFalse()
    {
        // Arrange
        using (var consoleOutput = new System.IO.StringWriter())
        {
            Console.SetOut(consoleOutput);

            // Act
            var result = _commandHandler.ProcessCommand("invalid");

            // Assert
            Assert.IsFalse(result);
            StringAssert.Contains(consoleOutput.ToString(), "Invalid command.");
        }
    }

    [TestMethod]
    public void ProcessCommand_QuitCommand_ReturnsTrue()
    {
        // Act
        var result = _commandHandler.ProcessCommand("quit");

        // Assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ProcessCommand_GoCommand_CallsMovePlayer()
    {
        // Arrange
        _mockGameState.Setup(gs => gs.MovePlayer("north")).Returns("You walk north.");

        using (var consoleOutput = new System.IO.StringWriter())
        {
            Console.SetOut(consoleOutput);

            // Act
            _commandHandler.ProcessCommand("go north");

            // Assert
            _mockGameState.Verify(gs => gs.MovePlayer("north"), Times.Once);
            StringAssert.Contains(consoleOutput.ToString(), "You walk north.");
        }
    }

    [TestMethod]
    public void ProcessCommand_TakeCommand_AddsItemToInventory()
    {
        // Arrange
        var testItem = new Item { Name = "hammer" };
        _mockGameState.Setup(gs => gs.GetCurrentRoomItems()).Returns(new[] { testItem });
        _mockGameState.Setup(gs => gs.AddItemToPlayerInventory(testItem)).Returns("You have picked up hammer.");

        using (var consoleOutput = new System.IO.StringWriter())
        {
            Console.SetOut(consoleOutput);

            // Act
            _commandHandler.ProcessCommand("take hammer");

            // Assert
            _mockGameState.Verify(gs => gs.AddItemToPlayerInventory(testItem), Times.Once);
            StringAssert.Contains(consoleOutput.ToString(), "You have picked up hammer.");
        }
    }

    [TestMethod]
    public void ProcessCommand_TakeCommand_ItemNotFound_PrintsError()
    {
        // Arrange
        _mockGameState.Setup(gs => gs.GetCurrentRoomItems()).Returns(Array.Empty<Item>());

        using (var consoleOutput = new System.IO.StringWriter())
        {
            Console.SetOut(consoleOutput);

            // Act
            _commandHandler.ProcessCommand("take sword");

            // Assert
            StringAssert.Contains(consoleOutput.ToString(), "There is no 'sword' here.");
        }
    }

    [TestMethod]
    public void ProcessCommand_LookCommand_CallsShowState()
    {
        // Act
        _commandHandler.ProcessCommand("look");

        // Assert
        _mockGameState.Verify(gs => gs.ShowState(), Times.Once);
    }
}
