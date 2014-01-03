using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Viiar.AtmFinder.Core.Repositories;
using Viiar.AtmFinder.UI.Extensions;

namespace Viiar.AtmFinder.UI
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        private readonly List<string> selectedBanks = new List<string>();
        private string pageName;

        public SettingsPage()
        {
            InitializeComponent();

            this.tglZoomEnabled.Content = AppResources.SwitchedOff;
            this.tglShowMapOnStartup.Content = AppResources.SwitchedOff;
            this.tglShowOnlyMyBank.Content = AppResources.SwitchedOff;
            this.tglOnlineServicesEnabled.Content = AppResources.SwitchedOff;
            this.tglLocationServicesEnabled.Content = AppResources.SwitchedOff;
            this.tglShowCoordinates.Content = AppResources.SwitchedOff;

            this.selectedBanks = AppSettings.Instance.MyChainNameSetting;
            this.lbMyBank.ItemsSource = RepositoryFactory.SupportedBanks.ToViewModel(this.selectedBanks);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            NavigationContext.QueryString.TryGetValue("page", out this.pageName);
            if (!string.IsNullOrWhiteSpace(this.pageName) && this.pageName.Equals("about", StringComparison.InvariantCultureIgnoreCase))
            {
                this.pivotMain.SelectedIndex = this.pivotMain.FindItemIndex("about");
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            AppSettings.Instance.MyChainNameSetting = this.selectedBanks;
        }

        protected void OnMyBankClicked(object sender, RoutedEventArgs e)
        {
            var checkbox = ((CheckBox)sender);
            if (checkbox.IsChecked.HasValue && checkbox.IsChecked.Value)
            {
                this.selectedBanks.Add(checkbox.Tag.ToString());
            }
            else
            {
                this.selectedBanks.Remove(checkbox.Tag.ToString());
            }
        }

        private void SwitchOn(object sender, RoutedEventArgs e)
        {
            ((ContentControl)sender).Content = AppResources.SwitchedOn;
        }

        private void SwitchOff(object sender, RoutedEventArgs e)
        {
            ((ContentControl)sender).Content = AppResources.SwitchedOff;
        }
    }
}
