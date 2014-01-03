using Viiar.AtmFinder.W8.Common;
using Windows.UI.ApplicationSettings;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;

namespace Viiar.AtmFinder.W8
{
    public class SettingsPageBase : LayoutAwarePage
    {
        protected const int ContentAnimationOffset = 100;

        protected override void GoBack(object sender, RoutedEventArgs e)
        {
            var parent = Parent as Popup;
            if (parent != null)
            {
                parent.IsOpen = false;
            }

            // If the app is not snapped, then the back button shows the Settings pane again.
            if (ApplicationView.Value != ApplicationViewState.Snapped)
            {
                SettingsPane.Show();
            }
        }
    }
}