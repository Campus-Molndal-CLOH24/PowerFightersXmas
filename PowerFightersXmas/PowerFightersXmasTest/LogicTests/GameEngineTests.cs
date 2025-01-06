using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PowerFightersXmas.Logic;
using PowerFightersXmas.Interface;
using System.IO;

[TestClass]
public class GameEngineTests
{
    [TestMethod]
    public void StopGame_SetsIsRunningToFalseAndOutputsMessage()
    {
        // Arrange
        var mockGameState = new Mock<IGameState>();
        var mockCommandHandler = new Mock<ICommandHandler>();
        using (var output = new StringWriter())
        {
            Console.SetOut(output);

            var gameEngine = new GameEngine(mockGameState.Object, mockCommandHandler.Object);

            // Act
            gameEngine.StopGame();

            // Assert
            // Kontrollera att rätt meddelande skrivs ut
            StringAssert.Contains(output.ToString().Trim(), "The game has been stopped. Goodbye!");

            // Kontrollera att IsRunning är false
            Assert.IsFalse(gameEngine.IsRunning, "The game engine should stop running.");
        }
    }

    [TestMethod]
    public void Run_ProcessesCommandAndStopsOnQuit()
    {
        // Arrange
        var mockGameState = new Mock<IGameState>();
        var mockCommandHandler = new Mock<ICommandHandler>();
        mockCommandHandler.Setup(ch => ch.ProcessCommand(It.Is<string>(s => s == "quit")))
                          .Returns(true); // "quit" command should stop the game

        // Redirect Console input/output
        using (var input = new StringReader("quit\n"))
        using (var output = new StringWriter())
        {
            Console.SetIn(input);
            Console.SetOut(output);

            var gameEngine = new GameEngine(mockGameState.Object, mockCommandHandler.Object);

            // Act
            gameEngine.Run();

            // Assert
            mockCommandHandler.Verify(ch => ch.ProcessCommand("quit"), Times.Once);
            StringAssert.Contains(output.ToString(), "The game has been stopped. Goodbye!");
        }
    }
}
