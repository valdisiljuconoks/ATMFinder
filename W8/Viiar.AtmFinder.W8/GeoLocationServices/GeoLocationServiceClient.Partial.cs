using System.ServiceModel;

namespace Viiar.AtmFinder.W8.GeoLocationServices
{
    public partial class GeoLocationServiceClient
    {
        public static GeoLocationServiceClient CreateInstance()
        {
            string uri = Constants.ReleaseServer;
#if DEBUG
            uri = Constants.LocalServer;
#endif
#if STAGING
            uri = Constants.StagingServer;
#endif
            return new GeoLocationServiceClient(
                    GetDefaultBinding(), 
                    new EndpointAddress(string.Format("http://{0}/GeoLocationService.svc", uri)));
        }
    }
}
