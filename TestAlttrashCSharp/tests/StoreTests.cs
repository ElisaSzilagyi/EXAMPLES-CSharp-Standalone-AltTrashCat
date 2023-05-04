using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using System;
using System.Threading;
using NUnit.Framework;

namespace alttrashcat_tests_csharp.tests
{
    public class StorePageTests
    {
        private AltDriver altDriver;
        private StorePage storePage;
        MainMenuPage mainMenuPage;
        // private StartPage startPage;
        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver();
            storePage = new StorePage(altDriver);
            storePage.Load();
        }
        [Test]
        public void StoreIsDisplayed()
        {
            Assert.True(storePage.IsDisplayed());
        }

        [Test]
        public void BuyRaccoon()
        {
            var tabName = "Character";
            // var characterName = "Raccoon";
            storePage.GoToTab("Item");
            storePage.GetMoreMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 1);
            Assert.True(storePage.AssertOwning(tabName, 1));
        }

        [Test]
        public void ChangeMoneyValue()
        {
            string value = "13";
            string currentMoneyValueAfterChange = storePage.ChangeAmountOfMoney(value);
            Assert.AreEqual("0", currentMoneyValueAfterChange);
            //we are not able to change this property with setComponentProperty
        }

        [Test]
        public void AssertDifferentColorsOnPressing()
        {
            Assert.True(storePage.DifferentStateWhenPressingBtn());
        }
        // [Test]
        // public void AssertSameColorWhenStopPressing(){
        //     // Assert.True(storePage.SameColorWhenStopPressing());
        //     Console.WriteLine(storePage.SameColorWhenStopPressing());
        // }
        [Test]
        public void PrintCurrentColor()
        {
            storePage.GoToTab("Character");
            Console.WriteLine("here!!!!!!!!!!!!!!!!!!!!!!!!!!!!1");
            Console.WriteLine(storePage.PrintCurrentColor());
            // storePage.PrintCurrentColor();
        }

        [Test]
        public void CheckNewMagnetName()
        {
            string value = "magneeeeeeet";
            string tabName = "Items";
            string newName = storePage.ChangeItemName(tabName, 0, value);
            Assert.AreEqual(value, newName);
            //we are not able to change this property with setComponentProperty
        }

        [Test]
        public void BuyMagnet()
        {
            var tabName = "Item";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 0);
            Assert.True(storePage.GetNumberOf(0) == 1);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 0));
        }
        [Test]
        public void Buyx2()
        {
            var tabName = "Item";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 1);
            Assert.True(storePage.GetNumberOf(1) == 1);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 1));
        }
        [Test]
        public void BuyInvincible()
        {
            var tabName = "Item";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 2);
            Assert.True(storePage.GetNumberOf(2) == 1);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 2));
        }
        [Test]
        public void BuyLife()
        {
            var tabName = "Item";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 3);
            Assert.True(storePage.GetNumberOf(3) == 1);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 3));
        }
        [Test]
        public void BuySafetyHat()
        {
            var tabName = "Accesories";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 0);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 0));
        }
        [Test]
        public void BuyPartyHat()
        {
            var tabName = "Accesories";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 1);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 1));
            // todo check that conserve is also taken
        }
        [Test]
        public void CheckIfFishbonesAmountIsNextToIcon()
        {
            storePage.GetMoreMoney();
            Assert.True(storePage.FishbonesAmountByCoordinates());
        }
        [Test]
        public void BuySmart()
        {
            var tabName = "Accesories";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 2);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 2));
        }
        [Test]
        public void AssertOwningTrashCatCharacter()
        {
            var tabName = "Character";
            storePage.GoToTab(tabName);
            Assert.True(storePage.AssertOwning(tabName, 0));
        }
        [Test]
        public void BuyAllHats()
        {
            var tabName = "Accesories";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.BuyAllFromTab(tabName);
            Assert.True(storePage.AssertOwning(tabName, 0));
            Assert.True(storePage.AssertOwning(tabName, 1));
            Assert.True(storePage.AssertOwning(tabName, 2));
            Assert.True(storePage.AssertOwning(tabName, 3));
            Assert.True(storePage.AssertOwning(tabName, 4));
        }
        [Test]
        public void BuyAllItems()
        {
            var tabName = "Item";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.BuyAllFromTab(tabName);
            Assert.True(storePage.GetNumberOf(0) == 1);
            Assert.True(storePage.GetNumberOf(1) == 1);
            Assert.True(storePage.GetNumberOf(2) == 1);
            Assert.True(storePage.GetNumberOf(3) == 1);
        }
        [Test]
        public void SetButtonInteractable()
        {
            storePage.GoToTab("Item");
            AltObject buyMagnetButton = storePage.GetObjectsBuyButton("Items", 0);
            storePage.SetButtonInteractable(buyMagnetButton);
            Assert.True(storePage.GetObjectProperty(buyMagnetButton, "interactable") == "true");

            int currentNumOfMagnets = storePage.GetNumberOf(0);
            storePage.Buy("Items", 0);
            int numberOfMagnets = storePage.GetNumberOf(0);
            Assert.True(numberOfMagnets - currentNumOfMagnets == 1);
        }
        [Test]
        public void DifferentStatesOnPointingObject(){
            AltObject buyMagnetButton = storePage.GetObjectsBuyButton("Items", 0);
            Assert.True(storePage.CheckObjectStatesOnPointing(buyMagnetButton));
        }
        [Test]
        public void TestGetMoreMoney(){
            int currentAmountOfFishbones = storePage.GetTotalAmountOfMoney();
            storePage.GetMoreMoney();
            Assert.True(storePage.GetTotalAmountOfMoney()-currentAmountOfFishbones == 1000000);
            //1000000
        }

        [TearDown]
        public void Dispose()
        {
            // mainMenuPage = new MainMenuPage(altDriver);
            // mainMenuPage.LoadScene();
            // mainMenuPage.DeleteData();
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}