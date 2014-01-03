using System.Runtime.Serialization;

namespace Viiar.AtmFinder.Contracts
{
    [DataContract(Namespace = "http://viiar-consulting.lv/atmfinder/azureservices")]
    public class GeoLocationMatch
    {
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string Country { get; set; }
        [DataMember]
        public double Longitude { get; set; }
        [DataMember]
        public double Latitude { get; set; }
    }
}
