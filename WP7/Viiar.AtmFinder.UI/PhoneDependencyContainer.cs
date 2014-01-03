using Viiar.AtmFinder.Core;

namespace Viiar.AtmFinder.UI
{
    public static class PhoneDependencyContainer
    {
        public static void Initialize()
        {
            DependencyContainer.Initialize(k => k.Bind<IResourceFileReader>().To<PhoneResourceFileReader>());
        }
    }
}
