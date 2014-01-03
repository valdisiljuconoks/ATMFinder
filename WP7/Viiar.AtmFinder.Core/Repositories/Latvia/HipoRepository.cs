using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Extensions;

namespace Viiar.AtmFinder.Core.Repositories.Latvia
{
    [Repository(Country = CountryConstants.Latvia, ChainName = EntityChainConstants.Hipo)]
    public class HipoRepository : BaseRepository
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
                        Address = string.Format(
                                                CultureInfo.InvariantCulture, 
                                                "{0}, {1}", 
                                                model["name"].SafeValue("Unknown"), 
                                                model["city"].SafeValue("Unknown")), 
                        Latitude = model["lat"].SafeValue(0.0), 
                        Longitude = model["lng"].SafeValue(0.0), 
                        Description = model["description"].SafeValue(string.Empty), 
                        CashDirection = CashDirection.Out, 
                        EntityType = EntityType.Atm
                    };

                var descLowerCase = entity.Description.ToLower();
                if (descLowerCase.Contains("filiale"))
                {
                    entity.CashDirection = CashDirection.In | CashDirection.Out;
                    entity.EntityType = EntityType.Branch;
                }

                result.Add(entity);
            }

            return result;
        }
    }
}
