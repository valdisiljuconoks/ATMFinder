using System;
using Viiar.AtmFinder.Core.Repositories.Estonia;
using Viiar.AtmFinder.Core.Repositories.Latvia;
using Viiar.AtmFinder.Core.Repositories.Lithuania;

namespace Viiar.AtmFinder.Core.Repositories
{
    public static class CountryRepositoryFactory
    {
        public static CountryRepositoryBase GetByName(string country)
        {
            if (country == null)
            {
                throw new ArgumentNullException("country");
            }

            if (country.Equals(CountryConstants.Latvia, StringComparison.OrdinalIgnoreCase))
            {
                return new LatviaCountryRepository();
            }

            if (country.Equals(CountryConstants.Estonia, StringComparison.OrdinalIgnoreCase))
            {
                return new EstoniaCountryRepository();
            }

            if (country.Equals(CountryConstants.Lithuania, StringComparison.OrdinalIgnoreCase))
            {
                return new LithuaniaCountryRepository();
            }

            throw new InvalidOperationException("Country '" + country + "' is not supported");
        }

        public static void SetByName(string country)
        {
            if (string.IsNullOrEmpty(country))
            {
                throw new ArgumentNullException("country");
            }

            CountryRepositoryBase.Current = GetByName(country);
        }
    }
}
