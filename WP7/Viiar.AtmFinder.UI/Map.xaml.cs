using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Shell;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.UI.Location;
using Viiar.AtmFinder.UI.Repositories;

namespace Viiar.AtmFinder.UI
{
    public partial class Map : PhoneApplicationPage
    {
        private const string IsFirstlaunchCackeKey = "__atmfinder_mapfirstlaunch";
        private readonly CredentialsProvider credentialsProvider = new ApplicationIdCredentialsProvider(App.Id);
        private readonly ImagePushpin currentPushpin;
        private double currentZoomLevel = 15;
        private bool directionsButtonAdded;
        private MapLayer mapLayer;
        private ApplicationBarIconButton refreshBarButton;
        private string showEntityType = "";
        private bool showSingleEntity;
        private string zoomToEntityId = "";
        private IDisposable eventStream;

        public Map()
        {
            InitializeComponent();
            InitializeControls();

            this.currentPushpin = new ImagePushpin(null)
                {
                    Location = DeviceLocationInfo.Current.CurrentCoordinates,
                };
        }

        public CredentialsProvider CredentialsProvider
        {
            get { return this.credentialsProvider; }
        }

        protected bool IsFirstLaunch
        {
            get
            {
                var result = true;
                if (PhoneApplicationService.Current.State.ContainsKey(IsFirstlaunchCackeKey))
                {
                    result = (bool)PhoneApplicationService.Current.State[IsFirstlaunchCackeKey];
                }

                return result;
            }
        }

        protected void OnMyPositionClick(object sender, EventArgs e)
        {
            CenterMap();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // user is going back to previous page
            // we have to reset zoom level and viewpoint
            if (e.NavigationMode == NavigationMode.Back)
            {
                PhoneApplicationService.Current.State[IsFirstlaunchCackeKey] = true;
            }

            // we are marking that we need only single event handler invocation, after marking this as disposed - when handler will be invoked, stream will be destroyed
            if (eventStream != null)
            {
                eventStream.Dispose();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavigationContext.QueryString.TryGetValue("zoomId", out this.zoomToEntityId);
            NavigationContext.QueryString.TryGetValue("show", out this.showEntityType);

            if (!string.IsNullOrEmpty(this.zoomToEntityId))
            {
                this.showSingleEntity = true;
            }

            // should we auto-refresh?
            string queryAutoRefresh;
            NavigationContext.QueryString.TryGetValue("autorefresh", out queryAutoRefresh);
            if (!string.IsNullOrWhiteSpace(queryAutoRefresh) && IsFirstLaunch)
            {
                OnRefreshClick(null, null);
            }
            else
            {
                DrawEntitiesOnMap();
            }

            if (this.showSingleEntity)
            {
                ApplicationBar.Buttons.Remove(this.refreshBarButton);

                if (!this.directionsButtonAdded)
                {
                    var directionsBarButton =
                        new ApplicationBarIconButton(new Uri("/icons/appbar.map.direction.rest.png", UriKind.Relative))
                            {
                                Text = AppResources.DirectionsBarButton
                            };

                    ApplicationBar.Buttons.Add(directionsBarButton);
                    directionsBarButton.Click += OnDirectionsButtonClick;
                    this.directionsButtonAdded = true;
                }
            }

            eventStream = Observable.FromEvent<DataLoadEventArgs>(eh => CashMachineRepository.Current.DataLoadCompleted += eh,
                                                                      eh => CashMachineRepository.Current.DataLoadCompleted -= eh).Take(1)
                .Subscribe(result => DrawEntitiesOnMap());
        }

        private void OnDirectionsButtonClick(object sender, EventArgs e)
        {
            var selectedEntity = CashMachineRepository.Current.GetById(new Guid(this.zoomToEntityId));
            DirectionsHelper.ShowDirections(selectedEntity);
        }

        protected void OnRefreshClick(object sender, EventArgs e)
        {
            this.loadingBar.Visibility = Visibility.Visible;
            this.currentPushpin.Location = DeviceLocationInfo.Current.CurrentCoordinates;
            CashMachineRepository.Current.SetCoordinates(DeviceLocationInfo.Current.CurrentCoordinates);
        }

        private void CenterMap()
        {
            this.map.SetView(this.currentPushpin.Location, this.currentZoomLevel);
        }

        private void DrawEntitiesOnMap()
        {
            this.mapLayer.Children.Clear();
            var lat = 0.0;
            var lng = 0.0;

            if (this.showSingleEntity)
            {
                // show only selected entity
                var selectedEntity = CashMachineRepository.Current.GetById(new Guid(this.zoomToEntityId));
                var entityPushpin = new ImagePushpin(selectedEntity);
                this.mapLayer.AddChild(entityPushpin.Image, entityPushpin.Location, PositionOrigin.Center);
                this.map.SetView(entityPushpin.Location, 18);
                lat = selectedEntity.Latitude;
                lng = selectedEntity.Longitude;
            }
            else
            {
                var list = GetEntityList();
                if (list != null && list.Any())
                {
                    foreach (var entity in list)
                    {
                        var entityPushpin = new ImagePushpin(entity) { Image = { Tag = entity.Id } };
                        entityPushpin.Image.MouseLeftButtonUp += ImageOnMouseLeftButtonUp;
                        this.mapLayer.AddChild(entityPushpin.Image, entityPushpin.Location, PositionOrigin.Center);

                        if (!string.IsNullOrEmpty(this.zoomToEntityId))
                        {
                            if (entity.Id.ToString().Equals(this.zoomToEntityId))
                            {
                                this.map.SetView(entityPushpin.Location, 18);
                            }
                        }
                    }

                    var closet = list.First();
                    lat = closet.Latitude;
                    lng = closet.Longitude;
                }
            }

            if (IsFirstLaunch)
            {
                CenterMap();
                if (AppSettings.Instance.ZoomToClosestSetting)
                {
                    this.map.SetView(new LocationRect(Math.Max(this.currentPushpin.Location.Latitude, lat),
                                                      Math.Min(this.currentPushpin.Location.Longitude, lng),
                                                      Math.Min(this.currentPushpin.Location.Latitude, lat),
                                                      Math.Max(this.currentPushpin.Location.Longitude, lng)));
                }

                PhoneApplicationService.Current.State[IsFirstlaunchCackeKey] = false;
            }

            this.mapLayer.AddChild(this.currentPushpin.Pushpin, this.currentPushpin.Location, PositionOrigin.Center);
            this.loadingBar.Visibility = Visibility.Collapsed;
        }

        private IEnumerable<Entity> GetEntityList()
        {
            ObservableCollection<Entity> list = null;
            if (string.IsNullOrEmpty(this.showEntityType))
            {
                list = CashMachineRepository.Current.GetCurrentList();
            }
            else if (this.showEntityType.Equals(CashDirection.Out.ToString(),
                                                StringComparison.InvariantCultureIgnoreCase))
            {
                list = CashMachineRepository.Current.GetCurrentCashOutList();
            }
            else if (this.showEntityType.Equals(CashDirection.In.ToString(),
                                                StringComparison.InvariantCultureIgnoreCase))
            {
                list = CashMachineRepository.Current.GetCurrentCashInList();
            }
            else if (this.showEntityType.Equals(EntityType.Branch.ToString(),
                                                StringComparison.InvariantCultureIgnoreCase))
            {
                list = CashMachineRepository.Current.GetCurrentOfficeList();
            }

            return list;
        }

        private void ImageOnMouseLeftButtonUp(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var image = sender as Image;
            if (image == null)
            {
                return;
            }

            if (image.Tag != null)
            {
                NavigationService.Navigate(new Uri("/DetailsPage.xaml?id=" + (Guid)image.Tag, UriKind.Relative));
            }
        }

        private void InitializeControls()
        {
            ApplicationBar = new ApplicationBar { Mode = ApplicationBarMode.Minimized, Opacity = 0.8D };

            var centerBarButton =
                new ApplicationBarIconButton(new Uri("/icons/appbar.map.centerme.rest.png", UriKind.Relative))
                    { Text = AppResources.CenterMeBarButton };
            centerBarButton.Click += OnMyPositionClick;
            ApplicationBar.Buttons.Add(centerBarButton);

            this.refreshBarButton = new ApplicationBarIconButton(new Uri("/icons/appbar.refresh.rest.png", UriKind.Relative))
                { Text = AppResources.RefreshBarButton };
            this.refreshBarButton.Click += OnRefreshClick;
            ApplicationBar.Buttons.Add(this.refreshBarButton);

            var arealViewBarButton =
                new ApplicationBarIconButton(new Uri("/icons/appbar.photo.redeye.rest.png", UriKind.Relative)) { Text = AppResources.ArealViewBarButton };
            arealViewBarButton.Click += OnAerialViewClick;
            ApplicationBar.Buttons.Add(arealViewBarButton);

            this.mapLayer = new MapLayer();
            this.map.Children.Add(this.mapLayer);
        }

        private void OnAerialViewClick(object sender, EventArgs e)
        {
            if (this.map.Mode is AerialMode)
            {
                this.map.Mode = new RoadMode();
            }
            else
            {
                this.map.Mode = new AerialMode(true);
            }
        }

        private void OnMapViewChangeEnd(object sender, MapEventArgs e)
        {
            this.currentZoomLevel = this.map.ZoomLevel;
        }
    }
}
