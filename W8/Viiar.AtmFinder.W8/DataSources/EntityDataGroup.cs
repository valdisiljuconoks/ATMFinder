using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Viiar.AtmFinder.W8.DataSources
{
    public class EntityDataGroup : EntityDataCommon
    {
        private readonly ObservableCollection<EntityDataItem> items = new ObservableCollection<EntityDataItem>();
        private readonly ObservableCollection<EntityDataItem> topItem = new ObservableCollection<EntityDataItem>();

        public EntityDataGroup(EntityDataGroup group, int count)
                : this(group.UniqueId, group.Title, group.Subtitle, group.ImagePath, group.Description)
        {
            // new ObservableCollection<EntityDataItem>
            // group.Items.Take(count);
        }

        public EntityDataGroup(
                string uniqueId, 
                string title, 
                string subtitle, 
                string imagePath, 
                string description) : base(uniqueId, title, subtitle, imagePath, description)
        {
            Items.CollectionChanged += ItemsCollectionChanged;
        }

        public ObservableCollection<EntityDataItem> Items
        {
            get
            {
                return this.items;
            }
        }
        public ObservableCollection<EntityDataItem> TopItems
        {
            get
            {
                return this.topItem;
            }
        }

        public static EntityDataGroup CashIn()
        {
            return new EntityDataGroup(
                    "CashIn", 
                    "Cash in", 
                    string.Empty, 
                    string.Empty, 
                    string.Empty);
        }

        public static EntityDataGroup CashOut()
        {
            return new EntityDataGroup(
                    "CashOut", 
                    "Cash out", 
                    string.Empty, 
                    string.Empty, 
                    string.Empty);
        }

        public static EntityDataGroup Office()
        {
            return new EntityDataGroup(
                    "Offices", 
                    "Offices", 
                    string.Empty, 
                    string.Empty, 
                    string.Empty);
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Provides a subset of the full items collection to bind to from a GroupedItemsPage
            // for two reasons: GridView will not virtualize large items collections, and it
            // improves the user experience when browsing through groups with large numbers of
            // items.
            // 
            // A maximum of 12 items are displayed because it results in filled grid columns
            // whether there are 1, 2, 3, 4, or 6 rows displayed
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        if (TopItems.Count > 12)
                        {
                            TopItems.RemoveAt(12);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Move:
                    if (e.OldStartingIndex < 12 && e.NewStartingIndex < 12)
                    {
                        TopItems.Move(e.OldStartingIndex, e.NewStartingIndex);
                    }
                    else if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        TopItems.Add(Items[11]);
                    }
                    else if (e.NewStartingIndex < 12)
                    {
                        TopItems.Insert(e.NewStartingIndex, Items[e.NewStartingIndex]);
                        TopItems.RemoveAt(12);
                    }

                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems.RemoveAt(e.OldStartingIndex);
                        if (Items.Count >= 12)
                        {
                            TopItems.Add(Items[11]);
                        }
                    }

                    break;
                case NotifyCollectionChangedAction.Replace:
                    if (e.OldStartingIndex < 12)
                    {
                        TopItems[e.OldStartingIndex] = Items[e.OldStartingIndex];
                    }

                    break;
                case NotifyCollectionChangedAction.Reset:
                    TopItems.Clear();
                    while (TopItems.Count < Items.Count && TopItems.Count < 12)
                    {
                        TopItems.Add(Items[TopItems.Count]);
                    }

                    break;
            }
        }
    }
}
