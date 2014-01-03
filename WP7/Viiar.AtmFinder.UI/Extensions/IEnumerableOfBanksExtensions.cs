using System.Collections.Generic;
using System.Linq;
using Viiar.AtmFinder.Core.Models;
using Viiar.AtmFinder.UI.ViewModels;

namespace Viiar.AtmFinder.UI.Extensions
{
    public static class IEnumerableOfBanksExtensions
    {
        public static IEnumerable<MyBankViewModel> ToViewModel(this IEnumerable<Bank> allBanks, List<string> selectedBanks)
        {
            var result = new List<MyBankViewModel>();

            foreach (var country in allBanks.Select(b => b.Country).Distinct())
            {
                var banksInCountry = allBanks.Where(b => b.Country == country).OrderBy(b => b.Name).ToList();
                for (var i = 0; i < banksInCountry.Count; i++)
                {
                    var bank = new MyBankViewModel
                                   {
                                       Name = string.Format("{0} ({1})", banksInCountry[i].Name, banksInCountry[i].Country),
                                       IsSelected = selectedBanks.Any(b => banksInCountry[i].Code == b),
                                       Code = banksInCountry[i].Code
                                   };

                    if (i == banksInCountry.Count - 1)
                    {
                        bank.IsLastInCountry = true;
                    }

                    result.Add(bank);
                }
            }

            // fix the very last bank settings
            result.Last().IsLastInCountry = false;

            return result;
        }
    }
}
