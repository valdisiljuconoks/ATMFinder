using System;
using System.Collections.Generic;

namespace Viiar.AtmFinder.W8.Common
{
    public static class ListOfTExtensions
    {
        public static void ForEach<T>(this List<T> collection, Action<T> action)
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
