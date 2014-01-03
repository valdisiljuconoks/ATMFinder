using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Repositories.Latvia;

namespace Viiar.AtmFinder.Tests.Latvia
{
    [TestClass]
    public class SwedbankRepositoryTest
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
        public void GetEntitiesListTest()
        {
            SwedbankRepository target = new SwedbankRepository();
            var actual = target.RetrieveEntities().ToList();

            Assert.AreEqual(true, actual.Any());

            var first = actual.First();

            Assert.AreEqual("Daugavpils", first.City);
            Assert.AreEqual(55.8704, first.Latitude);
            Assert.AreEqual(CashDirection.In, first.CashDirection & CashDirection.In);
            Assert.AreEqual(CashDirection.Out, first.CashDirection & CashDirection.Out);
        }
    }

    public class SwedbankRepositoryResourceFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            return
                @"[
{'type':'fcico','id':['53','496','491','700'],'name':'Daugavpils fili\u0101le,Izmaksas autom\u0101ts,Iemaksas autom\u0101ts','address':'Daugavpils, R\u012bgas iela 22a,\r\nt\u0101lr. 67444444\r\nfakss 67448604','city':'Daugavpils','lat':'55.8704','lng':'26.5151','description':'<br \/>Daugavpils fili\u0101le<\/b> - darbdien\u0101s 9:00-17:00<br \/>Izmaksas autom\u0101ts - neierobe\u017eots darba laiks
Iemaksas autom\u0101ts - darbdien\u0101s 9:00-17:00
Daugavpils, R\u012bgas iela 22a,\r\nt\u0101lr. 67444444\r\nfakss 67448604
'},{'type':'fco','id':['89','627'],'name':'Daugavpils fili\u0101le \'Dimants\',Izmaksas autom\u0101ts','address':'Daugavpils, Sak\u0146u iela 15, \r\nt\u0101lr. 67444444\r\nfakss 67448735','city':'Daugavpils','lat':'55.8739','lng':'26.5216','description':'
Daugavpils fili\u0101le \'Dimants\' - darbdien\u0101s 9:00-18:00\r\nsestdien\u0101s 10:00-14:00
Izmaksas autom\u0101ts - neierobe\u017eots darba laiks
Daugavpils, Sak\u0146u iela 15, \r\nt\u0101lr. 67444444\r\nfakss 67448735'}
]";
        }
    }
}
