using System;
using System.Collections.Generic;

namespace Viiar.AtmFinder.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            foreach (var item in collection)
            {
                action(item);
            }

            return collection;
        }
    }
}
