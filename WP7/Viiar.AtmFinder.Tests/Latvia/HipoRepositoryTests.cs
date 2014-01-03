﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Viiar.AtmFinder.Core;
using Viiar.AtmFinder.Core.Repositories.Latvia;

namespace Viiar.AtmFinder.Tests.Latvia
{
    [TestClass]
    public class HipoRepositoryTests
    {
        [ClassInitialize]
        public static void InjectDependencies(TestContext context)
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<HipoRepositoryResourceFileReader>());
        }

        [ClassCleanup]
        public static void UnloadContainer()
        {
            DependencyContainer.Uninitialize(k => k.Unbind<IResourceFileReader>());
        }

        [TestMethod]
        public void TestHipo()
        {
            var target = new HipoRepository();
            var actual = target.RetrieveEntities().ToList();

            Assert.AreEqual(true, actual.Any());
        }
    }

    public class HipoRepositoryResourceFileReader : IResourceFileReader
    {
        public string ReadToEnd(Uri uri)
        {
            return
                @"
[{'name': 'Melnsila iela 22', 'city': 'Rīga', 'lat': '56.9451018', 'lng': '24.0648548', 'description': 'Hipoteku bankas filiale'},{'name': 'Elizabetes iela 41/43', 'city': 'Rīga', 'lat': '56.9571374', 'lng': '24.112449', 'description': 'Hipoteku bankas filiale'},{'name': 'Kr.Valdemara iela 62', 'city': 'Rīga', 'lat': '56.9664162', 'lng': '24.1297915', 'description': 'T/c ""Optima""'},{'name': 'Pragas iela 1', 'city': 'Rīga', 'lat': '56.9442442', 'lng': '24.1163975', 'description': 'Autoosta'},{'name': 'Daugavgrivas iela 68', 'city': 'Rīga', 'lat': '56.9601549', 'lng': '24.0704069', 'description': 'Veikals ""1000 skruves""'},{'name': 'Lielirbes iela 29', 'city': 'Rīga', 'lat': '56.9304932', 'lng': '24.0345635', 'description': 'T/c ""Spice""'},{'name': 'Maskavas iela 240', 'city': 'Rīga', 'lat': '56.9209717', 'lng': '24.1709796', 'description': 'Hipoteku bankas filiale'},{'name': 'Doma laukums 4', 'city': 'Rīga', 'lat': '56.9495081', 'lng': '24.1044849', 'description': 'Hipoteku bankas filiale'},{'name': 'Brivibas iela 226', 'city': 'Rīga', 'lat': '56.9691328', 'lng': '24.1534032', 'description': 'Hipoteku bankas filiale'},{'name': 'Viestura iela 2', 'city': 'Daugavpils', 'lat': '55.8686639', 'lng': '26.5272196', 'description': 'Hipoteku bankas filiale'},{'name': 'Liela iela 12', 'city': 'Jelgava', 'lat': '56.6515830', 'lng': '23.7218289', 'description': 'Hipoteku bankas filiale'},{'name': 'Brivibas iela 116', 'city': 'Jekabpils', 'lat': '56.4955492', 'lng': '25.8695197', 'description': 'Hipoteku bankas filiale'},{'name': 'Ausekliša iela 2', 'city': 'Jekabpils', 'lat': '56.4940986', 'lng': '25.8870659', 'description': 'Veikals ""Beta""'},{'name': 'Liela iela 12', 'city': 'Liepaja', 'lat': '56.5096096', 'lng': '21.0121772', 'description': 'Hipoteku bankas filiale'},{'name': 'Liela iela 13', 'city': 'Liepaja', 'lat': '56.5075909', 'lng': '21.010373', 'description': 'T/n ""Kurzeme""'},{'name': 'Atbrivošanas aleja 119', 'city': 'Rezekne', 'lat': '56.5146876', 'lng': '27.3356908', 'description': 'Hipoteku bankas filiale'},{'name': 'Galdnieku iela 8', 'city': 'Rezekne', 'lat': '56.5020989', 'lng': '27.3302101', 'description': 'Veikals ""RIMI""'},{'name': 'Rīgas iela 22', 'city': 'Rezekne', 'lat': '56.5138224', 'lng': '27.3239697', 'description': 'Rezeknes galas kombinata'},{'name': 'G.Apina iela 10a', 'city': 'Valmiera', 'lat': '57.5413766', 'lng': '25.4163588', 'description': 'Veikals ""Maxima X""'},{'name': 'Rīgas iela 9', 'city': 'Valmiera', 'lat': '57.5381728', 'lng': '25.4214605', 'description': 'Hipoteku bankas filiale'},{'name': 'Andreja iela 6', 'city': 'Ventspils', 'lat': '57.3919774', 'lng': '21.5662512', 'description': 'Hipoteku bankas filiale'},{'name': 'Celtnieku iela 20', 'city': 'Ventspils', 'lat': '57.4064647', 'lng': '21.602766', 'description': 'Veikals ""IKI""'},{'name': 'Spidolas iela 11', 'city': 'Aizkraukle', 'lat': '56.6009756', 'lng': '25.2521961', 'description': 'Hipoteku bankas filiale'},{'name': 'Abolini', 'city': 'Aizkraukle', 'lat': '56.6043878', 'lng': '25.2555089', 'description': 'T/c ""IGA centrs""'},{'name': 'Pils iela 21', 'city': 'Aluksne', 'lat': '57.4247297', 'lng': '27.0463545', 'description': 'Hipoteku bankas filiale'},{'name': 'Gaujas iela 4/6', 'city': 'Adaži', 'lat': '57.0723687', 'lng': '24.3270755', 'description': 'Veikals ""Supernetto""'},{'name': 'Berzpils iela 2', 'city': 'Balvi', 'lat': '57.1319750', 'lng': '27.2680275', 'description': 'Hipoteku bankas filiale'},{'name': 'Karsavas iela 4', 'city': 'Baltinava', 'lat': '56.9449844', 'lng': '27.6449419', 'description': 'Veikals ""Pietalava""'},{'name': 'Pionieru iela 1/1', 'city': 'Bauska', 'lat': '56.4062844', 'lng': '24.1895537', 'description': 'Hipoteku bankas filiale'},{'name': 'Valnu iela 3', 'city': 'Cesis', 'lat': '57.3109402', 'lng': '25.2718819', 'description': 'Hipoteku bankas filiale'},{'name': 'Brivibas iela 14', 'city': 'Dobele', 'lat': '56.6253086', 'lng': '23.2805768', 'description': 'Hipoteku bankas filiale'},{'name': 'Muldavas iela 3a', 'city': 'Dobele', 'lat': '56.6274616', 'lng': '23.2788645', 'description': 'T/c ""Forums""'},{'name': 'Ausmana iela 5-4', 'city': 'Jaunberze', 'lat': '56.7418847', 'lng': '23.394123', 'description': 'Veikals ""Aibe""'},{'name': 'Rīgas iela 13', 'city': 'Ergli', 'lat': '56.8965867', 'lng': '25.6359637', 'description': 'Hipoteku bankas filiale'},{'name': 'Vitolu iela 12', 'city': 'Grobinas pag.', 'lat': '56.5090905', 'lng': '21.0031995', 'description': 'Veikals ""TEV""'},{'name': 'Rīgas iela 47', 'city': 'Gulbene', 'lat': '57.1731089', 'lng': '26.757383', 'description': 'Hipoteku bankas filiale'},{'name': 'Skolas iela 7', 'city': 'Gulbenes', 'lat': '57.1710684', 'lng': '26.7670775', 'description': 'Veikals ""Maxima""'},{'name': '""Akmens dzirnas""', 'city': 'Jaungulbene', 'lat': '57.0680610', 'lng': '26.5976353', 'description': 'Veikals ""Uguntina""'},{'name': 'Rīgas iela 19', 'city': 'Lejasciems', 'lat': '57.2788969', 'lng': '26.5764558', 'description': 'Veikals ""BAF""'},{'name': '""Akacijas""', 'city': 'Lizums', 'lat': '57.1891606', 'lng': '26.3740229', 'description': 'Pagastmaja'},{'name': 'Gatves iela 5-2', 'city': 'Ranka', 'lat': '57.2133179', 'lng': '26.1854502', 'description': 'Veikals ""Sintija un Co""'},{'name': 'Skolas iela 4', 'city': 'Iecava', 'lat': '56.5981218', 'lng': '24.2021296', 'description': 'Hipoteku bankas filiale'},{'name': 'Melioratoru iela 1a', 'city': 'Ikškile', 'lat': '56.8330637', 'lng': '24.4976772', 'description': '""Supernetto""'},{'name': 'Alejs', 'city': 'Jaunpils', 'lat': '56.7333247', 'lng': '23.0175555', 'description': '-'},{'name': 'Vecbebri', 'city': 'Bebru pagasts', 'lat': '56.7231300', 'lng': '25.488236', 'description': 'DUS Virši-A'},{'name': 'Latgales iela 16', 'city': 'Kraslava', 'lat': '55.8953747', 'lng': '27.1817155', 'description': 'Veikals ""Mego""'},{'name': 'Smilšu iela 24', 'city': 'Kuldiga', 'lat': '56.9694622', 'lng': '21.9618123', 'description': 'Hipoteku bankas filiale'},{'name': 'Ledmane', 'city': 'Ledmane', 'lat': '56.7677408', 'lng': '24.9975787', 'description': '""Tirdzniecibas centrs""'},{'name': 'Baumanu Karla lauk.1', 'city': 'Limbaži', 'lat': '57.5115560', 'lng': '24.7192816', 'description': 'Hipoteku bankas filiale'},{'name': '1.maija iela 5', 'city': 'Ludza', 'lat': '56.5459585', 'lng': '27.725173', 'description': 'Hipoteku bankas filiale'},{'name': 'Brivibas iela 9', 'city': 'Barkava', 'lat': '56.7229202', 'lng': '26.6107875', 'description': 'Barkavas biblioteka'},{'name': 'Poruka iela 2', 'city': 'Madona', 'lat': '56.8511627', 'lng': '26.2170721', 'description': 'Hipoteku bankas filiale'},{'name': 'Rupniecibas iela 49', 'city': 'Madona', 'lat': '56.8596395', 'lng': '26.2296786', 'description': 'Veikals ""Maxima""'},{'name': 'Lidosta ""Rīga""', 'city': 'Marupes pag.', 'lat': '56.922636', 'lng': '23.976974', 'description': '-'},{'name': 'Brivibas iela 22', 'city': 'Ogre', 'lat': '56.8168388', 'lng': '24.6048488', 'description': 'Hipoteku bankas filiale'},{'name': 'Rīgas iela 23', 'city': 'Ogre', 'lat': '56.8159610', 'lng': '24.589532', 'description': 'T/c ""Dauga""'},{'name': 'Brivibas iela 2', 'city': 'Preili', 'lat': '56.2945241', 'lng': '26.7231666', 'description': 'Hipoteku bankas filiale'},{'name': 'Saules iela 8', 'city': 'Riebini', 'lat': '56.2918778', 'lng': '26.7341503', 'description': 'Domes eka'},{'name': '""Zemzari""', 'city': 'Ropaži', 'lat': '56.9739118', 'lng': '24.6313778', 'description': 'Veikals ""Elda""'},{'name': 'Pilsrundale', 'city': 'Pilsrundale', 'lat': '56.4164195', 'lng': '24.0177968', 'description': 'Veikals ""Pilsrundale""'},{'name': 'Vidzemes iela 3a', 'city': 'Salacgriva', 'lat': '57.7548530', 'lng': '24.3608145', 'description': 'Veikals ""Maxima""'},{'name': 'Susejas iela 9', 'city': 'Sala', 'lat': '56.504434', 'lng': '25.763369', 'description': 'Salas novada pašvaldiba'},{'name': 'Apvadcelš 11', 'city': 'Saldus', 'lat': '56.6821404', 'lng': '22.4719715', 'description': 'DUS ""Statoil""'},{'name': 'Jelgavas iela 3', 'city': 'Saldus', 'lat': '56.6670174', 'lng': '22.4953547', 'description': 'Hipoteku bankas filiale'},{'name': 'Baznicas lauk. 11a', 'city': 'Smiltene', 'lat': '57.4246589', 'lng': '25.901641', 'description': 'Hipoteku bankas filiale'},{'name': 'Liela iela 18', 'city': 'Talsi', 'lat': '57.2442901', 'lng': '22.5906218', 'description': 'Hipoteku bankas filiale'},{'name': 'Rīgas iela 8', 'city': 'Talsi', 'lat': '57.2389726', 'lng': '22.5830027', 'description': 'Veikals ""Maxima""'},{'name': 'Brivibas lauk. 10', 'city': 'Tukums', 'lat': '56.9668778', 'lng': '23.1533816', 'description': 'Hipoteku bankas filiale'},{'name': 'Raina iela 12', 'city': 'Valka', 'lat': '57.7752199', 'lng': '26.0206299', 'description': 'Hipoteku bankas filiale'},{'name': 'Rīgas iela 3', 'city': 'Skaistkalne', 'lat': '56.3817280', 'lng': '24.6459522', 'description': 'Veikals ""Vesko""'}]
";
        }
    }
}
