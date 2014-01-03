using System.Runtime.Serialization;

namespace Viiar.AtmFinder.Contracts
{
    [DataContract(Namespace = "http://viiar-consulting.lv/atmfinder/azureservices")]
    public enum EntityType
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        Atm = 1,

        [EnumMember]
        Branch = 2
    }
}
