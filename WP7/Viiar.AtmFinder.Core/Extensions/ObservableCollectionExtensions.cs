using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Viiar.AtmFinder.Core.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> list)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            if (list == null)
            {
                throw new ArgumentNullException("list");
            }

            foreach (var item in list)
            {
                collection.Add(item);
            }
        }
    }
}
