﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json.Linq;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core.Extensions;

namespace Viiar.AtmFinder.Core.Repositories.Latvia
{
    [Repository(Country = CountryConstants.Latvia, ChainName = EntityChainConstants.Citadele)]
    public class CitadeleRepository : BaseRepository
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
                var coordinates = model["latlng"];
                var latitude = 0.0;
                var longitude = 0.0;
                if (coordinates != null)
                {
                    latitude = coordinates["lat"].SafeValue(0.0);
                    longitude = coordinates["lng"].SafeValue(0.0);
                }

                var entity = new Entity(CountryRepository.Name, ChainCode)
                                 {
                                     Id = Guid.NewGuid(),
                                     Title = model["name"].SafeValue("Unknown"),
                                     Address = model["name"].SafeValue("Unknown"),
                                     Latitude = latitude,
                                     Longitude = longitude,
                                     Description = model["description"].SafeValue(string.Empty),
                                     CashDirection = CashDirection.Out,
                                     EntityType = EntityType.Atm
                                 };

                var titleLowerCase = entity.Title.ToLower();
                var descLowerCase = entity.Description.ToLower();

                if (titleLowerCase.Contains("centrs"))
                {
                    entity.CashDirection = CashDirection.In | CashDirection.Out;
                    entity.EntityType = EntityType.Branch;
                }

                if (descLowerCase.Contains("iemaksa"))
                {
                    entity.CashDirection = entity.CashDirection | CashDirection.Out;
                }

                result.Add(entity);
            }

            return result;
        }
    }
}
