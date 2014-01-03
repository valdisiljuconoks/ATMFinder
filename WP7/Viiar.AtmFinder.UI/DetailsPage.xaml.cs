using System;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using Viiar.AtmFinder.Contracts;
using Viiar.AtmFinder.UI.Location;
using Viiar.AtmFinder.UI.Repositories;

namespace Viiar.AtmFinder.UI
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private const double OpacityValue = 0.3;
        private const string CacheKey = "__atmfinder_current_entity";
        private Entity selectedEntity;

        // TODO: remove this field
        private string selectedEntityId;

        public DetailsPage()
        {
            InitializeComponent();
            InitializeControls();
        }

        protected void OnMapButtonClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Map.xaml?zoomId=" + this.selectedEntityId, UriKind.Relative));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            object current;

            if (!NavigationContext.QueryString.TryGetValue("id", out this.selectedEntityId))
            {
                return;
            }

            var repository = new CashMachineRepository();
            var machineId = Guid.Parse(this.selectedEntityId);

            if (PhoneApplicationService.Current.State.TryGetValue(CacheKey, out current))
            {
                if (current != null && ((Entity)current).Id != new Guid(this.selectedEntityId))
                {
                    // reload current selected entity
                    PhoneApplicationService.Current.State[CacheKey] = repository.GetById(machineId);
                }
            }
            else
            {
                PhoneApplicationService.Current.State.Add(CacheKey, repository.GetById(machineId));
            }

            this.selectedEntity = PhoneApplicationService.Current.State[CacheKey] as Entity;
            SetIconOpacity();
            DataContext = this.selectedEntity;
        }

        private void InitializeControls()
        {
            ApplicationBar = new ApplicationBar();

            var mapBarButton = new ApplicationBarIconButton(new Uri("/icons/appbar.map.globe.rest.png", UriKind.Relative))
                                   {
                                       Text = AppResources.MapBarButton
                                   };
            mapBarButton.Click += OnMapButtonClick;
            ApplicationBar.Buttons.Add(mapBarButton);

            var directionsBarButton = new ApplicationBarIconButton(new Uri("/icons/appbar.map.direction.rest.png", UriKind.Relative))
                                          {
                                              Text = AppResources.DirectionsBarButton
                                          };
            directionsBarButton.Click += OnDirectionsButtonClick;
            ApplicationBar.Buttons.Add(directionsBarButton);

            var reportBarButton = new ApplicationBarIconButton(new Uri("/icons/appbar.report.rest.png", UriKind.Relative))
                                      {
                                          Text = AppResources.ReportBarButton
                                      };
            reportBarButton.Click += OnReportButtonClick;
            ApplicationBar.Buttons.Add(reportBarButton);

            var menuItemSettings = new ApplicationBarMenuItem(AppResources.SettingsMenuItem);
            menuItemSettings.Click += (sender, args) => NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
            ApplicationBar.MenuItems.Add(menuItemSettings);

            var menuItemAbout = new ApplicationBarMenuItem(AppResources.AboutPivotHeading);
            menuItemAbout.Click += (sender, args) => NavigationService.Navigate(new Uri("/SettingsPage.xaml?page=about", UriKind.Relative));
            ApplicationBar.MenuItems.Add(menuItemAbout);
        }

        private void OnReportButtonClick(object sender, EventArgs e)
        {
            var task = new EmailComposeTask
                           {
                               Subject = "Missing ATM in 'ATM Finder' application",
                               Body =
                                   @"Hi,
Seems like ATM is missing or mislocated. Check your data please.
ATM Id: '" + this.selectedEntityId +
                                   @"'.

Thanks!",
                               To = AppResources.Email,
                           };

            task.Show();
        }

        private void OnDirectionsButtonClick(object sender, EventArgs e)
        {
            DirectionsHelper.ShowDirections(this.selectedEntity);
        }

        private void SetIconOpacity()
        {
            if ((this.selectedEntity.CashDirection & CashDirection.Out) != CashDirection.Out)
            {
                this.icoCashOut.Opacity = OpacityValue;
                this.lblCashOut.Opacity = OpacityValue;
            }

            if ((this.selectedEntity.CashDirection & CashDirection.In) != CashDirection.In)
            {
                this.icoCashIn.Opacity = OpacityValue;
                this.lblCashIn.Opacity = OpacityValue;
            }

            if (this.selectedEntity.EntityType != EntityType.Branch)
            {
                this.icoOffice.Opacity = OpacityValue;
                this.lblOffice.Opacity = OpacityValue;
            }
        }
    }
}
