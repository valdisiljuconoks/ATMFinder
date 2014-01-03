using System;
using System.Device.Location;

namespace Viiar.AtmFinder.UI.Location
{
    public class DeviceLocationInfo
    {
        private static readonly object syncRoot = new object();

        private static DeviceLocationInfo instance;
        private readonly GeoCoordinateWatcher watcher;
        public GeoCoordinate CurrentCoordinates = new GeoCoordinate(56.9449741809, 24.0930175781);

        public DeviceLocationInfo()
        {
            this.watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.High) { MovementThreshold = 10 };
            this.watcher.StatusChanged += OnGeoCoordinateWatcherStatusChanged;
            this.watcher.PositionChanged += OnGeoCoordinateWatcherPositionChanged;
        }

        public static DeviceLocationInfo Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        instance = new DeviceLocationInfo();
                    }
                }

                return instance;
            }
        }

        public bool IsLocationKnown { get; set; }
        public event EventHandler<GeoPositionStatusChangedEventArgs> LocationFailed;
        public event EventHandler<LocationChangedEventArgs> PositionChanged;

        public void OnLocationFailed(GeoPositionStatusChangedEventArgs e)
        {
            var handler = LocationFailed;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public void StartTracking()
        {
            this.watcher.Start();
        }

        protected void OnPositionChanged(LocationChangedEventArgs e)
        {
            var handler = PositionChanged;
            if (handler != null)
            {
                handler(this, e);
            }

            IsLocationKnown = true;
        }

        private void OnGeoCoordinateWatcherPositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            var args = new LocationChangedEventArgs { Coordinates = e.Position.Location };
            if (this.CurrentCoordinates == e.Position.Location)
            {
                args.IsDuplicate = true;
            }

            this.CurrentCoordinates = e.Position.Location;
            OnPositionChanged(args);
        }

        private void OnGeoCoordinateWatcherStatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case GeoPositionStatus.Disabled:
                    if (this.watcher.Permission == GeoPositionPermission.Denied)
                    {
                        OnLocationFailed(e);
                    }

                    break;
                case GeoPositionStatus.NoData:
                    // The Location Service is working, but it cannot get location data due to poor signal fidelity (most likely)
                    OnLocationFailed(e);
                    break;
            }
        }
    }
}
