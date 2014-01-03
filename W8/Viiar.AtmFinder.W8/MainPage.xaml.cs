using System.Linq;
using System.Threading.Tasks;
using Viiar.AtmFinder.W8.Common;
using Viiar.AtmFinder.W8.DataSources;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Viiar.AtmFinder.W8
{
    public sealed partial class MainPage : LayoutAwarePage
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnAboutClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private void OnGridViewItemClick(object sender, ItemClickEventArgs e)
        {
            var entity = e.ClickedItem as EntityDataItem;
            if (entity != null)
            {
                Frame.Navigate(typeof(EntityDetailsPage), entity);
            }
        }

        private void OnGroupHeaderClick(object sender, RoutedEventArgs e)
        {
            var group = ((FrameworkElement)sender).DataContext;
            Frame.Navigate(typeof(EntityGroupDetailPage), ((EntityDataGroup)group).UniqueId);
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            DefaultViewModel["Groups"] = ApplicationState.Groups;
            DefaultViewModel["Settings"] = ApplicationSettings.Instance;

            bool isLoaded = false;
            if (!ApplicationState.IsDataLoaded)
            {
                isLoaded = await RefreshData();
            }

            // TODO: move to viewmodel
            this.Map.IsEnabled = ApplicationState.Groups.Any();
            this.coordinates.Text = string.Format("({0}; {1})", ApplicationState.Latitude.ToString("F4"), ApplicationState.Longitude.ToString("F4"));

            if (ApplicationSettings.Instance.ShowMapOnStartup && isLoaded)
            {
                OnMapClick(this, null);
            }
        }

        private void OnMapClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MapPage), new MapPageParameters { ShowSingleEntity = false, ShowAllGroups = true, ShowTopItems = true });
        }

        private async void OnRefreshClick(object sender, RoutedEventArgs e)
        {
            this.GlobalAppBar.IsOpen = false;

            await RefreshData();

            // TODO: move to viewmodel
            this.Map.IsEnabled = ApplicationState.Groups.Any();
            this.coordinates.Text = string.Format("({0}; {1})", ApplicationState.Latitude.ToString("F4"), ApplicationState.Longitude.ToString("F4"));
        }

        private async Task<bool> RefreshData()
        {
            var loader = new EntityLoader();
            return await loader.LoadAsync();
        }
    }
}
