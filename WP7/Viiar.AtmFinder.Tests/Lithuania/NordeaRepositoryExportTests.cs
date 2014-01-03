using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Repositories.Lithuania;

namespace Viiar.AtmFinder.Tests.Lithuania
{
    [TestClass]
    public class NordeaRepositoryExportTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<NordeaRepositoryResourceLocalFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void Export_Test()
        {
            var target = new NordeaRepository();
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
           ,'https://public.sn2.livefilestore.com/y1p-Wv1vAZMCNlyhoibnxQLi2g7kvGXTb25yXpKErEIDsWKzG5RnRE4PvK9jERocb7v5ISWaZNu0M5z2JohJPX1pQ/nordea-details.png'
           ,'https://public.sn2.livefilestore.com/y1p-Wv1vAZMCNk3tK1EVByBKIbc-4eqbkI71NoEp4O06cx8gcNieR3FKCX3fru75ckfJhumG7YBGjiXbmuhr7Qneg/nordea-list.png'
           ,'https://public.sn2.livefilestore.com/y1pbEycgppU-exLvFV_kROKHR2qCZROYNBlV4gK2oVwznxVUt9HCL0Fj8Cuy6lYWK5HlbkWzQWeA1_zVzRHgxOpEg/nordea-map.png')");
            }
        }
    }

    public class NordeaRepositoryResourceLocalFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            using (var fs = File.OpenRead(@"C:\Sources\AtmFinder\Dev\Viiar.AtmFinder\Viiar.AtmFinder.UI\Data\Lithuania\nordea.json"))
            {
                return new StreamReader(fs).ReadToEnd();
            }
        }
    }
}