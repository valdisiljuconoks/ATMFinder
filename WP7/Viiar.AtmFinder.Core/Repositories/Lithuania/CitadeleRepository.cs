using System.Collections.Generic;
using Viiar.AtmFinder.Contracts;

namespace Viiar.AtmFinder.Core.Repositories.Lithuania
{
    [Repository(Country = CountryConstants.Lithuania, ChainName = EntityChainConstants.Citadele)]
    public class CitadeleRepository : BaseRepository
    {
        protected override IEnumerable<Entity> RetrieveEntitiesList()
        {
            return new List<Entity>();
        }
    }
}
