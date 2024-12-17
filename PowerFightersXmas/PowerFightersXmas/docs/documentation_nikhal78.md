#### Dokumementation av julspel

### Niklas Häll - CLO24 - Power Fighters julen 2024

## Steg för steg-testning av menyn

1. Installerar MS Test genom kommandon:
- dotnet add package MSTest.TestFramework
- dotnet add package MSTest.TestAdapter

2. Högerklickar solutionen i Visual Studio 2022 och väljer "add > new project". Väljer MSTest och skapar ett nytt testprojekt i vanliga testfoldern.

3. Högerklickar testprojektet, väljer "add > project reference" och lägger till en referens i huvudprojektet

4. Dags att skapa en mock (fejkad testdata). Fick omedelbart ett problem:
![versionofmstest](image.png)
- Vi har troligtvis installerat en äldre version initialt, så property reflekterar det, sedan har vi uppgraderat till en nyare version så jag ändrar bara versionsnamnet i Properties!

5. 

### Problematik som uppstått i samband med testning

1. Jag hade kodat en switch-meny som Amir skulle testa. Menyn hade färger för att bli lite roligare och tydligare:
```cs
var entryMenuInput = _userInput.GetInput();
switch (entryMenuInput)
{
    case "1":
        MainMenu.StartNewGame();
        break;
    case "2":
        MainMenu.LoadGame();
        break;
    case "3":
        MainMenu.Instructions();
        break;
    case "4":
        GameDisplay.DisplayColourMessage("\n\tGoodbye! Evil Mage Marcus will come and haunt you forever!", ConsoleColor.Red);
        break;
    default:
        GameDisplay.DisplayColourMessage("Invalid input. Please try again.", ConsoleColor.Red);
        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
        MainMenu.EntryMenu(false);
        break;
}
```
Tanken var att det skulle se lite roligare och tydligare ut, så här ungefär:
![menu_v1](image-1.png)
Det blir ett problem med testningen då bara. Vi har en metod i menyn som testas, Amir pekade på att det var svårt för honom att testa och jag insåg att det inte är optimalt med en metod som skall returnera ett värde inuti en meny som redan testas! Lösningen på detta var att skapa ett interface för utmatningen, som jag gjorde med IUserInput för inmatningen. Hela testningen blir lättare med interfacen på plats, vi får en naturlig separation av concerns.