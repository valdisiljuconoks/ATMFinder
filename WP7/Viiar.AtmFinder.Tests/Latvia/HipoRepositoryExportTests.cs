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
    public class HipoRepositoryExportTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<HipoRepositoryResourceLocalFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void Export_Test()
        {
            var target = new HipoRepository();
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
           ,'https://public.sn2.livefilestore.com/y1pFcIMhQCmK7YLdMEet1InyeC7uD212kekaifQRbFNz7SDgHxSELx0fdI-qzWBUqmSse86uNrsZNLcwga2s5RILg/hipo-details.png'
           ,'https://public.sn2.livefilestore.com/y1pFcIMhQCmK7bShjYRzDSU74ayBQFzzsifaeq4GljEzyQuWfvQ-0nAQjL30M6tyZKowv7fsqWeQV2Wrb0DVSMnhQ/hipo-list.png'
           ,'https://public.sn2.livefilestore.com/y1pFcIMhQCmK7aIagFfBSOj-M90VNtccKCnzi_eIn4O9gE9m-pbozNXh5D9xid2Mxr6TJdekXP8NpJ3anyy1qITXA/hipo-map.png')");
            }
        }
    }

    public class HipoRepositoryResourceLocalFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            using (var fs = File.OpenRead(@"C:\Sources\AtmFinder\Dev\Viiar.AtmFinder\Viiar.AtmFinder.UI\Data\Latvia\hipo.json"))
            {
                return new StreamReader(fs).ReadToEnd();
            }
        }
    }
}
