using PowerFightersXmas.Data;
using static PowerFightersXmas.Data.Item;

namespace ItemTests
{
    [TestClass]
    public class ItemTests
    {
        [TestMethod]
        public void DisplayInfoTest()
        {
            // Arrange
            var item = new Item("An Axe", "A sharp axe that can be used for heavy duties", 1);
            string expected = $"Name: An Axe{Environment.NewLine}Description: A sharp axe that can be used for heavy duties{Environment.NewLine}Quantity: 1";
            //"I got an error when I used /n, and ChatGPT suggested Environment.NewLine,
            //system-defined string that represents the correct line break for the platform you're running on."

            //To test methods that write to the console, you can use a StringWriter to capture console output.

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                item.DisplayInfo();

                string actual = sw.ToString().Trim();

                // Assert
                Assert.AreEqual(expected, actual);
            }
        }


    }
}