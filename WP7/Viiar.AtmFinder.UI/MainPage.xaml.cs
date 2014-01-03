using System;
using System.Device.Location;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Reactive;
using Microsoft.Phone.Shell;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.UI.Location;
using Viiar.AtmFinder.UI.Repositories;

namespace Viiar.AtmFinder.UI
{
    public partial class MainPage : PhoneApplicationPage
    {
        private static bool locationFailedMessgeShown;
        private bool isPhoneLocationServicesAvailbale = true;
        private int locationChangeListenCount;
        private ApplicationBarIconButton mapBarButton;

#if DEBUG
        private const int locationChangeListenCountMax = 2;
#endif
#if !DEBUG
        private const int locationChangeListenCountMax = 1;
#endif


        public MainPage()
        {
            InitializeComponent();
            InitializeControls();

            Loaded += OnLoaded;
        }

        protected void OnListItemSelected(object sender, SelectionChangedEventArgs e)
        {
            var selectdItem = ((ListBox)sender).SelectedItem as Entity;
            if (selectdItem != null)
            {
                NavigationService.Navigate(new Uri("/DetailsPage.xaml?id=" + selectdItem.Id, UriKind.Relative));
            }
        }

        protected void OnMapButtonClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Map.xaml?show=" + ((PivotItem)this.pivotMain.SelectedItem).Tag,
                                               UriKind.Relative));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            UnselectLists();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!IsLocationAvailable())
            {
                ShowLocationFailedMessage();
            }
            else
            {
                if (!DeviceLocationInfo.Current.IsLocationKnown)
                {
                    // draw progress bar only during the very 1st launch of the app and if location services are not turned off
                    this.progress.Visibility = Visibility.Visible;
                }
            }

            Task.Factory.StartNew(InitializePage);
        }

        protected void OnRefreshButtonClick(object sender, EventArgs e)
        {
            RefreshList();
        }

        protected void OnSettingsButtonClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private static void ShowLocationFailedMessage()
        {
            if (!locationFailedMessgeShown)
            {
                MessageBox.Show(AppResources.LocationServicesNotEnabledError,
                                AppResources.ErrorDialogTitle,
                                MessageBoxButton.OK);
                locationFailedMessgeShown = true;
            }
        }

        private void OnAppSettingsChanged(object sender, SettingsChangeEventArgs args)
        {
            if (args.TriggerReload)
            {
                RefreshList();
            }
        }

        private void DataBindLists()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(DataBindLists);
                return;
            }

            this.listAtms.ItemsSource = CashMachineRepository.Current.GetCurrentList();
            this.listAtmsCashOut.ItemsSource = CashMachineRepository.Current.GetCurrentCashOutList();
            this.listAtmsCashIn.ItemsSource = CashMachineRepository.Current.GetCurrentCashInList();
            this.listOffices.ItemsSource = CashMachineRepository.Current.GetCurrentOfficeList();
        }

        private void InitializeControls()
        {
            ApplicationBar = new ApplicationBar();

            this.mapBarButton = new ApplicationBarIconButton(new Uri("/icons/appbar.map.globe.rest.png", UriKind.Relative))
                {
                    Text = AppResources.MapBarButton,
                    IsEnabled = false
                };
            this.mapBarButton.Click += OnMapButtonClick;
            ApplicationBar.Buttons.Add(this.mapBarButton);

            var refreshBarButton =
                new ApplicationBarIconButton(new Uri("/icons/appbar.refresh.rest.png", UriKind.Relative))
                    { Text = AppResources.RefreshBarButton };
            refreshBarButton.Click += OnRefreshButtonClick;
            ApplicationBar.Buttons.Add(refreshBarButton);

            var menuItemSettings = new ApplicationBarMenuItem(AppResources.SettingsMenuItem);
            menuItemSettings.Click += OnSettingsButtonClick;
            ApplicationBar.MenuItems.Add(menuItemSettings);

            var menuItemAbout = new ApplicationBarMenuItem(AppResources.AboutPivotHeading);
            menuItemAbout.Click += OnAboutButtonClick;
            ApplicationBar.MenuItems.Add(menuItemAbout);

            this.piCashOut.Tag = CashDirection.Out;
            this.piCashIn.Tag = CashDirection.In;
            this.piOffices.Tag = EntityType.Branch;
        }

        private void OnAboutButtonClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml?page=about", UriKind.Relative));
        }

        private void InitializePage()
        {
            if (AppSettings.Instance.IsFirstLaunchSetting)
            {
                return;
            }

            DataBindLists();

            // subscribe to event only once
            AppSettings.SettingsChanged -= OnAppSettingsChanged;
            AppSettings.SettingsChanged += OnAppSettingsChanged;

            if (!IsLocationAvailable())
            {
                return;
            }

            // try to retrieve device location for the very first time
            if (DeviceLocationInfo.Current.IsLocationKnown)
            {
                return;
            }

            DeviceLocationInfo.Current.PositionChanged += OnPositionChanged;
            DeviceLocationInfo.Current.LocationFailed -= OnLocationFailed;
            DeviceLocationInfo.Current.LocationFailed += OnLocationFailed;
            Task.Factory.StartNew(DeviceLocationInfo.Current.StartTracking);

            // TODO: move to separate type
            if (!PhoneApplicationService.Current.State.ContainsKey("__atmfinder_firstlaunch"))
            {
                PhoneApplicationService.Current.State["__atmfinder_firstlaunch"] = false;

                if (AppSettings.Instance.ShowMapOnStartupSetting)
                {
                    Dispatcher.BeginInvoke(() => NavigationService.Navigate(new Uri("/Map.xaml?autorefresh=true", UriKind.Relative)));
                }
            }
        }

        private bool IsLocationAvailable()
        {
            return AppSettings.Instance.IsLocationServicesAvailable && this.isPhoneLocationServicesAvailbale;
        }

        protected void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (!AppSettings.Instance.IsFirstLaunchSetting)
            {
                return;
            }

            MessageBox.Show(
                "Application upgraded or new version was installed. Please configure your settings!",
                "Configure settings",
                MessageBoxButton.OK);

            AppSettings.Instance.IsFirstLaunchSetting = false;
            OnSettingsButtonClick(null, null);
        }

        protected void OnLocationFailed(object sender, GeoPositionStatusChangedEventArgs e)
        {
            ShowLocationFailedMessage();
            this.isPhoneLocationServicesAvailbale = false;
            DeviceLocationInfo.Current.LocationFailed -= OnLocationFailed;
            RefreshList();
        }

        protected void OnPivotSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationBar.IsVisible = true;
            if (this.pivotMain.SelectedItem == this.piAbout)
            {
                ApplicationBar.IsVisible = false;
            }
        }

        protected void OnPositionChanged(object sender, LocationChangedEventArgs args)
        {
            // we need to listen to location change event twice because if application is launched
            // and device location is changed since last launch, 1st event is fired with previous coordinates (is this an emulator issue?).
            Interlocked.Increment(ref this.locationChangeListenCount);

            if (this.locationChangeListenCount == locationChangeListenCountMax)
            {
                DeviceLocationInfo.Current.PositionChanged -= OnPositionChanged;
            }

            RefreshList();
        }

        private void RefreshList()
        {
            if (!IsLocationAvailable())
            {
                return;
            }

            // check if we are on the main thread
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.BeginInvoke(RefreshList);
                return;
            }

            UnselectLists();

            var loadingObservable = Observable.FromEvent<DataLoadEventArgs>(
                ev =>
                    {
                        CashMachineRepository.Current.DataLoadCompleted += ev;
                        CashMachineRepository.Current.DataLoadFailed += ev;
                    },
                ev =>
                    {
                        CashMachineRepository.Current.DataLoadCompleted -= ev;
                        CashMachineRepository.Current.DataLoadFailed -= ev;
                    });

            loadingObservable.ObserveOnDispatcher().Take(1).Subscribe(ev =>
                {
                    // TODO: maybe move this more to some viewmodel properties?
                    this.progress.Visibility = Visibility.Collapsed;
                    this.mapBarButton.IsEnabled = CashMachineRepository.Current.GetCurrentList().Any();
                    LocalizedStrings.ApplicationName = "ATM Finder (" + ev.EventArgs.Repository + ")";
                    this.pivotMain.Title = LocalizedStrings.ApplicationName;

                    if (CashMachineRepository.Current.GetCurrentList().Any())
                    {
                        var entity = CashMachineRepository.Current.GetCurrentList().First();
                        if (!ApplicationState.EntityTooFarMessageShown && entity.Distance > 100)
                        {
                            MessageBox.Show(AppResources.EntityTooFarMessage);
                            ApplicationState.EntityTooFarMessageShown = true;
                        }
                    }
                });

            CashMachineRepository.Current.SetCoordinates(DeviceLocationInfo.Current.CurrentCoordinates);

            // TODO: maybe move this more to some viewmodel properties?
            this.progress.Visibility = Visibility.Visible;
            this.mapBarButton.IsEnabled = false;
            this.coord.Text = string.Format("({0}; {1})",
                                            DeviceLocationInfo.Current.CurrentCoordinates.Longitude.ToString("F4"),
                                            DeviceLocationInfo.Current.CurrentCoordinates.Latitude.ToString("F4"));
        }

        private void UnselectLists()
        {
            this.listAtms.SelectedItem = null;
            this.listAtmsCashIn.SelectedItem = null;
            this.listAtmsCashOut.SelectedItem = null;
            this.listOffices.SelectedItem = null;
        }
    }
}
