using System;
using System.Collections.ObjectModel;

namespace Viiar.AtmFinder.W8.Common
{
    public static class ObservableCollectionOfTExtensions
    {
        public static void ForEach<T>(this ObservableCollection<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (var item in collection)
            {
                action(item);
            }
        }
    }
}
