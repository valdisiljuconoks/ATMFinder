using System.Globalization;
using System.Threading;
using Microsoft.Phone.Tasks;
using Viiar.AtmFinder.Contracts;

namespace Viiar.AtmFinder.UI.Location
{
    public class DirectionsHelper
    {
        public static void ShowDirections(Entity target)
        {
            var originalCulture = Thread.CurrentThread.CurrentCulture.Name;
            try
            {
                var task = new BingMapsDirectionsTask
                               {
                                   Start = new LabeledMapLocation(AppResources.StartLocationMapDirections, DeviceLocationInfo.Current.CurrentCoordinates),
                                   End = new LabeledMapLocation(target.Title, target.GetGeoCoordinates())
                               };

                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                task.Show();
            }
            finally
            {
                // Switch back to the culture we had before
                Thread.CurrentThread.CurrentCulture = new CultureInfo(originalCulture);
            }
        }
    }
}
