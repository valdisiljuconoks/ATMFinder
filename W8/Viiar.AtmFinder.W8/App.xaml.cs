using System;
using Viiar.AtmFinder.W8.Common;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Search;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Animation;

namespace Viiar.AtmFinder.W8
{
    public sealed partial class App : Application
    {
        private const double SettingsWidth = 346;
        private Popup settingsPopup;

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            var splashScreen = args.SplashScreen;

            splashScreen.Dismissed += OnSplashScreenDismissed;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), args.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            Window.Current.Activate();
        }

        protected override async void OnSearchActivated(SearchActivatedEventArgs args)
        {
            var previousContent = Window.Current.Content;
            var frame = previousContent as Frame;

            if (frame == null)
            {
                frame = new Frame();
                SuspensionManager.RegisterFrame(frame, "AppFrame");

                if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        // Something went wrong restoring state.
                        // Assume there is no state and continue
                    }
                }
            }

            ActiveSearchResultsPage(frame, args.QueryText);
        }

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            base.OnWindowCreated(args);

            SettingsPane.GetForCurrentView().CommandsRequested += OnCommandsRequested;
            SearchPane.GetForCurrentView().QuerySubmitted += OnSearchQuerySubmitted;
        }

        private void ActiveSearchResultsPage(Frame frame, string queryText)
        {
            if (frame.CurrentSourcePageType == typeof(SearchResultsPage))
            {
                ((SearchResultsPage)frame.Content).Search(queryText);
            }
            else
            {
                frame.Navigate(typeof(SearchResultsPage), queryText);
                Window.Current.Content = frame;
            }

            Window.Current.Activate();
        }

        private void OnCommandsRequested(SettingsPane settingsPane, SettingsPaneCommandsRequestedEventArgs args)
        {
            args.Request.ApplicationCommands.Add(new SettingsCommand("AppearanceId", "Appearance", OnSettingsCommand));
            args.Request.ApplicationCommands.Add(new SettingsCommand("MyBankId", "My Bank", OnSettingsCommand));
            args.Request.ApplicationCommands.Add(
                                                 new SettingsCommand(
                                                         "PrivacyPolicyId", 
                                                         "Privacy Policy", 
                                                         uiCommand =>
                                                         Launcher.LaunchUriAsync(
                                                                                 new Uri(
                                                                                         Constants.PrivacyPolicyUri))));
        }

        private void OnPopupClosed(object sender, object e)
        {
            Window.Current.Activated -= OnWindowActivated;
        }

        private void OnSearchQuerySubmitted(SearchPane sender, SearchPaneQuerySubmittedEventArgs args)
        {
            var previousContent = Window.Current.Content;
            var frame = previousContent as Frame;
            if (frame == null)
            {
                return;
            }

            ActiveSearchResultsPage(frame, args.QueryText);
        }

        private void OnSettingsCommand(IUICommand command)
        {
            var windowBounds = Window.Current.Bounds;

            this.settingsPopup = new Popup();
            this.settingsPopup.Closed += OnPopupClosed;
            Window.Current.Activated += OnWindowActivated;
            this.settingsPopup.IsLightDismissEnabled = true;
            this.settingsPopup.Width = SettingsWidth;
            this.settingsPopup.Height = windowBounds.Height;

            this.settingsPopup.ChildTransitions = new TransitionCollection
                                                      {
                                                              new PaneThemeTransition
                                                                  {
                                                                          Edge = (SettingsPane.Edge == SettingsEdgeLocation.Right)
                                                                                         ? EdgeTransitionLocation.Right
                                                                                         : EdgeTransitionLocation.Left
                                                                  }
                                                      };

            if (command.Id.Equals("AppearanceId"))
            {
                this.settingsPopup.Child = new SettingsPage { Width = SettingsWidth, Height = windowBounds.Height };
            }
            else if (command.Id.Equals("MyBankId"))
            {
                this.settingsPopup.Child = new MyBankSettingsPage { Width = SettingsWidth, Height = windowBounds.Height };
            }

            this.settingsPopup.SetValue(Canvas.LeftProperty, SettingsPane.Edge == SettingsEdgeLocation.Right ? (windowBounds.Width - SettingsWidth) : 0);
            this.settingsPopup.SetValue(Canvas.TopProperty, 0);
            this.settingsPopup.IsOpen = true;
        }

        private void OnSplashScreenDismissed(SplashScreen sender, object args)
        {
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        private void OnWindowActivated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == CoreWindowActivationState.Deactivated)
            {
                this.settingsPopup.IsOpen = false;
            }
        }
    }
}
