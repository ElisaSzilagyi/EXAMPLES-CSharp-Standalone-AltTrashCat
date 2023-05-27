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
        private MainMenuPage mainMenuPage;
        public void AssertBuyItem(int index)
        {
            var tabName = "Item";
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            var initialNumber = storePage.GetNumberOf(index);
            storePage.Buy(tabName, index);
            Assert.True(storePage.GetNumberOf(index) - initialNumber == 1);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, index));
        }
        // private StartPage startPage;
        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver();
            storePage = new StorePage(altDriver);
            storePage.Load();
        }
        [Test]
        public void TestStoreIsDisplayed()
        {
            Assert.True(storePage.IsDisplayed());
        }

        [Test]
        public void TestBuyRaccoon()
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
        [Ignore("SetComponentProperty is not working in this case.")]
        public void TestChangeMoneyValue()
        {
            string value = "13";
            string currentMoneyValueAfterChange = storePage.ChangeAmountOfMoney(value);
            Assert.AreEqual("0", currentMoneyValueAfterChange);
            //we are not able to change this property with setComponentProperty
        }

        [Test]
        public void TestDifferentColorsOnPressing()
        {
            Assert.True(storePage.DifferentStateWhenPressingBtn());
        }

        [Test]
        public void TestNewMagnetName()
        {
            string value = "magneeeeeeet";
            string tabName = "Items";
            string newName = storePage.ChangeItemName(tabName, 0, value);
            Assert.AreEqual(value, newName);
        }

        [Test]
        public void TestBuyMagnet()
        {
            var tabName = "Item";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();

            storePage.GoToTab(tabName);
            var currentNumOfMagnets = storePage.GetNumberOf(0);

            storePage.Buy(tabName, 0); //buy a magnet
            Assert.True(storePage.GetNumberOf(0) - currentNumOfMagnets == 1);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 0));
        }
        [Test]
        public void TestBuyx2()
        {
            //todo make it independent

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
        public void TestBuyInvincible()
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
        public void TestBuyLife()
        {
            //todo make it independent

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
        public void TestBuySafetyHat()
        {
            //todo make it independent
            var tabName = "Accesories";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 0);
            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 0));
        }
        [Test]
        public void TestBuyPartyHat()
        {
            //todo make it independent
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
        public void TestBuySmart()
        {
            mainMenuPage = new MainMenuPage(altDriver);
            mainMenuPage.LoadScene();
            mainMenuPage.DeleteData();
            storePage.Load();
            var tabName = "Accesories";
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            var moneyAmount = storePage.GetTotalAmountOfMoney();
            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 2);

            Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, 2));
        }
        [Test]

        public void TestBuyAllHats()
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
        public void TestBuyAllItems()
        {
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            for (int i = 0; i < 3; i++) AssertBuyItem(i);
        }
        [Test]
        public void TestCheckIfFishbonesAmountIsNextToIcon()
        {
            storePage.GetMoreMoney();
            Assert.True(storePage.FishbonesAmountByCoordinates());
        }
        [Test]
        public void TestAssertOwningTrashCatCharacter()
        {
            var tabName = "Character";
            storePage.GoToTab(tabName);
            Assert.True(storePage.AssertOwning(tabName, 0));
        }
        [Test]
        public void TestSetButtonInteractable()
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
        public void TestDifferentStatesOnPointingObject()
        {
            AltObject buyMagnetButton = storePage.GetObjectsBuyButton("Items", 0);
            Assert.True(storePage.CheckObjectStatesOnPointing(buyMagnetButton));
        }
        [Test]
        public void TestGetMoreMoney()
        {
            int currentAmountOfFishbones = storePage.GetTotalAmountOfMoney();
            storePage.GetMoreMoney();
            Assert.True(storePage.GetTotalAmountOfMoney() - currentAmountOfFishbones == 1000000);
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