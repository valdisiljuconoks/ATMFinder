using System;
using System.Collections.Generic;
using System.Globalization;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Extensions;

namespace Viiar.AtmFinder.Core.Repositories
{
    public abstract class BaseRepository
    {
        private readonly string DataFileRootPath = "/Viiar.AtmFinder.UI;component/data";
        protected string ChainCode { get; set; }
        protected string ChainName { get; set; }
        protected CountryRepositoryBase CountryRepository { get; set; }

        protected Uri DataFilePath
        {
            get
            {
                var path = string.Format(CultureInfo.InvariantCulture,
                                         "{0}/{1}/{2}.json",
                                         this.DataFileRootPath,
                                         CountryRepository.Name,
                                         ChainCode);

                return new Uri(path, UriKind.Relative);
            }
        }

        protected IResourceFileReader ResourceFileReader
        {
            get { return DependencyContainer.Get<IResourceFileReader>(); }
        }

        public IEnumerable<Entity> RetrieveEntities()
        {
            var attribute = RepositoryAttribute.ExtractFromType(GetType());

            CountryRepository = CountryRepositoryFactory.GetByName(attribute.Country);

            // set protected properties used by child classes
            ChainName = attribute.ChainName;
            ChainCode = CountryRepository.ResolveBankCode(ChainName);
            var result = RetrieveEntitiesList();

            return result.ForEach(e => e.ChainCode = ChainCode);
        }

        protected abstract IEnumerable<Entity> RetrieveEntitiesList();
    }
}
