using System.Collections.Generic;
using Viiar.AtmFinder.Contracts;

namespace Viiar.AtmFinder.Core.Repositories.Latvia
{
    [Repository(Country = CountryConstants.Latvia, ChainName = EntityChainConstants.GEMoney)]
    public class GEMoneyRepository : BaseRepository
    {
        protected override IEnumerable<Entity> RetrieveEntitiesList()
        {
            return new List<Entity>();
        }
    }
}
