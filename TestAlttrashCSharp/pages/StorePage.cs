using Altom.AltDriver;

namespace alttrashcat_tests_csharp.pages
{
    public class StorePage : BasePage
    {
        public StorePage(AltDriver driver) : base(driver)
        {
        }

        public void Load()
        {
            Driver.LoadScene("Shop");
        }
        public AltObject ItemsTabBtn { get => Driver.WaitForObject(By.NAME, "Item", timeout: 5); }
        public AltObject CharactersTabBtn { get => Driver.WaitForObject(By.NAME, "Character", timeout: 5); }
        public AltObject AccessoriesTabBtn { get => Driver.WaitForObject(By.NAME, "Accesories", timeout: 5); }
        public AltObject ThemesTabBtn { get => Driver.WaitForObject(By.NAME, "Themes", timeout: 5); }
        public AltObject CloseStoreBtn { get => Driver.WaitForObject(By.NAME, "Button", timeout: 5); }
        public AltObject TotalFishbones { get => Driver.WaitForObject(By.NAME, "CoinsCounter", timeout: 5); }
        public AltObject StoreSecretMoneyButton { get => Driver.WaitForObject(By.NAME, "StoreTitle", timeout: 5); }
        public AltObject FishbonesIcon { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/Coin/Image"); }

        public bool IsDisplayed()
        {
            if (ItemsTabBtn != null && CharactersTabBtn != null && AccessoriesTabBtn != null && ThemesTabBtn != null)
                return true;
            return false;
        }

        /// <summary>
        /// tabName = Item, Character, Accesories, Themes
        /// </summary>
        public void GoToTab(string tabName)
        {
            Driver.WaitForObject(By.NAME, tabName).Tap();
        }

        public string GetPathByTabName(string tabName)
        {
            if (tabName == "Item") return "ItemsList";
            if (tabName == "Character") return "CharacterList";
            if (tabName == "Accesories") return "CharacterAccessoriesList";
            if (tabName == "Themes") return "ThemeList";
            return "";

        }

        public void GetMoreMoney()
        {
            StoreSecretMoneyButton.Click();
        }

        /// <summary>
        /// tabName = Items, Accesories, Character, Themes
        /// </summary>
        public AltObject GetObjectsBuyButton(string tabName, int index, string endPath = "")
        {
            string tabNamePath = GetPathByTabName(tabName);
            var Objects = Driver.FindObjectsWhichContain(By.PATH, $"/Canvas/Background/{tabNamePath}/Container/ItemEntry(Clone)/NamePriceButtonZone/PriceButtonZone/BuyButton{endPath}");
            return Objects[index];
        }
        public int GetNumberOf(int index)
        {
            var Objects = Driver.FindObjectsWhichContain(By.PATH, "/Canvas/Background/ItemsList/Container/ItemEntry(Clone)/Icon/Count");
            return int.Parse(Objects[index].GetText());
        }
        public AltObject GetNameObjectByIndexInPage(string tabName, int index)
        {
            string tabNamePath = GetPathByTabName(tabName);
            var Objects = Driver.FindObjectsWhichContain(By.PATH, $"/Canvas/Background/{tabNamePath}/Container/ItemEntry(Clone)/NamePriceButtonZone/Name");
            return Objects[index];
        }
        public int GetPriceOf(string tabName, int index)
        {
            string tabNamePath = GetPathByTabName(tabName);
            var Objects = Driver.FindObjectsWhichContain(By.PATH, $"/Canvas/Background/{tabNamePath}/Container/ItemEntry(Clone)/NamePriceButtonZone/PriceButtonZone/PriceZone/PriceCoin/Amount");
            return int.Parse(Objects[index].GetText());
        }

        /// <summary>
        /// tabName = Items, Accesories, Character, Themes
        /// </summary>
        public void Buy(string tabName, int index)
        {
            GetObjectsBuyButton(tabName, index).Tap();
        }

        /// <summary>
        /// tabName = Items, Accesories, Character, Themes
        /// </summary>
        public void BuyAllFromTab(string tabName)
        {
            int index = 0;
            bool goOn = true;
            while (goOn == true)
            {
                try
                {
                    GetObjectsBuyButton(tabName, index).Tap();
                    index += 1;

                }
                catch
                {
                    goOn = false;
                }
            }
        }

        public string ChangeAmountOfMoney(string value)
        {
            const string propertyName = "text";
            TotalFishbones.SetComponentProperty("UnityEngine.UI.Text", propertyName, value, "UnityEngine.UI");
            TotalFishbones.SetText(value, true);
            var propertyValue = TotalFishbones.GetComponentProperty<string>("UnityEngine.UI.Text", propertyName, "UnityEngine.UI");
            return propertyValue;
        }

        public bool FishbonesAmountByCoordinates()
        {
            int currentFishbones = GetTotalAmountOfMoney();
            var fishbonesAmount = Driver.FindObjectAtCoordinates(new AltVector2(134 + FishbonesIcon.x, FishbonesIcon.y - 3));
            return (currentFishbones == int.Parse(fishbonesAmount.GetText()));
        }

        public void SetButtonInteractable(AltObject button)
        {
            button.SetComponentProperty("UnityEngine.UI.Button", "interactable", true, "UnityEngine.UI");
        }
        public string GetObjectProperty(AltObject button, string propertyName)
        {
            var propertyValue = button.GetComponentProperty<string>("UnityEngine.UI.Button", propertyName, "UnityEngine.UI");
            return propertyValue.ToString();
        }

        public bool DifferentStateWhenPressingBtn()
        {
            int state1 = StoreSecretMoneyButton.CallComponentMethod<int>("UnityEngine.UI.Button", "get_currentSelectionState", "UnityEngine.UI", new object[] { });
            var state2 = StoreSecretMoneyButton.PointerDownFromObject().CallComponentMethod<int>("UnityEngine.UI.Button", "get_currentSelectionState", "UnityEngine.UI", new object[] { });
            if (state1 == state2) return false;
            return true;
        }
        public int GetCurrentSelectionStateNumber(AltObject button)
        {
            return button.CallComponentMethod<int>("UnityEngine.UI.Button", "get_currentSelectionState", "UnityEngine.UI", new object[] { });
        }
        public bool CheckObjectStatesOnPointing(AltObject button)
        {
            //this function has only a purpose of learning the states of the button, and not actually test button state
            int state0 = GetCurrentSelectionStateNumber(button);
            button.PointerDownFromObject();
            Thread.Sleep(1000);
            int state1 = GetCurrentSelectionStateNumber(button);
            button.PointerUpFromObject();
            int state2 = GetCurrentSelectionStateNumber(button);
            button.PointerEnterObject();
            int state3 = GetCurrentSelectionStateNumber(button);
            button.PointerExitObject();
            int state4 = GetCurrentSelectionStateNumber(button);

            Console.WriteLine("State 0:" + state0);
            Console.WriteLine("State 1:" + state1);
            Console.WriteLine("State 2:" + state2);
            Console.WriteLine("State 3:" + state3);
            Console.WriteLine("State 4:" + state4);

            if (state1 == state2) return false;
            return true;
        }

        /// <summary>
        /// tabName = Items, Character, Accessories, Themes???? is it this way?
        /// index = the position of the element in the list
        /// </summary>
        public string ChangeItemName(string tabName, int index, string newName)
        {

            const string propertyName = "text";
            AltObject NewObject = GetNameObjectByIndexInPage(tabName, index);
            NewObject.SetText(newName, true);

            var propertyValue = NewObject.GetComponentProperty<string>("UnityEngine.UI.Text", propertyName, "UnityEngine.UI");
            return propertyValue;
        }
        public int GetTotalAmountOfMoney()
        {
            string coins = TotalFishbones.GetText();
            return int.Parse(coins);
        }
        public bool IsOwned(AltObject buyButton){
            string buttonText = buyButton.GetText();
            if (buttonText == "Owned") return true;
            return false;
        }

        public bool AssertOwning(string tabName, int index)
        {
            var buyBtnText = GetObjectsBuyButton(tabName, index, "/Text");
            if (buyBtnText.GetText() == "Owned")
                return true;
            return false;
        }

        public void CloseStore()
        {
            CloseStoreBtn.Tap();
        }
    }
}