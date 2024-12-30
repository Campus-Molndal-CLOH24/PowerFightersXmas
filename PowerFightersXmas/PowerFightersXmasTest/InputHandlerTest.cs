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
        [TestMethod, Timeout(5000)]
        public void EntryMenuInput_HandlesValidInput()
        {
            // Arrange
            var mockUserInput = new MockUserInput(new[] { "4" });
            var mockGameDisplay = new MockGameDisplay();
            var inputHandler = new InputHandler(mockUserInput, mockGameDisplay);
            
            // Act
            inputHandler.EntryMenuInput();
            
            // Assert
            // Assert.IsTrue(mockGameDisplay.Messages.Any(message => message.Contains("Goodbye! Evil Mage Marcus will come and haunt you forever!")));
            Assert.IsTrue(mockGameDisplay.Messages.Any(message => message.Contains("Goodbye! Evil Mage Marcus will come and haunt you forever!")),
        $"Expected message not found. Messages: {string.Join(", ", mockGameDisplay.Messages)}");
        }

        [TestMethod, Timeout(5000)]
        public void EntryMenuInput_HandlesInvalidInput()
        {
            // Arrange
            var mockUserInput = new MockUserInput(new[] { "8", "text", "4" }); // We need to provide at least one valid input to avoid an infinite loop
            var mockGameDisplay = new MockGameDisplay();
            var inputHandler = new InputHandler(mockUserInput, mockGameDisplay);
            // Act
            inputHandler.EntryMenuInput(restartOnInvalidInput: false);
            // Assert
            Assert.IsTrue(mockGameDisplay.Messages.Any(m => m.Contains("Invalid input. Please try again.")));
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
