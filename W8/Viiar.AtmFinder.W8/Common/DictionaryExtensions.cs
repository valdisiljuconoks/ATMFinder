using System;
using System.Collections.Generic;

namespace Viiar.AtmFinder.W8.Common
{
    public static class DictionaryExtensions
    {
        public static void ForEach<TK, TV>(this Dictionary<TK, TV> collection, Action<TK, TV> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            foreach (var item in collection)
            {
                action(item.Key, item.Value);
            }
        }
    }
}
