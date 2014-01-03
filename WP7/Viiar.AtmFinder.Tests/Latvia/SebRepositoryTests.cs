using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Repositories.Latvia;

namespace Viiar.AtmFinder.Tests.Latvia
{
    [TestClass]
    public class SebRepositoryTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<SebRepositoryResourceFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void TestRepository()
        {
            var target = new SebRepository();
            var actual = target.RetrieveEntities().ToList();

            Assert.AreEqual(true, actual.Any());

            var first = actual.First();

            Assert.AreEqual(EntityType.Atm, first.EntityType);
            Assert.AreEqual(CashDirection.Out, first.CashDirection & CashDirection.Out);
        }
    }

    public class SebRepositoryResourceFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            return
                @"
[{'name': 'Veikals ""Elvi""', 'city': 'Adaži', 'type': 'InOut', 'lat': '57.0759872', 'lng': '24.319554', 'address': 'Rigas gatve 51'},
{'name': 'T/C ""Stockmann""', 'city': 'Riga', 'type': 'Out', 'lat': '56.9459092', 'lng': '24.1135778', 'address': '13. janvara iela 8'}]";
        }
    }
}
