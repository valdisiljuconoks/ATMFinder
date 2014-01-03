using System.ServiceModel;

namespace Viiar.AtmFinder.W8.OnlineServices
{
    public partial class AtmFinderServiceClient
    {
        public static AtmFinderServiceClient CreateInstance()
        {
            string uri = Constants.ReleaseServer;
#if DEBUG
            uri = Constants.LocalServer;
#endif
#if STAGING
            uri = Constants.StagingServer;
#endif
            return new AtmFinderServiceClient(
                    GetDefaultBinding(),
                    new EndpointAddress(string.Format("http://{0}/atmfinderservice.svc", uri)));
        }
    }
}
