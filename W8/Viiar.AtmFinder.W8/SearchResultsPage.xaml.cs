using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Viiar.AtmFinder.W8.Common;
using Viiar.AtmFinder.W8.DataSources;
using Viiar.AtmFinder.W8.GeoLocationServices;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Viiar.AtmFinder.W8
{
    public sealed partial class SearchResultsPage : LayoutAwarePage
    {
        private string query = string.Empty;
        private object searchResults;

        public SearchResultsPage()
        {
            InitializeComponent();
        }

        public void Search(string searchQuery)
        {
            object results;
            ObservableCollection<SearchMatch> resultsCollection;
            if (DefaultViewModel.TryGetValue("Results", out results) &&
                (resultsCollection = results as ObservableCollection<SearchMatch>) != null &&
                resultsCollection.Count != 0)
            {
                resultsCollection.Clear();
            }

            LoadState(searchQuery, null);
        }

        protected override async void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            this.query = navigationParameter as string;

            // set first title for the page while loading results
            DefaultViewModel["QueryText"] = '\u201c' + this.query + '\u201d';

            var state = pageState;
            if (pageState == null)
            {
                state = await ExecuteSearch(this.query);
            }

            BindData(state);
        }

        protected override void SaveState(Dictionary<string, object> pageState)
        {
            pageState["QueryString"] = this.query;
            pageState["Results"] = this.searchResults;
        }

        private void BindData(Dictionary<string, object> pageState)
        {
            this.searchResults = DefaultViewModel["Results"] = pageState["Results"];

            DefaultViewModel["Filters"] = new List<Filter> { new Filter("All", 10, true) };
            DefaultViewModel["ShowFilters"] = false;
        }

        private async Task<Dictionary<string, object>> ExecuteSearch(string searchTerm)
        {
            DefaultViewModel["IsDataLoading"] = true;
            var result = new Dictionary<string, object>();
            var matches = new ObservableCollection<SearchMatch>();
            try
            {
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    var searchService = GeoLocationServiceClient.CreateInstance();
                    var response = await searchService.FindCoordinatesAsync(searchTerm);

                    if (response.Locations.Any())
                    {
                        response.Locations.ForEach(
                                                   m => matches.Add(
                                                                    new SearchMatch
                                                                        {
                                                                                Title = m.Address, 
                                                                                Country = m.Country, 
                                                                                Latitude = m.Latitude, 
                                                                                Longitude = m.Longitude, 
                                                                                MapImageUri = MapHelper.CreateMiniMap(m.Latitude, m.Longitude, 15, 200, 150, 14)
                                                                        }));
                    }
                }
            }
            catch (Exception)
            {
                // TODO: do something with these exceptions
            }
            finally
            {
                DefaultViewModel["IsDataLoading"] = false;
            }

            result["QueryString"] = searchTerm;
            result["Results"] = matches;

            return result;
        }

        private void Filter_Checked(object sender, RoutedEventArgs e)
        {
            // Mirror the change into the CollectionViewSource used by the corresponding ComboBox
            // to ensure that the change is reflected when snapped
            if (this.filtersViewSource.View != null)
            {
                var filter = (sender as FrameworkElement).DataContext;
                this.filtersViewSource.View.MoveCurrentTo(filter);
            }
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedFilter = e.AddedItems.FirstOrDefault() as Filter;
            if (selectedFilter != null)
            {
                selectedFilter.Active = true;

                object results;
                ICollection resultsCollection;
                if (DefaultViewModel.TryGetValue("Results", out results) &&
                    (resultsCollection = results as ICollection) != null &&
                    resultsCollection.Count != 0)
                {
                    VisualStateManager.GoToState(this, "ResultsFound", true);
                    return;
                }
            }

            // Display informational text when there are no search results.
            VisualStateManager.GoToState(this, "NoResultsFound", true);
        }

        private async void OnResultsGridViewItemClicked(object sender, ItemClickEventArgs args)
        {
            var item = args.ClickedItem as SearchMatch;
            if (item == null)
            {
                return;
            }

            var loader = new EntityLoader();
            var response = await loader.LoadByCoordinatesAsync(item.Latitude, item.Longitude, Convert.ToInt32(ApplicationSettings.Instance.ItemsToShow) / 3);

            if (response == null)
            {
                var dialog =
                        new MessageDialog(
                                "Failed to load ATMs nearby from online services. It could be that services are down and we definitely are working on it.",
                                "Oops!");
                await dialog.ShowAsync();
                return;
            }

            var searchGroup = new EntityDataGroup("search", "Search Results", string.Empty, null, string.Empty);
            response.NearbyCashOutMachines.ForEach(e => searchGroup.Items.Add(e.ConvertToDataItem(searchGroup)));

            Frame.Navigate(
                           typeof(MapPage), 
                           new MapPageParameters
                               {
                                       ShowSearchResults = true, 
                                       SearchMatchLatitude = item.Latitude, 
                                       SearchMatchLongitude = item.Longitude,
                                       ShowGroup = searchGroup
                               });
        }
    }
}
