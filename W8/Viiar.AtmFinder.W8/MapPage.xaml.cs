using System;
using System.Collections.Generic;
using System.Linq;
using Bing.Maps;
using Viiar.AtmFinder.W8.Common;
using Viiar.AtmFinder.W8.DataSources;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Viiar.AtmFinder.W8
{
    public sealed partial class MapPage : LayoutAwarePage
    {
        private const double ViewPaddingCoeficient = 0.005f;
        private const double DefaultZoomLevel = 16f;
        private EntityDataItem currentEntity;
        private double currentLatitude;
        private double currentLongitude;
        private MapPageParameters parameters;

        public MapPage()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            this.map.Credentials = Constants.BingMapsKey;
        }

        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            this.parameters = navigationParameter as MapPageParameters;
        }

        private void AddEntitiesIcons()
        {
            var lat = this.currentLatitude;
            var lng = this.currentLongitude;

            if (this.parameters.ShowSingleEntity)
            {
                this.currentEntity = this.parameters.Entity;
                AddEntityIconToMap(this.currentEntity);

                lat = this.currentEntity.Latitude;
                lng = this.currentEntity.Longitude;
            }
            else
            {
                if (!this.parameters.ShowSearchResults)
                {
                    // show more than one entity
                    if (this.parameters.ShowAllGroups)
                    {
                        ApplicationState.Groups.ForEach(g => g.TopItems.ForEach(AddEntityIconToMap));
                        var closest = ApplicationState.Groups.First().TopItems.First();
                        lat = closest.Latitude;
                        lng = closest.Longitude;
                    }
                    else
                    {
                        this.parameters.ShowGroup.Items.ForEach(AddEntityIconToMap);
                        var closest = this.parameters.ShowGroup.Items.First();
                        lat = closest.Latitude;
                        lng = closest.Longitude;
                    }
                }
                else
                {
                    // show search matches on the map
                    this.parameters.ShowGroup.Items.ForEach(AddEntityIconToMap);
                    var closest = this.parameters.ShowGroup.Items.First();
                    lat = closest.Latitude;
                    lng = closest.Longitude;
                }
            }

            CenterMap(lat, lng, DefaultZoomLevel);

            // zoom to closest
            this.map.SetView(
                             new LocationRect(
                                     new Location(
                                             Math.Max(this.currentLatitude, lat) + ViewPaddingCoeficient, 
                                             Math.Min(this.currentLongitude, lng) - ViewPaddingCoeficient), 
                                     new Location(
                                             Math.Min(this.currentLatitude, lat) - ViewPaddingCoeficient, 
                                             Math.Max(this.currentLongitude, lng) + ViewPaddingCoeficient)));
        }

        private void AddEntityIconToMap(EntityDataItem entity)
        {
            var entityPushpin = new Image { Source = entity.MapImage, Stretch = Stretch.None };
            this.map.Children.Add(entityPushpin);
            entityPushpin.Tapped += (sender, args) => Frame.Navigate(typeof(EntityDetailsPage), entity);

            MapLayer.SetPosition(entityPushpin, new Location(entity.Latitude, entity.Longitude));
            MapLayer.SetPositionAnchor(entityPushpin, new Point(27, 27));
        }

        private void AddMyPositionIcon()
        {
            var myPositionImage = new Image
                                      {
                                              Source = new BitmapImage(new Uri(EntityDataCommon.ResourceBaseUri, "Assets/Icons/my-position.png")), 
                                              Stretch = Stretch.None
                                      };

            this.map.Children.Add(myPositionImage);
            MapLayer.SetPosition(myPositionImage, new Location(ApplicationState.Latitude, ApplicationState.Longitude));

            // set the anchor bottom middle point - tail of the pushpin
            MapLayer.SetPositionAnchor(myPositionImage, new Point(12, 37));

            if (this.parameters.ShowSearchResults)
            {
                var foundPositionImage = new Image
                                             {
                                                     Source = new BitmapImage(new Uri(EntityDataCommon.ResourceBaseUri, "Assets/Icons/found-position.png")), 
                                                     Stretch = Stretch.None
                                             };

                this.map.Children.Add(foundPositionImage);
                MapLayer.SetPosition(foundPositionImage, new Location(this.currentLatitude, this.currentLongitude));

                // set the anchor bottom middle point - tail of the pushpin
                MapLayer.SetPositionAnchor(foundPositionImage, new Point(12, 37));
            }
        }

        private void CenterMap(double latitude, double longitude, double zoomLevel)
        {
            this.map.SetView(new Location(latitude, longitude), zoomLevel);
        }

        private void OnDirectionsClick(object sender, RoutedEventArgs e)
        {
            DirectionsHelper.ShowDirections(this.currentEntity.Latitude, this.currentEntity.Longitude);
        }

        private void OnFoundPositionClick(object sender, RoutedEventArgs e)
        {
            CenterMap(this.parameters.SearchMatchLatitude, this.parameters.SearchMatchLongitude, DefaultZoomLevel);
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (this.parameters == null)
            {
                return;
            }

            this.currentLatitude = ApplicationState.Latitude;
            this.currentLongitude = ApplicationState.Longitude;

            if (this.parameters.ShowSearchResults)
            {
                this.currentLatitude = this.parameters.SearchMatchLatitude;
                this.currentLongitude = this.parameters.SearchMatchLongitude;
            }

            AddEntitiesIcons();
            AddMyPositionIcon();

            this.Directions.IsEnabled = this.parameters.ShowSingleEntity;
            this.FoundPosition.Visibility = this.parameters.ShowSearchResults ? Visibility.Visible : Visibility.Collapsed;
        }

        private void OnMyPositionClick(object sender, RoutedEventArgs e)
        {
            CenterMap(ApplicationState.Latitude, ApplicationState.Longitude, DefaultZoomLevel);
        }
    }
}
