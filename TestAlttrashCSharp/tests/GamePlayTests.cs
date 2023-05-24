using System;
using System.Threading;
using Altom.AltDriver;
using alttrashcat_tests_csharp.pages;
using NUnit.Framework;

namespace alttrashcat_tests_csharp.tests
{
    public class GamePlayTests
    {
        AltDriver altDriver;
        BasePage basePage;
        StorePage storePage;
        MainMenuPage mainMenuPage;
        GamePlay gamePlayPage;
        PauseOverlayPage pauseOverlayPage;
        GetAnotherChancePage getAnotherChancePage;
        [SetUp]
        public void Setup()
        {

            altDriver = new AltDriver();

            storePage = new StorePage(altDriver);
            storePage.Load();
            storePage.GetMoreMoney();

            storePage.GoToTab("Item");
            storePage.Buy("Item", 0);
            storePage.GoToTab("Character");
            storePage.Buy("Character", 1);
            storePage.GoToTab("Accesories");
            storePage.Buy("Accesories", 0);
            storePage.GoToTab("Themes");
            storePage.Buy("Themes", 1);

            mainMenuPage = new MainMenuPage(altDriver);
            mainMenuPage.LoadScene();

            mainMenuPage.TapArrowButton("power", "Right");
            mainMenuPage.TapArrowButton("theme", "Right");
            mainMenuPage.SelectRaccoonCharacter();
            mainMenuPage.TapArrowButton("character", "Right");


            mainMenuPage.MoveAltTesterLogo();
            mainMenuPage.PressRun();

            gamePlayPage = new GamePlay(altDriver);
            pauseOverlayPage = new PauseOverlayPage(altDriver);
            getAnotherChancePage = new GetAnotherChancePage(altDriver);

        }
        [Test]
        public void TestAssertCharacterIsMoving()
        {
            Assert.True(gamePlayPage.CharacterIsMoving());
        }
        [Test]
        public void TestGamePlayDisplayedCorrectly()
        {
            Assert.True(gamePlayPage.IsDisplayed());
        }
        [Test]
        public void TestGameCanBePausedAndResumed()
        {
            gamePlayPage.PressPause();
            Assert.True(pauseOverlayPage.IsDisplayed());
            pauseOverlayPage.PressResume();
            Assert.True(gamePlayPage.IsDisplayed());
        }
        public void TestGameCanBePausedAndStopped()
        {
            gamePlayPage.PressPause();
            pauseOverlayPage.PressMainMenu();
            Assert.True(mainMenuPage.IsDisplayed());
        }
        [Test]
        public void TestAvoidingObstacles()
        {
            gamePlayPage.AvoidObstacles(10);
            Assert.True(gamePlayPage.GetCurrentLife() > 0);
            //this test doesnt work properly
        }
        [Test]
        public void TestUseMagnetInGame()
        {
            gamePlayPage.ActivateMagnetInGame();
        }

        [Test]
        public void TestPlayerDiesWhenObstacleNotAvoided()
        {
            float timeout = 20;
            while (timeout > 0)
            {
                try
                {
                    getAnotherChancePage.IsDisplayed();
                    break;
                }
                catch (Exception)
                {
                    timeout -= 1;
                }
            }
            Assert.True(getAnotherChancePage.IsDisplayed());
            AltObject PremiumButton = getAnotherChancePage.GetAnotherChanceButton();

            Assert.True(getAnotherChancePage.AssertDifferentColorsOnPressing(PremiumButton));
            Assert.True(getAnotherChancePage.GetExpectedColorCodeValue(PremiumButton, "r") == getAnotherChancePage.GetCurrentColorCodeValue(PremiumButton, "r"));
            Assert.True(getAnotherChancePage.GetExpectedColorCodeValue(PremiumButton, "g") == getAnotherChancePage.GetCurrentColorCodeValue(PremiumButton, "g"));
            Assert.True(getAnotherChancePage.GetExpectedColorCodeValue(PremiumButton, "b") == getAnotherChancePage.GetCurrentColorCodeValue(PremiumButton, "b"));

            // Console.WriteLine("Expected r is: "+ getAnotherChancePage.GetExpectedColorCodeValue(getAnotherChancePage.GetAnotherChanceButton(), "r"));
            // Console.WriteLine("Current r is: "+ getAnotherChancePage.GetCurrentColorCodeValue(getAnotherChancePage.GetAnotherChanceButton(), "r"));

        }

        [Test]
        public void TestTimeScale()
        {
            altDriver.SetTimeScale(0.1f);
            Thread.Sleep(1000);
            var timeScaleFromGame = altDriver.GetTimeScale();
            Assert.AreEqual(0.1f, timeScaleFromGame);
            //teardown
            
        }

        [Test]
        public void TestDisplayAllEnabledElementsFromAnotherChancePage()
        {
            // is this a test?
            gamePlayPage.AvoidObstacles(3);
            getAnotherChancePage.DisplayAllEnabledElements();
        }

        [TearDown]
        public void Dispose()
        {
            mainMenuPage.LoadScene();
            mainMenuPage.DeleteData();
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}