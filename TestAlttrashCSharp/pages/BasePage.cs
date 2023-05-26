using Altom.AltDriver;
using Newtonsoft.Json;

namespace alttrashcat_tests_csharp.pages
{
    public class BasePage
    {
        AltDriver driver;

        public AltDriver Driver { get => driver; set => driver = value; }
        public BasePage(AltDriver driver)
        {
            Driver = driver;
        }
        public AltObject GetObjectByPath(string path){
            return Driver.WaitForObject(By.PATH, path, timeout: 2);
        }
        public float GetRGBFromColorObject(AltObject color)
        {
            string json = JsonConvert.SerializeObject(color);
            dynamic colorData = JsonConvert.DeserializeObject(json);
            return colorData.r;
        }
    }
}