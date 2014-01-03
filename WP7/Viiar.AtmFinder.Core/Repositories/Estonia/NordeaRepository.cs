using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Extensions;

namespace Viiar.AtmFinder.Core.Repositories.Estonia
{
    [Repository(Country = CountryConstants.Estonia, ChainName = EntityChainConstants.Nordea)]
    public class NordeaRepository : BaseRepository
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
                        Title = model["l_location"].SafeValue("Unknown"), 
                        Address = model["l_address"].SafeValue("Unknown"), 
                        Description = model["description"].SafeValue(string.Empty), 
                        Region = model["city"].SafeValue(string.Empty), 
                        Latitude = model["lat"].SafeValue(0.0), 
                        Longitude = model["lng"].SafeValue(0.0), 
                        EntityType = EntityType.Atm, 
                        CashDirection = CashDirection.Out
                    };

                if (string.IsNullOrEmpty(entity.Description))
                {
                    entity.Description = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", entity.Address, entity.Region);
                }

                var cashDirection = model["type"].SafeValue(string.Empty);
                if (cashDirection.Contains("b"))
                {
                    entity.EntityType = EntityType.Branch;
                }

                if (cashDirection.Contains("ci"))
                {
                    entity.CashDirection = entity.CashDirection | CashDirection.In;
                }

                if (cashDirection.Contains("co"))
                {
                    entity.CashDirection = entity.CashDirection | CashDirection.Out;
                }

                result.Add(entity);
            }

            return result;
        }
    }
}
