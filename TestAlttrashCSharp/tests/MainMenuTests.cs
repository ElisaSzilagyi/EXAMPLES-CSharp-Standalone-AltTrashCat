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
        StorePage storePage;
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
        public void TestAssertMovingLogo()
        {
            AltVector2 initialPosition = mainMenuPage.AltTesterLogo.GetScreenPosition();
            mainMenuPage.MoveObject(mainMenuPage.AltTesterLogo, 30, 60);
            Assert.True(initialPosition.x - mainMenuPage.AltTesterLogo.getScreenPosition().x == 30);
            Assert.True(mainMenuPage.AltTesterLogo.getScreenPosition().y - initialPosition.y == 60);

        }
        [Test]
        public void TestPrintAllButtonsFromPage()
        {
            List<string> buttonsNames = new List<string>(mainMenuPage.GetAllButtons());
            Assert.True(buttonsNames.Contains("OpenLeaderboard"));
            Assert.True(buttonsNames.Contains("StoreButton"));
            Assert.True(buttonsNames.Contains("MissionButton"));
            Assert.True(buttonsNames.Contains("SettingButton"));
            Assert.True(buttonsNames.Contains("StoreButton"));
            Assert.True(buttonsNames.Contains("StartButton"));
        }
        [Test]
        public void TestButtonsAreCorrectlyDisplayed()
        {
            Assert.True(mainMenuPage.ButtonsAndTextDisplayedCorrectly());
        }
        [Test]
        public void TestStringKeyPlayerPref()
        {
            string setStringPref = "stringPlayerPrefInTrashcat";
            var stringPlayerPref = mainMenuPage.GetKeyPlayerPref("test", setStringPref);
            Console.WriteLine("stringPlayerPref: " + stringPlayerPref);
            Assert.That(stringPlayerPref, Is.EqualTo(setStringPref));
        }
        [Test]
        [Ignore("Loading scene is not working and I do not know why.")]
        public void TestUnloadScene()
        {
            mainMenuPage.LoadScene();
            Console.WriteLine("Number of loaded scenes: " + altDriver.GetAllLoadedScenes().Count);
            Console.WriteLine("First loaded scene: " + altDriver.GetAllLoadedScenes()[0]);
            mainMenuPage.Driver.LoadScene("Start");
            // storePage.Load();

            Console.WriteLine("Number of loaded scenes: " + altDriver.GetAllLoadedScenes().Count);

            Assert.AreEqual(2, altDriver.GetAllLoadedScenes().Count);
            // altDriver.UnloadScene("Store");
            Console.WriteLine("Number of loaded scenes after unloading store scene: " + altDriver.GetAllLoadedScenes().Count);
            Assert.AreEqual(1, altDriver.GetAllLoadedScenes().Count);
            Console.WriteLine("First loaded scene: " + altDriver.GetAllLoadedScenes()[0]);
            // Assert.AreEqual("Main", altDriver.GetAllLoadedScenes()[0]);
        }

        [Test]
        public void TestGetApplicationScreenSize()
        {
            altDriver.CallStaticMethod<string>("UnityEngine.Screen", "SetResolution", "UnityEngine.CoreModule",
            new string[] { "1920", "1080", "false" }, new string[] { "System.Int32", "System.Int32", "System.Boolean" });
            // to transporm this into a method in page

            var screensize = altDriver.GetApplicationScreenSize();

            Assert.Multiple(() =>
            {
                Assert.AreEqual(1920, screensize.x);
                Assert.AreEqual(1080, screensize.y);
                mainMenuPage.SetScreenResolutionUsingCallStaticMethod("380", "600");
            });
        }

        [Test]
        public void TestDeleteKey()
        {
            mainMenuPage.Driver.DeletePlayerPref();
            mainMenuPage.Driver.SetKeyPlayerPref("test", 1);
            var val = mainMenuPage.Driver.GetIntKeyPlayerPref("test");
            Assert.AreEqual(1, val);
            mainMenuPage.Driver.DeleteKeyPlayerPref("test");
            try
            {
                mainMenuPage.Driver.GetIntKeyPlayerPref("test");
                Assert.Fail();
            }
            catch (NotFoundException exception)
            {
                Assert.AreEqual("PlayerPrefs key test not found", exception.Message);
            }
        }

        [Test]
        public void TestGetCurrentScene()
        {
            Assert.AreEqual("Main", mainMenuPage.Driver.GetCurrentScene());
        }

        [TearDown]
        public void Dispose()
        {
            altDriver.Stop();
            Thread.Sleep(1000);
        }
    }
}