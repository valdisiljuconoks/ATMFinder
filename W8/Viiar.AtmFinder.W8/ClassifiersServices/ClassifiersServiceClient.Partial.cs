using System.ServiceModel;

namespace Viiar.AtmFinder.W8.ClassifiersServices
{
    public partial class ClassifiersServiceClient
    {
        public static ClassifiersServiceClient CreateInstance()
        {
            string uri = Constants.ReleaseServer;
#if DEBUG
            uri = Constants.LocalServer;
#endif
#if STAGING
            uri = Constants.StagingServer;
#endif
            return new ClassifiersServiceClient(
                    GetDefaultBinding(), 
                    new EndpointAddress(string.Format("http://{0}/classifiersservice.svc", uri)));
        }
    }
}
