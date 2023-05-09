using Altom.AltDriver;

namespace alttrashcat_tests_csharp.pages
{
    public class MainMenuPage : BasePage
    {
        public MainMenuPage(AltDriver driver) : base(driver)
        {
        }
        public void LoadScene()
        {
            Driver.LoadScene("Main");
        }

        public AltObject StoreButton { get => Driver.WaitForObject(By.NAME, "StoreButton", timeout: 10); }
        public AltObject LeaderBoardButton { get => Driver.WaitForObject(By.NAME, "OpenLeaderboard", timeout: 10); }
        public AltObject SettingsButton { get => Driver.WaitForObject(By.NAME, "SettingButton", timeout: 10); }
        public AltObject DeleteDataBtn { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/SettingPopup/Background/DeleteData", timeout: 10); }
        public AltObject ConfirmDeleteDataBtn { get => Driver.WaitForObject(By.NAME, "YESButton", timeout: 10); }

        public AltObject MissionButton { get => Driver.WaitForObject(By.NAME, "MissionButton", timeout: 10); }
        public AltObject RunButton { get => Driver.WaitForObject(By.NAME, "StartButton", timeout: 10); }
        public AltObject CharacterName { get => Driver.WaitForObject(By.NAME, "CharName", timeout: 10); }
        public AltObject AccessoriesTopBtn { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/AccessoriesSelector/ButtonTop", timeout: 10); }

        public AltObject ThemeName { get => Driver.WaitForObject(By.NAME, "ThemeZone", timeout: 10); }
        public AltObject ButtonRight { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/PowerupZone/ButtonRight", timeout: 10); }
        public AltObject RightCharacterBtn { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/CharZone/CharName/CharSelector/ButtonRight", timeout: 10); }
        // /UICamera/Loadout/ThemeZone/ThemeSelector/ButtonRight

        public AltObject AltTesterLogo { get => Driver.WaitForObject(By.PATH, "/AltTesterPrefab/AltUnityDialog/Icon", timeout: 10); }


        public bool IsDisplayed()
        {
            if (StoreButton != null && LeaderBoardButton != null && SettingsButton != null && MissionButton != null && RunButton != null && CharacterName != null && ThemeName != null)
                return true;
            return false;
        }
        public AltObject GetAltTesterLogo()
        {
            return AltTesterLogo;
        }



        public void TapArrowButton(string section, string direction)
        {
            // section shoud be one of these: character, power, theme
            // direction should be Right or Left
            string path = "/UICamera/Loadout";
            if (section == "character")
                path += $"/CharZone/CharName/CharSelector/Button{direction}";
            if (section == "power")
                path += $"/PowerupZone/Button{direction}";
            if (section == "theme")
                path += $"/ThemeZone/ThemeSelector/Button{direction}";

            Driver.WaitForObject(By.PATH, path, timeout: 5).Tap();
        }

        public void UseMagnetSetup()
        {
            ButtonRight.Tap();
        }
        public void ChangeCharacter()
        {
            RightCharacterBtn.Tap();
        }
        public void SelectRaccoonCharacter()
        {
            while (GetCharacterName() != "Rubbish Raccoon")
                ChangeCharacter();
        }
        public void ChangeHat()
        {
            AccessoriesTopBtn.Tap();
        }
        public void SetScreenResolutionUsingCallStaticMethod(string widthSet, string heightSet)
        {
            string[] parameters = new[] { widthSet, heightSet, "false" };
            string[] typeOfParameters = new[] { "System.Int32", "System.Int32", "System.Boolean" };
            Driver.CallStaticMethod<string>("UnityEngine.Screen", "SetResolution", "UnityEngine.CoreModule", parameters, typeOfParameters);
        }
        public string GetCharacterName()
        {
            return CharacterName.GetComponentProperty<string>("UnityEngine.UI.Text", "text", "UnityEngine.UI");
        }

        public void MoveAltTesterLogo(int xMoving = 20, int yMoving = 20)
        {
            // var initialPosition = new AltVector2(AltTesterLogo.x, AltTesterLogo.y);
            AltVector2 initialPosition = AltTesterLogo.GetScreenPosition();
            int fingerId = Driver.BeginTouch(initialPosition);
            AltVector2 newPosition = new AltVector2(AltTesterLogo.x - xMoving, AltTesterLogo.y + yMoving);
            Driver.MoveTouch(fingerId, newPosition);
            Driver.EndTouch(fingerId);
        }

        public string GetKeyPlayerPref(string key, string setValue)
        {
            Driver.SetKeyPlayerPref(key, setValue);
            return Driver.GetStringKeyPlayerPref(key);
        }



        public void GetAllButtons()
        {
            var allButtons = Driver.FindObjectsWhichContain(By.NAME, "Button");
            foreach (var button in allButtons)
            {
                var path = Driver;
                // get button's path
                // go to text path based on button's path
                // check text / print it
                Console.WriteLine(button.GetComponentProperty<string>("UnityEngine.UI.Button", "name", "UnityEngine.UI"));
            }
        }

        public void PressRun()
        {
            RunButton.Tap();
        }

        public bool ButtonsAndTextDisplayedCorrectly()
        {
            bool everythingIsFine = true;
            var textFromPage = Driver.FindObjects(By.NAME, "Text");
            foreach (AltObject textObject in textFromPage)
            {
                if (textObject.GetComponentProperty<string>("UnityEngine.UI.Text", "text", "UnityEngine.UI") == "LEADERBOARD")
                {
                    if (textObject.getParent().name != "OpenLeaderboard")
                        everythingIsFine = false;
                }
                else if (textObject.GetComponentProperty<string>("UnityEngine.UI.Text", "text", "UnityEngine.UI") == "STORE")
                {
                    if (textObject.getParent().name != "StoreButton")
                        everythingIsFine = false;
                }
                else if (textObject.GetComponentProperty<string>("UnityEngine.UI.Text", "text", "UnityEngine.UI") == "MISSIONS")
                {
                    if (textObject.getParent().name != "MissionButton")
                        everythingIsFine = false;
                }
                else if (textObject.GetComponentProperty<string>("UnityEngine.UI.Text", "text", "UnityEngine.UI") == "Settings")
                {
                    if (textObject.getParent().name != "SettingButton")
                        everythingIsFine = false;
                }
            }
            return everythingIsFine;
        }

        public void DeleteData()
        {
            SettingsButton.Tap();
            DeleteDataBtn.Tap();
            ConfirmDeleteDataBtn.Tap();

        }
    }
}