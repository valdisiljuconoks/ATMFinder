using System.Collections.Generic;
using System.Linq;
using Viiar.AtmFinder.W8.Common;
using Viiar.AtmFinder.W8.DataSources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Viiar.AtmFinder.W8
{
    public sealed partial class EntityGroupDetailPage : LayoutAwarePage
    {
        private EntityDataGroup currentGroup;

        public EntityGroupDetailPage()
        {
            InitializeComponent();
        }

        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            var groupId = (string)navigationParameter;
            this.currentGroup = ApplicationState.Groups.First(g => g.UniqueId.Equals(groupId));

            DefaultViewModel["Group"] = this.currentGroup;
            DefaultViewModel["Items"] = this.currentGroup.Items;

            this.itemGridView.SelectedItem = null;
        }
        
        private void OnItemViewClick(object sender, ItemClickEventArgs e)
        {
            var entity = e.ClickedItem as EntityDataItem;
            if (entity != null)
            {
                Frame.Navigate(typeof(EntityDetailsPage), entity);
            }
        }

        private void OnMapClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MapPage), new MapPageParameters { ShowGroup = this.currentGroup, ShowAllGroups = false, ShowTopItems = false });
        }

        private async void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            var loader = new EntityLoader();
            this.GlobalAppBar.IsOpen = false;

            var result = await loader.LoadAsync();
            if (!result)
            {
                this.Map.IsEnabled = false;
            }

            this.itemGridView.SelectedItem = null;
        }
        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.GlobalAppBar.IsOpen = e.AddedItems.Any();
        }

        private void OnMoreClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }
    }
}
