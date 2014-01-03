using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Viiar.AtmFinder.Contracts
{
    [DataContract(Namespace = "http://viiar-consulting.lv/atmfinder/azureservices")]
    public class ServiceResponse
    {
        [DataMember]
        public List<Entity> NearbyCashInMachines { get; set; }

        [DataMember]
        public List<Entity> NearbyCashOutMachines { get; set; }

        [DataMember]
        public List<Entity> NearbyOffices { get; set; }
    }
}
