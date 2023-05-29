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
            storePage.GetMoreMoney();
            storePage.GoToTab(tabName);

            var moneyAmount = storePage.GetTotalAmountOfMoney();

            var initialNumber = storePage.GetNumberOf(index);
            storePage.Buy(tabName, index);
            Assert.Multiple(() =>
            {
                Assert.True(storePage.GetNumberOf(index) - initialNumber == 1);
                Assert.True(moneyAmount - storePage.GetTotalAmountOfMoney() == storePage.GetPriceOf(tabName, index));
            });

        }
        public void AssertBuyAccessory(int index)
        {
            var tabName = "Accesories";
            storePage.GetMoreMoney();
            storePage.GoToTab(tabName);

            var initialMoneyAmount = storePage.GetTotalAmountOfMoney();
            bool isOwned = storePage.AssertOwning(tabName, index);
            storePage.Buy(tabName, index);


            if (isOwned)
                Assert.True(initialMoneyAmount == storePage.GetTotalAmountOfMoney());

            else
            {
                Assert.True(initialMoneyAmount - storePage.GetPriceOf(tabName, index) == storePage.GetTotalAmountOfMoney());
                Assert.True(storePage.AssertOwning(tabName, index));
                //todo to check also price 2
            }

        }
        public void DeleteAllData()
        {
            mainMenuPage = new MainMenuPage(altDriver);
            mainMenuPage.LoadScene();
            mainMenuPage.DeleteData();
        }


        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver();
            storePage = new StorePage(altDriver);
            storePage.Load();
        }
        [Test]
        public void TestGetMoreMoney()
        {
            int currentAmountOfFishbones = storePage.GetTotalAmountOfMoney();
            storePage.GetMoreMoney();
            Assert.True(storePage.GetTotalAmountOfMoney() - currentAmountOfFishbones == 1000000);
        }
        [Test]
        public void TestStoreIsDisplayed()
        {
            Assert.True(storePage.IsDisplayed());
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
        public void TestBuyMagnet()
        {
            AssertBuyItem(0);
        }

        [Test]
        public void TestBuyx2()
        {
            AssertBuyItem(1);
        }

        [Test]
        public void TestBuyInvincible()
        {
            AssertBuyItem(2);
        }

        [Test]
        public void TestBuyLife()
        {
            AssertBuyItem(3);
        }
        [Test]
        public void TestBuyAllItems()
        {
            storePage.GoToTab("Character");
            storePage.GetMoreMoney();
            for (int i = 0; i < 3; i++) AssertBuyItem(i);
        }
        [Test]
        public void TestAssertOwningTrashCatCharacter()
        {
            var tabName = "Character";
            storePage.GoToTab(tabName);
            Assert.True(storePage.AssertOwning(tabName, 0));
        }
        [Test]
        public void TestBuyRaccoon()
        {
            var tabName = "Character";
            storePage.GoToTab("Item");
            storePage.GetMoreMoney();

            storePage.GoToTab(tabName);
            storePage.Buy(tabName, 1);

            Assert.Multiple(() =>
            {
                Assert.True(storePage.AssertOwning(tabName, 1));
            });
        }
        [Test]
        public void TestBuySafetyHat()
        {
            AssertBuyAccessory(0);
        }
        
        [Test]
        public void TestBuyPartyHat()
        {

            AssertBuyAccessory(1);

        }

        [Test]
        public void TestBuySmart()
        {

            AssertBuyAccessory(2);

        }
        [Test]
        public void TestBuyAllHats()
        {
            Assert.Multiple(() =>
            {
                AssertBuyAccessory(0);
                AssertBuyAccessory(1);
                AssertBuyAccessory(2);
            });
        }

        [Test]
        public void TestCheckIfFishbonesAmountIsNextToIcon()
        {
            TestGetMoreMoney();
            Assert.True(storePage.FishbonesAmountByCoordinates());
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
        public void TestNewMagnetName()
        {
            string value = "magneeeeeeet";
            string tabName = "Items";
            string newName = storePage.ChangeItemName(tabName, 0, value);
            Assert.AreEqual(value, newName);
        }
        [Test]
        public void TestDifferentColorsOnPressing()
        {
            Assert.True(storePage.DifferentStateWhenPressingBtn());
        }
        [Test]
        [Ignore("This test is only for learning purposes")]
        public void TestDifferentStatesOnPointingObject()
        {
            AltObject buyMagnetButton = storePage.GetObjectsBuyButton("Items", 0);
            Assert.True(storePage.CheckObjectStatesOnPointing(buyMagnetButton));
        }

        [Test]
        public void TestBuyRaccoonAndHats()
        {
            Assert.Multiple(() =>
            {
                TestBuyRaccoon();
                AssertBuyAccessory(3);
                AssertBuyAccessory(4);
            });
        }

        [TearDown]
        public void Dispose()
        {
            DeleteAllData();
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}