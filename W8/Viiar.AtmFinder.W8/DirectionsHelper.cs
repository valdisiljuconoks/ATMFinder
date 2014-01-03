using System;
using Windows.Foundation;
using Windows.System;

namespace Viiar.AtmFinder.W8
{
    public static class DirectionsHelper
    {
        public static IAsyncOperation<bool> ShowDirections(double lat, double lng)
        {
            var url = string.Format(
                                    "bingmaps://launch/?rtp=pos.{0}_{1}~pos.{2}_{3}&lvl={4}&trfc=0", 
                                    ApplicationState.Latitude, 
                                    ApplicationState.Longitude, 
                                    lat, 
                                    lng, 
                                    16);

            return Launcher.LaunchUriAsync(new Uri(url));
        }
    }
}
