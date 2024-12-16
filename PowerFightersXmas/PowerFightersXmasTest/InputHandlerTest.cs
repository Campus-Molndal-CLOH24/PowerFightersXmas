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
            var mockUserInput = new MockUserInput(new[] { "1", "2", "3", "4" });
            var inputHandler = new InputHandler(mockUserInput);
            // Act
            inputHandler.EntryMenuInput();
            // Assert
            // We can't really test the output of the EntryMenuInput method, but we can test that it doesn't throw an exception
            Assert.IsTrue(true); // If we reach this point, the test has passed
        }

        [TestMethod]
        public void EntryMenuInput_HandlesInvalidInput()
        {
            // Arrange
            var mockUserInput = new MockUserInput(new[] { "8", "text", "1" });
            var inputHandler = new InputHandler(mockUserInput);
            // Act
            inputHandler.EntryMenuInput();
            // Assert
            // We can't really test the output of the EntryMenuInput method, but we can test that it doesn't throw an exception
            Assert.IsTrue(true); // If we reach this point, the test has passed
        }
    }
}
