using System.Collections.ObjectModel;
using Viiar.AtmFinder.W8.DataSources;

namespace Viiar.AtmFinder.W8.Common
{
    public static class ObservableCollectionOfEntityGroupExtensions
    {
        public static ObservableCollection<EntityDataGroup> ToSummary(this ObservableCollection<EntityDataGroup> collection, int count)
        {
            var result = new ObservableCollection<EntityDataGroup>();

            collection.ForEach(g => result.Add(new EntityDataGroup(g, count)));

            return result;
        }
    }
}
