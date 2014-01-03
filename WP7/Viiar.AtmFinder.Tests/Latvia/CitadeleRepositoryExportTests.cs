using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Repositories.Latvia;

namespace Viiar.AtmFinder.Tests.Latvia
{
    [TestClass]
    public class CitadeleRepositoryExportTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<CitadeleRepositoryResourceLocalFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void Export_Test()
        {
            var target = new CitadeleRepository();
            var list = target.RetrieveEntities().ToList();

            foreach (var entity in list)
            {
                Console.WriteLine(
                    @"
INSERT INTO [dbo].[Entity]
           ([Id]
           ,[Title]
           ,[Address]
           ,[City]
           ,[Country]
           ,[Latitude]
           ,[Longitude]
           ,[Description]
           ,[Branch]
           ,[CashOut]
           ,[CashIn]
           ,[Chain]
           ,[Icon]
           ,[IconSmall]
           ,[IconMap])
     VALUES
           ('" + entity.Id.ToString() + @"'
           ,N'" + entity.Title + @"'
           ,N'" + entity.Address + @"'
           ,N'" + entity.City + @"'
           ,N'" + entity.Country + @"'
           ," + entity.Latitude + @"
           ," + entity.Longitude + @"
           ,N'" + entity.Description + @"'
           ,'" + (entity.EntityType == EntityType.Branch ? "True" : "False") + @"'
           ,'" + ((entity.CashDirection & CashDirection.Out) == CashDirection.Out ? "True" : "False") + @"'
           ,'" + ((entity.CashDirection & CashDirection.In) == CashDirection.In ? "True" : "False") + @"'
           ,'" + entity.ChainCode + @"'" + @"
           ,'https://public.sn2.livefilestore.com/y1ptbQHQWhWNkIW1dBHV980Oyaz69HQzOqTeY2j2DqoyALQ4UtojDytv3o2i0BynvSkyRDVOa5yBU9cmoNqIb0DUw/citadele-details.png'
           ,'https://public.sn2.livefilestore.com/y1ptbQHQWhWNkIBeD9b2lev9YclpyLx0Xxx1eKHW6dvj8JokCi4DbhEROvtsK-Sd0xD3vcbrf0BhdMd1Ps37ZJJVg/citadele-list.png'
           ,'https://public.sn2.livefilestore.com/y1ptbQHQWhWNkKUpzHI7Mfd58sE2jpdftHdyyRAeQkSC0ZFQn91LreL-KqLVVZgdM0RDSJ8ka0fgr9bJnJ_Y3jbog/citadele-map.png')");
            }
        }
    }

    public class CitadeleRepositoryResourceLocalFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            using (var fs = File.OpenRead(@"C:\Sources\AtmFinder\Dev\Viiar.AtmFinder\Viiar.AtmFinder.UI\Data\Latvia\citadele.json"))
            {
                return new StreamReader(fs).ReadToEnd();
            }
        }
    }
}
