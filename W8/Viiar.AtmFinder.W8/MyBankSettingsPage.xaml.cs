using System;
using System.Collections.Generic;
using Viiar.AtmFinder.W8.Common;
using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Viiar.AtmFinder.W8
{
    public sealed partial class MyBankSettingsPage : SettingsPageBase
    {
        private List<string> selectedBanks = new List<string>();

        public MyBankSettingsPage()
        {
            InitializeComponent();

            this.FlyoutContent.Transitions = new TransitionCollection
                                                 {
                                                         new EntranceThemeTransition
                                                             {
                                                                     FromHorizontalOffset =
                                                                             (SettingsPane.Edge == SettingsEdgeLocation.Right)
                                                                                     ? ContentAnimationOffset
                                                                                     : (ContentAnimationOffset * -1)
                                                             }
                                                 };

            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            try
            {
                var manager = new RepositoryManager();
                var retrievedBanks = await manager.SupportedBanks();
                this.selectedBanks = ApplicationSettings.Instance.MyBanks;

                retrievedBanks.ForEach(
                                       g => g.Banks.ForEach(
                                                            b =>
                                                                {
                                                                    if (this.selectedBanks.Contains(b.Code))
                                                                    {
                                                                        b.IsSelected = true;
                                                                    }
                                                                }));

                DefaultViewModel["Groups"] = retrievedBanks;
            }
            catch (Exception)
            {
                this.errorMessage.Visibility = Visibility.Visible;
            }
            finally
            {
                this.progressMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void OnMyBankClicked(object sender, RoutedEventArgs args)
        {
            var checkBox = sender as CheckBox;
            if (checkBox == null)
            {
                return;
            }

            var item = checkBox.Tag as MyBankViewModel;
            if (item != null)
            {
                UpdateMyBanks(item);
            }
        }

        private void OnMyBankItemClick(object sender, ItemClickEventArgs args)
        {
            var item = args.ClickedItem as MyBankViewModel;
            if (item == null)
            {
                return;
            }

            item.IsSelected = !item.IsSelected;
            UpdateMyBanks(item);
        }

        private void UpdateMyBanks(MyBankViewModel item)
        {
            if (item.IsSelected)
            {
                this.selectedBanks.Add(item.Code);
            }
            else
            {
                if (this.selectedBanks.Contains(item.Code))
                {
                    this.selectedBanks.Remove(item.Code);
                }
            }

            ApplicationSettings.Instance.MyBanks = this.selectedBanks;
        }
    }
}
