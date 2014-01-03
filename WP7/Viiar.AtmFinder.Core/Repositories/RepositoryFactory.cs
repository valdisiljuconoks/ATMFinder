using System;
using System.Collections.Generic;
using System.Linq;
using Viiar.AtmFinder.Core.Extensions;
using Viiar.AtmFinder.Core.Models;

namespace Viiar.AtmFinder.Core.Repositories
{
    public static class RepositoryFactory
    {
        public static IEnumerable<Bank> SupportedBanks
        {
            get
            {
                var result = new List<Bank>();

                foreach (var country in Settings.SupportedCountries)
                {
                    var countryRepository = CountryRepositoryFactory.GetByName(country);
                    countryRepository.RetrieveSupportedChains().ForEach(c => result.Add(new Bank
                                                                                            {
                                                                                                Name = c,
                                                                                                Country = countryRepository.Name,
                                                                                                Code =
                                                                                                    countryRepository.ResolveBankCode(c) + "-" +
                                                                                                    countryRepository.Name.ToLowerInvariant()
                                                                                            }));
                }

                return result;
            }
        }

        public static IEnumerable<Bank> DecodeFromSettings(List<string> settingValue)
        {
            var result = new List<Bank>();
            if (settingValue == null)
            {
                return result;
            }

            if (settingValue.Any())
            {
                foreach (var value in settingValue)
                {
                    var splited = value.Split(new[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    if (splited.Length != 2)
                    {
                        throw new InvalidOperationException("Failed to decode from settings key. Cannot split value '" + value + "'");
                    }

                    var chainCode = splited[0];
                    var country = splited[1];

                    var repository = CountryRepositoryFactory.GetByName(country);

                    result.Add(new Bank { Name = repository.ResolveBankName(chainCode), Code = chainCode, Country = country });
                }
            }

            return result;
        }
    }
}
