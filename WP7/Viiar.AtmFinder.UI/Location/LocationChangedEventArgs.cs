using System;
using System.Device.Location;

namespace Viiar.AtmFinder.UI.Location
{
    public class LocationChangedEventArgs : EventArgs
    {
        public bool IsDuplicate { get; set; }
        public GeoCoordinate Coordinates { get; set; }
    }
}