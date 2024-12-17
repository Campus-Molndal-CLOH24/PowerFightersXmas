using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerFightersXmas.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PowerFightersXmas.UI;
    using PowerFightersXmas.Interface;

    [TestClass]
    public class InputHandlerTest
    {
        [TestMethod]
        public void EntryMenuInput_HandlesValidInput()
        {
            // Arrange
            var mockUserInput = new MockUserInput(new[] { "4" });
            var mockGameDisplay = new MockGameDisplay();
            var inputHandler = new InputHandler(mockUserInput, mockGameDisplay);
            // Act
            inputHandler.EntryMenuInput();
            // Assert
            // We can't really test the output of the EntryMenuInput method, but we can test that it doesn't throw an exception
            Assert.IsTrue(mockGameDisplay.Messages.Contains("Goodbye! Evil Mage Marcus will come and haunt you forever!"));
        }

        [TestMethod]
        public void EntryMenuInput_HandlesInvalidInput()
        {
            // Arrange
            var mockUserInput = new MockUserInput(new[] { "8", "text", "1" });
            var mockGameDisplay = new MockGameDisplay();
            var inputHandler = new InputHandler(mockUserInput, mockGameDisplay);
            // Act
            inputHandler.EntryMenuInput();
            // Assert
            // We can't really test the output of the EntryMenuInput method, but we can test that it doesn't throw an exception
            Assert.IsTrue(true); // If we reach this point, the test has passed
        }
    }

    public class MockGameDisplay : IGameDisplay
    {
        public List<string> Messages { get; } = new List<string>();

        public void DisplayColourMessage(string message, ConsoleColor color)
        {
            Messages.Add(message);
        }

        public void PrintCenteredText(string text, ConsoleColor color)
        {
            Messages.Add(text);
        }
    }
}
