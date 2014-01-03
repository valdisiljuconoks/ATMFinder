using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Repositories.Latvia;

namespace Viiar.AtmFinder.Tests.Latvia
{
    [TestClass]
    public class DnbRepositoryTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<DnbRepositoryResourceFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void GetEntitiesListTest()
        {
            var target = new DnbRepository();
            var actual = target.RetrieveEntities().ToList();

            Assert.AreEqual(true, actual.Any());
            var first = actual.First();

            Assert.AreEqual(56.9350394646, first.Latitude);
            Assert.AreEqual(24.0711616442, first.Longitude);
        }
    }

    public class DnbRepositoryResourceFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            return
                @"[{'id':'5','name':'Fili\u0101le \u201e\u0100genskalns\u201d','city':'R\u012bga','address':'M\u0101rupes iela 2','description':'Darba laiks: <br \/>\r\nI-V 9:00 \u2013 19:00<br \/>\r\n<br \/>\r\nT\u0101lrunis:1880','countryId':'1','markerTypeId':'5','lat':'56.9350394646','lng':'24.0711616442','zoom':'17','guid':'','country':{'id':'1','name':'Latvia'},'accessHours':[],'available':1}]";
        }
    }
}
