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
        public AltObject BuyMagnetBtn { get => Driver.WaitForObject(By.NAME, "BuyButton", timeout: 5); }
        public AltObject CloseStoreBtn { get => Driver.WaitForObject(By.NAME, "BuyButton", timeout: 5); }
        public AltObject NumOfMagnets { get => Driver.WaitForObject(By.PATH, "/Canvas/Background/ItemsList/Container/ItemEntry(Clone)/Icon/Count", timeout: 5); }




        public bool IsDisplayed()
        {
            if (ItemsTabBtn != null && CharactersTabBtn != null && AccessoriesTabBtn != null && ThemesTabBtn != null)
                return true;
            return false;
        }
        public void BuyMagnet()
        {
            BuyMagnetBtn.Tap();
        }
        public int GetNumberOfMagnets()
        {
            string numOfM = NumOfMagnets.GetText();
            return int.Parse(numOfM);
        }
        public void CloseStore()
        {
            CloseStoreBtn.Tap();
        }
    }
}