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
        public AltObject ThemeName { get => Driver.WaitForObject(By.NAME, "ThemeZone", timeout: 10); }
        public AltObject RightCharacterBtn { get => Driver.WaitForObject(By.PATH, "/UICamera/Loadout/CharZone/CharName/CharSelector/ButtonRight", timeout: 10); }
        public AltObject AltTesterLogo { get => Driver.WaitForObject(By.PATH, "/AltTesterPrefab/AltUnityDialog/Icon", timeout: 10); }


        public bool IsDisplayed()
        {
            if (StoreButton != null && LeaderBoardButton != null && SettingsButton != null && MissionButton != null && RunButton != null && CharacterName != null && ThemeName != null)
                return true;
            return false;
        }
        public AltObject GetAltTesterLogo(){
            AltTesterLogo.UpdateObject();
            return AltTesterLogo;
        }

        /// <summary>
        /// section = character, power, theme
        /// direction = Right, Left
        /// </summary>
        public void TapArrowButton(string section, string direction)
        {
            string path = "/UICamera/Loadout";
            if (section == "character")
                path += $"/CharZone/CharName/CharSelector/Button{direction}";
            if (section == "power")
                path += $"/PowerupZone/Button{direction}";
            if (section == "theme")
                path += $"/ThemeZone/ThemeSelector/Button{direction}";

            Driver.WaitForObject(By.PATH, path, timeout: 5).Tap();
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

        public void MoveObject(AltObject obj, int xMoving = 20, int yMoving = 20)
        {
            // obj.UpdateObject();
            AltVector2 initialPosition = obj.GetScreenPosition();
            int fingerId = Driver.BeginTouch(initialPosition);
            AltVector2 newPosition = new AltVector2(initialPosition.x - xMoving, initialPosition.y + yMoving);
            Driver.MoveTouch(fingerId, newPosition);
            Driver.EndTouch(fingerId);
        }

        public string GetKeyPlayerPref(string key, string setValue)
        {
            Driver.SetKeyPlayerPref(key, setValue);
            return Driver.GetStringKeyPlayerPref(key);
        }

        public List<string> GetAllButtons()
        {
            var allButtons = Driver.FindObjectsWhichContain(By.NAME, "Button");
            allButtons.Add(Driver.FindObjectWhichContains(By.NAME, "Leaderboard"));
            List<string> buttonsNames = new List<string>();
            foreach (var button in allButtons)
            {
                var path = Driver;
                buttonsNames.Add(button.GetComponentProperty<string>("UnityEngine.UI.Button", "name", "UnityEngine.UI"));
            }
            return buttonsNames;
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
                string title = textObject.GetComponentProperty<string>("UnityEngine.UI.Text", "text", "UnityEngine.UI");
                string buttonTitle = textObject.GetParent().name;

                switch (title)
                {
                    case "LEADERBOARD":
                        if (buttonTitle != "OpenLeaderboard")
                            everythingIsFine = false;
                        break;
                    case "STORE":
                        if (buttonTitle != "StoreButton")
                            everythingIsFine = false;
                        break;
                    case "MISSIONS":
                        if (buttonTitle != "MissionButton")
                            everythingIsFine = false;
                        break;
                    case "Settings":
                        if (buttonTitle != "SettingButton")
                            everythingIsFine = false;
                        break;
                }
            }

            return everythingIsFine;
        }

        public AltVector2 GetScreenSize()
        {
            var width = Driver.GetStaticProperty<int>("UnityEngine.Screen", "currentResolution.width", "UnityEngine.CoreModule");
            var height = Driver.GetStaticProperty<int>("UnityEngine.Screen", "currentResolution.height", "UnityEngine.CoreModule");
            AltVector2 size = new AltVector2(width, height);
            return size;
        }

        public void DeleteData()
        {
            SettingsButton.Tap();
            DeleteDataBtn.Tap();
            ConfirmDeleteDataBtn.Tap();

        }
    }
}