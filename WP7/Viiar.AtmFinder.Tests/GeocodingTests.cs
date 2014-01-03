using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Viiar.AtmFinder.Tests
{
    [TestClass]
    public class GeocodingTests
    {
        [TestMethod]
        public void GetCoordinates()
        {
            var places = new Dictionary<string, string>
                {

{"227", "Maskavas iela 427b, Rīga, Latvia"},
{"228", "Brīvības gatve 386, Rīga, Latvia"},
{"229", "Ulmaņa gatve 84, Rīga, Latvia"},
{"230", "Lucavsalas iela 1, Rīga, Latvia"},
{"231", "Krasta iela 68, Rīga, Latvia"},
{"232", "Vienības gatve 144, Rīga, Latvia"},
{"233", "Upesgrīvas iela 1, Mārupe, Latvia"},
{"234", "Lubānas iela 64, Rīga, Latvia"},
{"235", "Kuldīgas iela 1, Rīga, Latvia"},
{"236", "Dambja iela 10, Rīga, Latvia"},
{"237", "Jūrmalas gatve 46b, Rīga, Latvia"},
{"238", "Senču iela 2b, Rīga, Latvia"},
{"239", "Lielirbes iela 30, Rīga, Latvia"},
{"240", "Rankas iela 2, Rīga, Latvia"},
{"241", "Brīvības iela 253, Rīga, Latvia"},
{"242", "Dzelzavas iela 3, Rīga, Latvia"},
{"243", "Biķernieku iela 115a, Rīga, Latvia"},
{"244", "A.Deglava iela 51a, Rīga, Latvia"},
{"245", "Dzirnavu iela 127, Rīga, Latvia"},
{"246", "Kaugurciems 83, Jūrmala, Latvia"},
{"247", "Viestura iela 21, Jūrmala, Latvia"},
{"248", "Kurzemes iela 7a, Tukums, Latvia"},
{"249", "Raiņa iela 2, Liepāja, Latvia"},
{"250", "K.Zāles laukums 7, Liepāja, Latvia"},
{"251", "Pulvera iela 10b, Liepāja, Latvia"},
{"252", "Loka maģistrāle 2a, Jelgava, Latvia"},
{"253", "Atmodas iela 21b, Jelgava, Latvia"},
{"254", "Rīgas iela 11a, Ogre, Latvia"},
{"255", "Mazā stacijas iela 14, Valmiera, Latvia"},
{"256", "Rīgas iela 76, Valmiera, Latvia"},
{"257", "Valmieras iela 4, Cēsis, Latvia"},
{"258", "Jāņa Poruka iela 49, Cēsis, Latvia"},
{"259", "Atbrīvošanas aleja 146, Rēzekne, Latvia"},
{"260", "Lāčplēša iela 4, Ventspils, Latvia"},
{"261", "Kurzemes iela 2, Ventspils, Latvia"},
{"262", "Vienības iela 1e, Jēkabpils, Latvia"},
{"263", "Rīgas iela 247, Jēkabpils, Latvia"},
{"264", "\"Krustiņi\", Iecavas pagasts, Latvia"},
{"265", "Rūpniecības iela 47a, Madona, Latvia"},
{"266", "Smilšu iela 32, Daugavpils, Latvia"},
{"267", "Dostojevska iela 6, Daugavpils, Latvia"},
{"268", "Pionieru iela 2, Bauska, Latvia"},
{"269", "Elejas iela 1, Bauska, Latvia"},
{"270", "Vidzemes šoseja 4, Sigulda, Latvia"},
{"271", "Dundagas iela 1, Talsi, Latvia"},
{"272", "Sūru iela 3, Kuldīga, Latvia"},
{"273", "Valdemāra iela 82, Ainaži, Latvia"},
{"274", "\"Apvedceļš\" 5, Saldus, Latvia"},
{"275", "Jelgavas iela 3, Saldus, Latvia"},
{"276", "Brīvības iela 68, Gulbene, Latvia"},
{"277", "Pils iela 9c, Alūksne, Latvia"},
{"278", "Zviedru iela 1c, Salaspils, Latvia"},
{"279", "Rīgas gatve 5a, Ādaži, Latvia"},
{"280", "Rīgas iela 24, Ķekava, Latvia"},

                };

            var rnd = new Random();

            foreach (var place in places)
            {
                var coord = GeoCode(place.Value);
                if (coord == null)
                {
                    Debug.WriteLine(place.Key + "|FAILED");
                }
                else
                {
                    Debug.WriteLine(place.Key + "|" + coord[0] + "|" + coord[1]);
                }

                Thread.Sleep(rnd.Next(1000, 3000));
            }
        }

        private string[] GeoCode(string place)
        {
            string url =
                "http://maps.google.com/maps/geo?output=xml&key=ABQIAAAAYNbZqhmvJGanHf4fvernDRRYUZroXgecFVB5Ht2OyBQ2xZEpGRRo0PaxLSV0rPAWIaDmHcYLDQt-CQ&q="
                + HttpUtility.UrlEncode(place);
            WebRequest req = WebRequest.Create(url);
            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream());
            try
            {
                Match coord = Regex.Match(sr.ReadToEnd(), "<coordinates>.*</coordinates>");
                if (!coord.Success)
                {
                    return null;
                }

                var split = coord.Value.Split(',');
                return new[] { split[1], split[0].Replace("<coordinates>", "") };
            }
            finally
            {
                sr.Close();
            }
        }
    }
}
