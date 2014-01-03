using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Extensions;

namespace Viiar.AtmFinder.Core.Repositories.Latvia
{
    [Repository(Country = CountryConstants.Latvia, ChainName = EntityChainConstants.Dnb)]
    public class DnbRepository : BaseRepository
    {
        protected override IEnumerable<Entity> RetrieveEntitiesList()
        {
            var result = new List<Entity>();
            var content = ResourceFileReader.ReadToEnd(DataFilePath);
            if (string.IsNullOrEmpty(content))
            {
                return result;
            }

            var jsonModel = JArray.Parse(content);
            foreach (var model in jsonModel)
            {
                var entity = new Entity(CountryRepository.Name, ChainCode)
                                 {
                                     Id = Guid.NewGuid(),
                                     Title = model["name"].SafeValue("Unknown"),
                                     Address = model["address"].SafeValue("Unknown"),
                                     City = model["city"].SafeValue(string.Empty),
                                     Latitude = model["lat"].SafeValue(0.0),
                                     Longitude = model["lng"].SafeValue(0.0),
                                     Description = model["description"].SafeValue(string.Empty),
                                     CashDirection = CashDirection.Out,
                                     EntityType = EntityType.Atm
                                 };

                var marker = model["markerTypeId"].SafeValue(0);
                switch (marker)
                {
                    case 2:
                        entity.CashDirection = CashDirection.Out;
                        break;
                    case 5:
                        entity.CashDirection = CashDirection.Out | CashDirection.In;
                        entity.EntityType = EntityType.Branch;
                        break;
                    case 6:
                        entity.CashDirection = CashDirection.Out | CashDirection.In;
                        break;
                }

                result.Add(entity);
            }

            return result;
        }
    }
}
