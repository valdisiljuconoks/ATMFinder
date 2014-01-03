using System.Collections.Generic;
using Viiar.AtmFinder.Contracts;

namespace Viiar.AtmFinder.Core.Repositories.Estonia
{
    [Repository(Country = CountryConstants.Estonia, ChainName = EntityChainConstants.Sampo)]
    public class SampoRepository : BaseRepository
    {
        protected override IEnumerable<Entity> RetrieveEntitiesList()
        {
            return new List<Entity>();
        }
    }
}
