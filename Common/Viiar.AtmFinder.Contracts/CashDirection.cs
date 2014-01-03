using System;
using System.Runtime.Serialization;

namespace Viiar.AtmFinder.Contracts
{
    [DataContract(Namespace = "http://viiar-consulting.lv/atmfinder/azureservices")]
    [Flags]
    public enum CashDirection
    {
        [EnumMember]
        None = 0,

        [EnumMember]
        In = 0x01,

        [EnumMember]
        Out = 0x02,
    }
}
