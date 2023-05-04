using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using System;
using System.Threading;
using NUnit.Framework;
namespace alttrashcat_tests_csharp.tests
{
    public class MainMenuTests
    {
        AltDriver altDriver;
        MainMenuPage mainMenuPage;
        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver(port: 13000);
            mainMenuPage = new MainMenuPage(altDriver);
            mainMenuPage.LoadScene();
        }



        [Test]
        public void TestMainMenuPageLoadedCorrectly()
        {
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        public void AssertMovingLogo(){
            AltVector2 initialPosition = mainMenuPage.GetAltTesterLogo().GetScreenPosition();
            mainMenuPage.MoveAltTesterLogo(30, 60);
            Assert.True(initialPosition.x - mainMenuPage.GetAltTesterLogo().getScreenPosition().x == 30);
            Assert.True(mainMenuPage.GetAltTesterLogo().getScreenPosition().y - initialPosition.y == 60);

        }
        [Test]
        public void PrintAllButtonsFromPage(){
            mainMenuPage.GetAllButtons();
        }
        [Test]
        public void ButtonsAreCorrectlyDisplayed(){
            Assert.True(mainMenuPage.ButtonsAndTextDisplayedCorrectly());
        }

        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }



    }
}