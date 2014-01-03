using System.Collections.Generic;

namespace Viiar.AtmFinder.Core.Repositories.Latvia
{
    internal class LatviaCountryRepository : CountryRepositoryBase
    {
        private static readonly List<BaseRepository> repositories = new List<BaseRepository>
                                                                        {
                                                                            new SwedbankRepository(),
                                                                            new NordeaRepository(),
                                                                            new DnbRepository(),
                                                                            new SebRepository(),
                                                                            new CitadeleRepository(),
                                                                            new HipoRepository(),
                                                                            new GEMoneyRepository()
                                                                        };

        public override string Name
        {
            get { return CountryConstants.Latvia; }
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
