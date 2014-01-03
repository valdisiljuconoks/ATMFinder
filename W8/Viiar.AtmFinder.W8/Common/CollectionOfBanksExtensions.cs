using System;
using System.Collections.ObjectModel;
using System.Linq;
using Viiar.AtmFinder.W8.ClassifiersServices;

namespace Viiar.AtmFinder.W8.Common
{
    public static class CollectionOfBanksExtensions
    {
        public static ObservableCollection<MyBankViewModel> ToViewModel(this ObservableCollection<Bank> banks)
        {
            if (banks == null)
            {
                throw new ArgumentNullException("banks");
            }

            return new ObservableCollection<MyBankViewModel>(
                    banks.Select(
                                 b => new MyBankViewModel
                                          {
                                                  Code = b.Code, 
                                                  Name = b.Name, 
                                                  IsSelected = false
                                          }));
        }
    }
}
