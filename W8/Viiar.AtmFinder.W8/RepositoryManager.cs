using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Viiar.AtmFinder.W8.ClassifiersServices;
using Viiar.AtmFinder.W8.Common;

namespace Viiar.AtmFinder.W8
{
    public class RepositoryManager
    {
        private static ObservableCollection<CountryGroup> cachedList = new ObservableCollection<CountryGroup>();

        public async Task<ObservableCollection<CountryGroup>> SupportedBanks()
        {
            if (cachedList.Any())
            {
                return cachedList;
            }

            var service = ClassifiersServiceClient.CreateInstance();

            var result = await service.GetSupportedBanksAsync();
            result.ForEach((k, v) => cachedList.Add(new CountryGroup(k, v.ToViewModel())));

            return cachedList;
        }
    }
}
