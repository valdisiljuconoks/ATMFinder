using System;
using System.Collections.Generic;
using System.Linq;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Repositories;

namespace Viiar.AtmFinder.Core.Extensions
{
    public static class IEnumerableOfEntityExtensions
    {
        public static IEnumerable<Entity> FilterOnlyMyBanks(this IEnumerable<Entity> list, List<string> myBanks)
        {
            if (myBanks == null)
            {
                return list;
            }

            if (!myBanks.Any())
            {
                return list;
            }

            var banks = RepositoryFactory.DecodeFromSettings(myBanks);
            return list.Where(e => banks.Any(b => e.Chain.Equals(b.Code, StringComparison.OrdinalIgnoreCase)
                                                  && e.Country.Equals(b.Country, StringComparison.OrdinalIgnoreCase))).ToList();
        }
    }
}
