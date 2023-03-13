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
        // MainMenuPage mainMenuPage;
        // private StartPage startPage;
        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver();
            storePage = new StorePage(altDriver);
            storePage.Load();

        }

        [Test]
        // public void GoToStorePage()
        // {
        //     mainMenuPage.PressStoreButton();
        //     Assert.True(storePage.IsDisplayed());
        // }
        public void BuyMagnet()
        {
            int currentMagnets = storePage.GetNumberOfMagnets();
            storePage.BuyMagnet();
            Assert.True(storePage.GetNumberOfMagnets() - currentMagnets == 1);
        }

        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}