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

        /// <summary>
        /// buy with items, characters, accessories elements' indexes.
        /// </summary>
        public void BuyFromStore(int[] fromItems = null, int[] fromCharacters = null, int[] fromAccessories = null, int[] fromThemes = null)
        {
            storePage = new StorePage(altDriver);
            storePage.Load();
            storePage.GetMoreMoney();

            if (fromItems != null && fromItems.Length > 0)
            {
                storePage.GoToTab("Item");
                foreach (int itemIndex in fromItems) storePage.Buy("Item", itemIndex);
            }

            if (fromCharacters != null && fromCharacters.Length > 0)
            {
                storePage.GoToTab("Character");
                foreach (int characterIndex in fromCharacters) storePage.Buy("Character", characterIndex);
            }

            if (fromAccessories != null && fromAccessories.Length > 0)
            {
                storePage.GoToTab("Accesories");
                foreach (int accessoryIndex in fromAccessories) storePage.Buy("Accessories", accessoryIndex);
            }

            if (fromThemes != null && fromThemes.Length > 0)
            {
                storePage.GoToTab("Themes");
                foreach (int themeIndex in fromThemes) storePage.Buy("Themes", themeIndex);
            }
        }

        [SetUp]
        public void Setup()
        {
            altDriver = new AltDriver();
            storePage = new StorePage(altDriver);
            mainMenuPage = new MainMenuPage(altDriver);

            BuyFromStore(new int[] { 0, 1 }, new int[] { 1 }, new int[] { 0, 1 });

            mainMenuPage.LoadScene();
            mainMenuPage.MoveObject(mainMenuPage.AltTesterLogo);
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
        [Test]
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
            BuyFromStore(new int[] { 0, 1 }); //bought magnet
            int numOfMagents = storePage.GetNumberOf(0);
            mainMenuPage.LoadScene();
            mainMenuPage.TapArrowButton("power", "Right");
            mainMenuPage.PressRun();
            gamePlayPage.ActivateMagnetInGame();

            storePage.Load();
            storePage.GoToTab("Item");
            Assert.True(numOfMagents - storePage.GetNumberOf(0) == 1);
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
        }

        [Test]
        public void TestTimeScale()
        {
            altDriver.SetTimeScale(0.1f);
            Thread.Sleep(1000);
            var timeScaleFromGame = altDriver.GetTimeScale();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(0.1f, timeScaleFromGame);
                altDriver.SetTimeScale(1f);
            });
        }

        [Test]
        public void TestDisplayAllEnabledElementsFromAnotherChancePage()
        {
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