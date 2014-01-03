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
    public class DnbRepositoryExportTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<DnbRepositoryResourceLocalFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void Export_Test()
        {
            var target = new DnbRepository();
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
           ,'https://public.sn2.livefilestore.com/y1pBzdndwM1x0HzhDErY4at1fVpYbkf4j9-bjg2-QCOerty9hguR0lbY_mvf7CyUmzVXrpDPLqvhuNYvxGda0pHZw/dnb-details.png'
           ,'https://public.sn2.livefilestore.com/y1pBzdndwM1x0GFXENvbJgy7sy9TKx3FsUz5kogF-alJsj6QfLJAustVOO_8rC7USL_mepleUIft-fm8pyhwtwISw/dnb-list.png'
           ,'https://public.sn2.livefilestore.com/y1pBzdndwM1x0GDi7m2DqoqZitfUNJfqCGFpvtaV14f_J4blQtw3ZLr_ZgKXXs-AEI9cY81IOXiIZsKjqhBvb8A-w/dnb-map.png')");
            }
        }
    }

    public class DnbRepositoryResourceLocalFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            using (var fs = File.OpenRead(@"C:\Sources\AtmFinder\Dev\Viiar.AtmFinder\Viiar.AtmFinder.UI\Data\Latvia\dnb.json"))
            {
                return new StreamReader(fs).ReadToEnd();
            }
        }
    }
}
