using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Tasks;
using System.Windows;

namespace Viiar.AtmFinder.UI
{
    public partial class AboutApp : UserControl
    {
        public AboutApp()
        {
            InitializeComponent();

            if (!CultureHelper.IsNativelySupported(AppSettings.Instance.UILanguageSetting))
            {
                this.tbStatementInSupportedLanguage.Text = AppResources.ResourceManager.GetString("NativeOSLanguageWarning", null);
                this.tbStatementInSupportedLanguage.Visibility = Visibility.Visible;
            }
        }

        private void OnContactByEmailClick(object sender, MouseButtonEventArgs e)
        {
            var task = new EmailComposeTask
                           {
                               Subject = "Feedback on 'ATM Finder' application",
                               To = AppResources.Email,
                           };

            task.Show();
        }

        private void OnRateApplicationClick(object sender, MouseButtonEventArgs e)
        {
            var marketReview = new MarketplaceReviewTask();
            marketReview.Show();
        }
    }
}
