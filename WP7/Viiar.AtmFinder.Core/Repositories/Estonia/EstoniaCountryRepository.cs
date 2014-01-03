using System.Collections.Generic;

namespace Viiar.AtmFinder.Core.Repositories.Estonia
{
    internal class EstoniaCountryRepository : CountryRepositoryBase
    {
        private static readonly List<BaseRepository> repositories = new List<BaseRepository>
                                                                        {
                                                                            new SwedbankRepository(),
                                                                            new NordeaRepository(),
                                                                            new SebRepository(),
                                                                            new SampoRepository()
                                                                        };

        public override string Name
        {
            get { return CountryConstants.Estonia; }
        }

        public override IEnumerable<BaseRepository> Repositories
        {
            get { return repositories; }
        }

        public override string ResolveBankCode(string chain)
        {
            return EntityChainConstants.GetCode(chain);
        }

        public override string ResolveBankName(string chainCode)
        {
            return EntityChainConstants.GetName(chainCode);
        }
    }
}
