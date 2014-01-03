using System.Collections.Generic;

namespace Viiar.AtmFinder.Core
{
    public static class Settings
    {
        public static List<string> SupportedCountries
        {
            get
            {
                return new List<string>
                           {
                               CountryConstants.Latvia,
                               CountryConstants.Estonia,
                               CountryConstants.Lithuania
                           };
            }
        }
    }
}
