using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Repositories.Estonia;

namespace Viiar.AtmFinder.Tests.Estonia
{
    [TestClass]
    public class SwedbankRepositoryTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<SwedbankRepositoryResourceFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void TestRepository()
        {
            var target = new SwedbankRepository();
            var actual = target.RetrieveEntities().ToList();

            Assert.AreEqual(true, actual.Any());

            var first = actual.First();

            Assert.AreEqual(EntityType.Branch, first.EntityType);
        }
    }

    public class SwedbankRepositoryResourceFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            return
                @"[
{'name': 'KALLAVERE', 'type': 'b', 'address': 'NURGA 3, 74113 MAARDU, Harjumaa', 'region': 'Harjumaa', 'lat': '59.4863061', 'lng': '25.0198758', 'description': 'E-R 10.00-18.00'},
{'name': 'KEILA', 'type': 'b', 'address': 'KESKVÄLJAK 10, 76607 KEILA, Harjumaa', 'region': 'Harjumaa', 'lat': '59.3084482', 'lng': '24.4242336', 'description': 'E-R 10.00-18.00\r\nL 10.00-14.00'}
]";
        }
    }
}
