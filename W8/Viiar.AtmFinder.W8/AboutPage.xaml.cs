using System;
using Viiar.AtmFinder.W8.Common;
using Windows.System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Viiar.AtmFinder.W8
{
    public sealed partial class AboutPage : LayoutAwarePage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void OnBlogUrlTextBlockTapped(object sender, TappedRoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri(((TextBlock)sender).Text));
        }

        private void OnPrivacyPolicyTapped(object sender, TappedRoutedEventArgs e)
        {
            Launcher.LaunchUriAsync(new Uri(Constants.PrivacyPolicyUri));
        }
    }
}
