using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Extensions;

namespace Viiar.AtmFinder.Core.Repositories.Latvia
{
    [Repository(Country = CountryConstants.Latvia, ChainName = EntityChainConstants.Seb)]
    public class SebRepository : BaseRepository
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
                        City = model["city"].SafeValue("Unknown"), 
                        Latitude = model["lat"].SafeValue(0.0), 
                        Longitude = model["lng"].SafeValue(0.0), 
                        Description = model["description"].SafeValue(string.Empty), 
                        CashDirection = CashDirection.Out, 
                        EntityType = EntityType.Atm
                    };

                if (!string.IsNullOrEmpty(entity.City))
                {
                    entity.Address += ", " + entity.City;
                }

                var type = model["type"].SafeValue(string.Empty).ToLower();
                if (type.Contains("in"))
                {
                    entity.CashDirection = entity.CashDirection | CashDirection.In;
                }

                if (type.Contains("out"))
                {
                    entity.CashDirection = entity.CashDirection | CashDirection.Out;
                }

                if (type.Contains("office"))
                {
                    entity.CashDirection = CashDirection.In | CashDirection.Out;
                    entity.EntityType = EntityType.Branch;
                }

                var nameLowerCase = entity.Title.ToLower();
                if (nameLowerCase.Contains("filiale"))
                {
                    entity.EntityType = EntityType.Branch;
                }

                result.Add(entity);
            }

            return result;
        }
    }
}
