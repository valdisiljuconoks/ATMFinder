using System;
using System.Collections.Generic;
using Viiar.AtmFinder.W8.Common;
using Viiar.AtmFinder.W8.DataSources;
using Viiar.AtmFinder.W8.OnlineServices;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;

namespace Viiar.AtmFinder.W8
{
    public sealed partial class EntityDetailsPage : LayoutAwarePage
    {
        private const double OpacityValue = 0.3;
        private EntityDataItem entity;

        public EntityDetailsPage()
        {
            InitializeComponent();
        }

        protected override void LoadState(object navigationParameter, Dictionary<string, object> pageState)
        {
            this.entity = navigationParameter as EntityDataItem;
            if (this.entity == null)
            {
                return;
            }

            DefaultViewModel["Entity"] = this.entity;

            var s = MapHelper.CreateMiniMap(this.entity.Latitude, this.entity.Longitude, 15, 500, 300);
            this.mapSnapshot.Source = new BitmapImage(new Uri(s));

            SetIconOpacity();
        }

        private void OnAboutClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AboutPage));
        }

        private void OnDirectionsClick(object sender, RoutedEventArgs e)
        {
            ShowDirections();
        }

        private void OnMapClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MapPage), new MapPageParameters { ShowSingleEntity = true, Entity = this.entity });
        }

        private void OnMapSnapshotTapped(object sender, TappedRoutedEventArgs e)
        {
            ShowDirections();
        }

        private void OnReportClick(object sender, RoutedEventArgs e)
        {
            var mailto =
                    new Uri(string.Format("mailto:?to={0}&subject={1}&body={2}",
                                          "valdis.iljuconoks@outlook.com",
                                          "[ATM Finder] Hey, review your data!",
                                          "Hello, You know I'm pretty sure that ATM is not here. Please check out your data! ATM Id: " + this.entity.UniqueId));
            Launcher.LaunchUriAsync(mailto);
        }

        private void SetIconOpacity()
        {
            if ((this.entity.CashDirection & CashDirection.Out) != CashDirection.Out)
            {
                this.icoCashOut.Opacity = OpacityValue;
                this.lblCashOut.Opacity = OpacityValue;
            }

            if ((this.entity.CashDirection & CashDirection.In) != CashDirection.In)
            {
                this.icoCashIn.Opacity = OpacityValue;
                this.lblCashIn.Opacity = OpacityValue;
            }

            if (this.entity.EntityType != EntityType.Branch)
            {
                this.icoOffice.Opacity = OpacityValue;
                this.lblOffice.Opacity = OpacityValue;
            }
        }

        private void ShowDirections()
        {
            DirectionsHelper.ShowDirections(this.entity.Latitude, this.entity.Longitude);
        }
    }
}
