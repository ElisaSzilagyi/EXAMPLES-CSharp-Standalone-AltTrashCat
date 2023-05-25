using Altom.AltDriver;
using Newtonsoft.Json;
using System.Text.Json;



namespace alttrashcat_tests_csharp.pages
{
    public class GetAnotherChancePage : BasePage
    {
        public GetAnotherChancePage(AltDriver driver) : base(driver)
        {
        }

        public AltObject GameOverButton { get => Driver.WaitForObject(By.NAME, "GameOver", timeout: 2); }
        public AltObject PremiumButton { get => Driver.WaitForObject(By.NAME, "Premium Button", timeout: 2); }
        public AltObject AvailableCurrency { get => Driver.WaitForObject(By.NAME, "PremiumOwnCount", timeout: 2); }

        public bool IsDisplayed()
        {
            if (GameOverButton != null && PremiumButton != null && AvailableCurrency != null)
                return true;
            return false;
        }

        public object GetCurrentColorDetails(AltObject button)
        {
            object color = button.CallComponentMethod<object>("UnityEngine.CanvasRenderer", "GetColor", "UnityEngine.UIModule", new object[] { });
            return color;
        }
        public void DisplayAllEnabledElements()
        {
            List<AltObject> altObjects = Driver.GetAllElements(enabled: true);
            Console.WriteLine("Number of enabled elements: "+  altObjects.Count);
            // foreach (AltObject obj in altObjects)
                // Console.WriteLine("AltObject from list: " + obj.name);
        }

        public int GetCurrentStateNumber(AltObject button)
        {
            int state = button.CallComponentMethod<int>("UnityEngine.UI.Button", "get_currentSelectionState", "UnityEngine.UI", new object[] { });
            return state;
        }
        public object GetListOfStates(AltObject button)
        {
            object state = button.CallComponentMethod<object>("UnityEngine.UI.Button", "get_colors", "UnityEngine.UI", new object[] { });
            return state;
        }
        public string GetStateReference(int index)
        {
            switch (index)
            {
                case 0:
                    return "normalColor";
                case 1:
                    return "highlightedColor";
                case 2:
                    return "pressedColor";
                case 3:
                    return "selectedColor";
                case 4:
                    return "disabledColor";
                default:
                    return "";
            }
        }

        public float GetExpectedColorCodeValue(AltObject button, string rgbChar)
        {
            int currentState = GetCurrentStateNumber(button);
            string expectedStateRefference = GetStateReference(currentState);
            object listOfStates = GetListOfStates(button);

            string json = JsonConvert.SerializeObject(listOfStates);
            JsonElement parsedJson = JsonDocument.Parse(json).RootElement;

            float value = parsedJson.GetProperty(expectedStateRefference).GetProperty(rgbChar).GetSingle();

            return value;
        }

        public float GetCurrentColorCodeValue(AltObject button, string rgbChar)
        {
            object initialColor = GetCurrentColorDetails(button);
            string json = JsonConvert.SerializeObject(initialColor);
            // JsonElement parsedJson = JsonDocument.Parse(json).RootElement;
            // float currentColor = parsedJson.GetProperty(rbgChar).GetSingle();
            dynamic colorData = JsonConvert.DeserializeObject(json);
            if (rgbChar == "r") return colorData.r;
            else if (rgbChar == "g") return colorData.g;
            else return colorData.b;
            // float rInitialColor = colorData.r;

        }
        public float GetRFromCurrentColor(AltObject obj){
            object initialColor = GetCurrentColorDetails(obj);
            string json = JsonConvert.SerializeObject(initialColor);
            dynamic colorData = JsonConvert.DeserializeObject(json);
            float rInitialColor = colorData.r;
            return rInitialColor;
        }

        public bool AssertDifferentColorsOnPressing(AltObject button)
        {

            //idee de test:
            // get current color
            // get current state
            // compare the current color with the color tha the state should have.

            float rInitialColor = GetRFromCurrentColor(button);

            button.PointerDownFromObject();
            Thread.Sleep(1000);

            float rAfterPointerDownColor = GetRFromCurrentColor(button);
            
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("r initial color:" + rInitialColor);
            Console.WriteLine("r after pointing down color:" + rAfterPointerDownColor);

            return (rInitialColor != rAfterPointerDownColor);
        }
        public void PrintColor(object color)
        {
            string json = JsonConvert.SerializeObject(color);
            dynamic colorData = JsonConvert.DeserializeObject(json);
            float rValue = colorData.r;
            // FailedToParseArgumentsException rValue = colorData.r;
            Console.WriteLine("The color object: " + color);
            Console.WriteLine("The json: " + json);
            Console.WriteLine("The colorData: " + colorData);
            Console.WriteLine("The r value is: " + rValue);
            Console.WriteLine("Type of r value: " + rValue.GetType());

        }
        public float GetRGBFromColorObject(AltObject color)
        {
            string json = JsonConvert.SerializeObject(color);
            dynamic colorData = JsonConvert.DeserializeObject(json);
            float rValue = colorData.r;
            return rValue;


            // float rValue = colorData.r;
            // // FailedToParseArgumentsException rValue = colorData.r;
            // Console.WriteLine("The color object: " + color);
            // Console.WriteLine("The json: " + json);
            // Console.WriteLine("The colorData: " + colorData);
            // Console.WriteLine("The r value is: " + rValue);
        }
        public AltObject GetAnotherChanceButton()
        {
            return PremiumButton;
        }
    }
}