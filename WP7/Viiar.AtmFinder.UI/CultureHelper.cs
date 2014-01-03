using System.Globalization;

namespace Viiar.AtmFinder.UI
{
    public class CultureHelper
    {
        public static readonly string EnglishLanguage = "English";
        public static readonly string LatvianLanguage = "Latvian";
        public static readonly string EstonianLanguage = "Estonian";
        public static readonly string LithuanianLanguage = "Lithuanian";
        public static readonly string RussianLanguage = "Russian";

        public static void SetResourceCulture(string cultureName)
        {
            if (cultureName.Equals(EnglishLanguage))
            {
                AppResources.Culture = new CultureInfo("en-US");
            }
            else if (cultureName.Equals(LatvianLanguage))
            {
                AppResources.Culture = new CultureInfo("lv-LV");
            }
            else if (cultureName.Equals(EstonianLanguage))
            {
                AppResources.Culture = new CultureInfo("et-EE");
            }
            else if (cultureName.Equals(LithuanianLanguage))
            {
                AppResources.Culture = new CultureInfo("lt-LT");
            }
            else if (cultureName.Equals(RussianLanguage))
            {
                AppResources.Culture = new CultureInfo("ru-RU");
            }
        }

        public static bool IsNativelySupported(string language)
        {
            return language.Equals(EnglishLanguage) || language.Equals(RussianLanguage);
        }
    }
}
