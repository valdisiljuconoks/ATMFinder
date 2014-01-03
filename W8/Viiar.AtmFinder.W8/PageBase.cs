using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Viiar.AtmFinder.W8
{
    public class PageBase : Page
    {
        protected virtual void GoBack(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to return to the previous page
            if (Frame != null && Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        protected virtual void GoForward(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to move to the next page
            if (Frame != null && Frame.CanGoForward)
            {
                Frame.GoForward();
            }
        }

        protected virtual void GoHome(object sender, RoutedEventArgs e)
        {
            // Use the navigation frame to return to the topmost page
            if (Frame != null)
            {
                while (Frame.CanGoBack)
                {
                    Frame.GoBack();
                }
            }
        }
    }
}
