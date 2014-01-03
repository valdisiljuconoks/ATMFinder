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
    public class SebRepositoryExportTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<SebRepositoryResourceLocalFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void Export_Test()
        {
            var target = new SebRepository();
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
           ,'https://public.sn2.livefilestore.com/y1p5_YspPNoGAtTPJ-2nRfewaaE0l482ZKz1tYcBg62O2c31rAYY7NMedeoQquUp3o9lAQ3OIdrA4VzpxRbrwrTTg/seb-details.png'
           ,'https://public.sn2.livefilestore.com/y1pwxXFOcT0JPfDoU5qzYfW6vK4Sn2lHa3dcQKRjzbSXw1yp306f1x9pErty2Y1lek5LZf4B1Er0ikdSl4tfOePYg/seb-list.png'
           ,'https://public.sn2.livefilestore.com/y1p5_YspPNoGAucTgeioIPa1vYjUa5ofpGLj8CunW-9mTtdQF1yUXC9_HAWpL5DX0MpRZEW2Mxz8W8spWyDQuQyzg/seb-map.png')");
            }
        }
    }

    public class SebRepositoryResourceLocalFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            using (var fs = File.OpenRead(@"C:\Sources\AtmFinder\Dev\Viiar.AtmFinder\Viiar.AtmFinder.UI\Data\Lithuania\seb.json"))
            {
                return new StreamReader(fs).ReadToEnd();
            }
        }
    }
}