using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Repositories.Estonia;

namespace Viiar.AtmFinder.Tests.Estonia
{
    [TestClass]
    public class SwedbankRepositoryExportTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<SwedbankRepositoryResourceLocalFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void Export_Test()
        {
            var target = new SwedbankRepository();
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
           ,'https://public.sn2.livefilestore.com/y1pKOlQfZIviShgTJZQ1WsivFhOqlRtgVgP1sMNRnzRKTYi6vV1bK17nubHaLCpv9QEJDTYhvnQxyr1aJyhS-EyUQ/swedbank-details.png'
           ,'https://public.sn2.livefilestore.com/y1pKOlQfZIviSiKIebSOvBNvwrHsTYGlpd7IPRKkiGkFxsVKgJZo96K-sjLFhlyh031vNYZpR5ZedUvgH5WM4yTbQ/swedbank-list.png'
           ,'https://public.sn2.livefilestore.com/y1pDfD1fyeUX4qqF2S1W3MQfST3Jnz-sHNCXrC08CE10RahJS1DzYQweIbokavH3CkPyBwsFo-hs2ZV8LZoqjp4bw/swedbank-map.png')");
            }
        }
    }

    public class SwedbankRepositoryResourceLocalFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            using (var fs = File.OpenRead(@"C:\Sources\AtmFinder\Dev\Viiar.AtmFinder\Viiar.AtmFinder.UI\Data\Estonia\swedbank.json"))
            {
                return new StreamReader(fs).ReadToEnd();
            }
        }
    }
}
