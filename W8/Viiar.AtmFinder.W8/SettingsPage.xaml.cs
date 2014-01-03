using Windows.UI.ApplicationSettings;
using Windows.UI.Xaml.Media.Animation;

namespace Viiar.AtmFinder.W8
{
    public sealed partial class SettingsPage : SettingsPageBase
    {
        public SettingsPage()
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
        }
    }
}
