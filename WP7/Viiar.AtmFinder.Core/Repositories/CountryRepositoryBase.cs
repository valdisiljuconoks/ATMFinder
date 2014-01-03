using System.Collections.Generic;

namespace Viiar.AtmFinder.Core.Repositories
{
    public abstract class CountryRepositoryBase
    {
        private static readonly object syncRoot = new object();
        private static CountryRepositoryBase currentCountry;
        private List<string> cachedBankList;

        public static CountryRepositoryBase Current
        {
            get { return currentCountry; }

            internal set
            {
                lock (syncRoot)
                {
                    currentCountry = value;
                }
            }
        }

        public abstract string Name { get; }

        public abstract IEnumerable<BaseRepository> Repositories { get; }

        public abstract string ResolveBankCode(string chain);

        public abstract string ResolveBankName(string chainCode);

        public List<string> RetrieveSupportedChains()
        {
            if (this.cachedBankList == null)
            {
                this.cachedBankList = new List<string>();

                foreach (var repository in Repositories)
                {
                    var attribute = RepositoryAttribute.ExtractFromType(repository.GetType());
                    this.cachedBankList.Add(attribute.ChainName);
                }
            }

            return this.cachedBankList;
        }
    }
}
