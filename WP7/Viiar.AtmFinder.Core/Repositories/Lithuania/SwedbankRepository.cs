﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Extensions;

namespace Viiar.AtmFinder.Core.Repositories.Lithuania
{
    [Repository(Country = CountryConstants.Lithuania, ChainName = EntityChainConstants.Swedbank)]
    public class SwedbankRepository : BaseRepository
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
                        EntityType = EntityType.Atm
                    };

                var cashDirection = model["type"].SafeValue(string.Empty);

                if (cashDirection.Contains("f"))
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
