using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Viiar.AtmFinder.Contracts
{
    [DataContract(Namespace = "http://viiar-consulting.lv/atmfinder/azureservices")]
    public class GeoServiceResponse
    {
        public GeoServiceResponse()
        {
            Locations = new List<GeoLocationMatch>();
        }

        [DataMember]
        public List<GeoLocationMatch> Locations { get; set; }
    }
}
