using System.Globalization;
using System.Runtime.Serialization;

namespace Viiar.AtmFinder.Contracts
{
    [DataContract(Namespace = "http://viiar-consulting.lv/atmfinder/azureservices")]
    public class BaseEntity
    {
        private string chainCode;

        public BaseEntity(string country, string chainName)
        {
            Country = country;
            Chain = chainName;
        }

        [DataMember]
        public string ChainCode
        {
            get { return this.chainCode; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                this.chainCode = value;
                SmallIcon = string.Format(CultureInfo.InvariantCulture, "icons/{0}-list.png", this.chainCode);
                Icon = string.Format(CultureInfo.InvariantCulture, "icons/{0}-details.png", this.chainCode);
                IconMap = string.Format(CultureInfo.InvariantCulture, "icons/{0}-map.png", this.chainCode);
            }
        }

        [DataMember]
        public string Chain { get; set; }

        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public string IconMap { get; set; }

        [DataMember]
        public string SmallIcon { get; set; }
    }
}
