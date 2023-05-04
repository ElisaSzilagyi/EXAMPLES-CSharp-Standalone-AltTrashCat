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
        public AltObject GetObjectByName(string name){
            return Driver.WaitForObject(By.NAME, name, timeout: 2);
        }
        public AltObject GetObjectByPath(string path){
            return Driver.WaitForObject(By.PATH, path, timeout: 2);
        }
        public float GetRGBFromColorObject(AltObject color)
        {
            // List<float> rgb = new List<float>();
            string json = JsonConvert.SerializeObject(color);
            dynamic colorData = JsonConvert.DeserializeObject(json);
            // rgb.Add(colorData.r);
            // rgb.Add(colorData.g);
            // rgb.Add(colorData.b);
            return colorData.r;


            // float rValue = colorData.r;
            // // FailedToParseArgumentsException rValue = colorData.r;
            // Console.WriteLine("The color object: " + color);
            // Console.WriteLine("The json: " + json);
            // Console.WriteLine("The colorData: " + colorData);
            // Console.WriteLine("The r value is: " + rValue);
        }
    }
}