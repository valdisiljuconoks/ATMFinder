using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Viiar.AtmFinder.Contracts
{
    [DataContract(Namespace = "http://viiar-consulting.lv/atmfinder/azureservices")]
    public class Entity : BaseEntity
    {
        private string address;
        private decimal distance;
        private string title;

        public Entity(string country, string chainName) : base(country, chainName)
        {
        }

        [DataMember]
        public string Address
        {
            get { return this.address; }
            set
            {
                this.address = value;
                if (!string.IsNullOrEmpty(value))
                {
                    AddressShort = value.TakeFirst(25);
                }
            }
        }

        [DataMember]
        public string AddressShort { get; set; }

        [DataMember]
        public CashDirection CashDirection { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal Distance
        {
            get { return this.distance; }
            set
            {
                this.distance = value;
                if (value > 0.02M)
                {
                    DistanceView = value > 1M
                                       ? string.Format(CultureInfo.InvariantCulture, "({0} km)", Distance)
                                       : string.Format(CultureInfo.InvariantCulture, "({0} m)", Math.Round(Distance * 1000));
                }
                else
                {
                    DistanceView = "(somewhere here)";
                }
            }
        }

        [DataMember]
        public string DistanceUnits { get; set; }

        [DataMember]
        public string DistanceView { get; set; }

        [DataMember]
        public EntityType EntityType { get; set; }

        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public double Latitude { get; set; }

        [DataMember]
        public double Longitude { get; set; }

        [DataMember]
        public string Region { get; set; }

        [DataMember]
        public bool ShowCommissionWithdrawalSign { get; set; }

        [DataMember]
        public string Title
        {
            get { return this.title; }
            set
            {
                this.title = value;
                if (!string.IsNullOrEmpty(value))
                {
                    TitleShort = value.TakeFirst(25);
                }
            }
        }

        [DataMember]
        public string TitleShort { get; set; }

        [DataMember]
        public string WorkingHours { get; set; }

        [DataMember]
        public int WorkingHoursEnd { get; set; }

        [DataMember]
        public int WorkingHoursStart { get; set; }

        public void CalculateDistance(double latitude, double longitude)
        {
            var R = 6371; // radius of the Earth

            var lon1 = Longitude * Math.PI / 180;
            var lon2 = longitude * Math.PI / 180;
            var lat1 = Latitude * Math.PI / 180;
            var lat2 = latitude * Math.PI / 180;

            var x = (lon2 - lon1) * Math.Cos((lat1 + lat2) / 2);
            var y = lat2 - lat1;
            var d = Math.Sqrt(x * x + y * y) * R;

            Distance = Convert.ToDecimal(Math.Round(d, 2));
        }
    }
}
