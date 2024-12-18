using PowerFightersXmas.Data;
namespace PlayerTests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void InitializePlayerWithNameAndEmptyInventory()
        {
            //Arrange
            string name = "Jedi Bob";
            //Act
            Player player = new Player(name);
            //Assert
            Assert.AreEqual(name, player.Name);
        }
        [TestMethod]
        public void AddItemToInventoryTest()
        {
            //Arrange
            string name = "Jedi Bob";
            Player player = new Player(name);
            Item item = new Item("An Axe", "A sharp axe that can be used for heavy duties", 1);
            //Act
            player.Inventory.Add(item);
            //Assert
            Assert.AreEqual(1, player.Inventory.Count);
        }
        [TestMethod]
        public void ShowInventory_WhenEmpty_ShouldPrintEmptyMessage()
        {
            // Arrange
            string name = "Jedi Bob";
            Player player = new Player(name);
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            player.ShowInventory();

            // Assert
            string output = stringWriter.ToString();
            Assert.IsTrue(output.Contains("Inventory is empty."));
        }
        [TestMethod]
        public void ShowInventory_WhenAddingItem_ShouldPrintItem()
        {
            // Arrange
            string name = "Jedi Bob";
            Player player = new Player(name);
            Item item = new Item("An Axe", "A sharp axe that can be used for heavy duties");

            // Lägg till StringWriter för att fånga konsolutmatningen
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            player.Inventory.Add(item);
            player.ShowInventory();

            // Assert
            string output = stringWriter.ToString();
            Assert.IsTrue(output.Contains("An Axe: A sharp axe that can be used for heavy duties"));
            Assert.AreEqual(1, player.Inventory.Count);
        }


    }
}