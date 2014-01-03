using System.Collections.ObjectModel;
using Viiar.AtmFinder.W8.Common;

namespace Viiar.AtmFinder.W8
{
    public class CountryGroup : BindableBase
    {
        private readonly ObservableCollection<MyBankViewModel> banksCollection;

        public CountryGroup(string country, ObservableCollection<MyBankViewModel> banks)
        {
            this.banksCollection = banks;
            Country = country;
        }

        public string Country { get; set; }

        public ObservableCollection<MyBankViewModel> Banks
        {
            get
            {
                return this.banksCollection;
            }
        }
    }
}