namespace Viiar.AtmFinder.UI
{
    public class LocalizedStrings
    {
        private static readonly AppResources localizedResources = new AppResources();

        //public static DependencyProperty ApplicationNameProperty = DependencyProperty.Register("ApplicationName",
        //                                                                                       typeof(string),
        //                                                                                       typeof(LocalizedStrings),
        //                                                                                       new PropertyMetadata(default(string)));

        //private static string appName;

        public AppResources LocalizedResources
        {
            get { return localizedResources; }
        }

        public static string ApplicationName { get; set; }
    }

    //public event PropertyChangedEventHandler PropertyChanged;

    //private static void SetValue(DependencyProperty applicationNameProperty, string value)
    //{
    //    appName = value;
    //}

    //private static string GetValue(DependencyProperty applicationNameProperty)
    //{
    //    return string.IsNullOrEmpty(appName) ? "ATM Finder" : appName;
    //}
}
