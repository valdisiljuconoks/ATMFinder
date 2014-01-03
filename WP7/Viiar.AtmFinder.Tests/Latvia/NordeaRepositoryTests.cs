using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Repositories.Latvia;

namespace Viiar.AtmFinder.Tests.Latvia
{
    [TestClass]
    public class NordeaRepositoryTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<NordeaRepositoryResourceFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void GetEntitiesListTest()
        {
            var target = new NordeaRepository();
            var actual = target.RetrieveEntities().ToList();

            Assert.AreEqual(true, actual.Any());
            var first = actual.First();

            Assert.AreEqual(56.966957, first.Latitude);
            Assert.AreEqual(24.130421, first.Longitude);
        }
    }

    public class NordeaRepositoryResourceFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            return
                "[{id:'A',fid:'00048bf9cc78cc44e2e42',latlng:{lat:56.966957,lng:24.130421},image:'http://demo.digibrand.lv/helmuts/nordea/nordea-kac.jpg',ext:{width:20,height:20,shadow:'http://',shadow_width:37,shadow_height:20,mask:false},drg:false,laddr:'Nordea Klientu apkalpošanas centrs @56.966957,24.130421',geocode:'',sxti:'Nordea Klientu apkalpošanas centrs',name:'Nordea Klientu apkalpošanas centrs',description:'\x3cdiv dir\x3d\x22ltr\x22\x3e\x3cfont size\x3d\x222\x22\x3eNordea Latvijas galvenais birojs\x3c/font\x3e\x3cfont size\x3d\x222\x22 style\x3d\x22font-weight:bold\x22\x3e\x3c/font\x3e\x3cfont size\x3d\x222\x22\x3e\x3cspan style\x3d\x22font-weight:bold\x22\x3e \x3cbr\x3eK. Valdemara 62\x3c/span\x3e\x3cbr\x3e\x3c/font\x3e\x3cfont size\x3d\x222\x22\x3eRiga \x3cbr\x3eP - P: 9:00 - 17:00 \x3cbr\x3e\x3c/font\x3e\x3cfont size\x3d\x222\x22\x3e\x3ca href\x3d\x22http://www.nordea.lv/\x22\x3ewww.nordea.lv\x3c/a\x3e\x3c/font\x3e\x3c/div\x3e',infoWindow:{title:'Nordea Klientu apkalpošanas centrs',basics:'\x3cdiv transclude\x3d\x22iw\x22\x3e\x3c/div\x3e',dscr:'\x3cdiv dir\x3d\x22ltr\x22\x3e\x3cfont size\x3d\x222\x22\x3eNordea Latvijas galvenais birojs\x3c/font\x3e\x3cfont size\x3d\x222\x22 style\x3d\x22font-weight:bold\x22\x3e\x3c/font\x3e\x3cfont size\x3d\x222\x22\x3e\x3cspan style\x3d\x22font-weight:bold\x22\x3e \x3cbr\x3eK. Valdemara 62\x3c/span\x3e\x3cbr\x3e\x3c/font\x3e\x3cfont size\x3d\x222\x22\x3eRiga \x3cbr\x3eP - P: 9:00 - 17:00 \x3cbr\x3e\x3c/font\x3e\x3cfont size\x3d\x222\x22\x3e\x3ca href\x3d\x22http://www.nordea.lv/\x22\x3ewww.nordea.lv\x3c/a\x3e\x3c/font\x3e\x3c/div\x3e',dscr_dir:'ltr'},ss:{edit:false,detailseditable:false,deleted:false,rapenabled:false,mmenabled:false,usecombinedpeppy:false},b_s:0,elms:[3,2,6,1,12]}]";
        }
    }
}
