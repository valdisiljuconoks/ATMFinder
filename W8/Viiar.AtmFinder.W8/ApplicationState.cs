using System.Collections.ObjectModel;
using Viiar.AtmFinder.W8.Common;
using Viiar.AtmFinder.W8.DataSources;

namespace Viiar.AtmFinder.W8
{
    public class ApplicationState : BindableBase
    {
        private static ObservableCollection<EntityDataGroup> groups;
        private static bool isLocationPermissionDenied;
        private static ApplicationState instance;
        private bool isDataLoading;
        public static double Latitude { get; set; }
        public static double Longitude { get; set; }
        public static ObservableCollection<EntityDataGroup> Groups
        {
            get
            {
                return groups ?? (groups = new ObservableCollection<EntityDataGroup>
                                               {
                                                       EntityDataGroup.CashOut(),
                                                       EntityDataGroup.CashIn(),
                                                       EntityDataGroup.Office()
                                               });
            }
        }

        public static ApplicationState Instance
        {
            get
            {
                return instance ?? (instance = new ApplicationState());
            }
        }

        public static bool IsDataLoaded { get; set; }
        public static bool IsLocationPermissionDeniedShow { get; set; }
        public static bool IsLocationPermissionDenied
        {
            get
            {
                return isLocationPermissionDenied;
            }

            set
            {
                isLocationPermissionDenied = value;
                if (!value)
                {
                    IsLocationPermissionDeniedShow = false;
                }
            }
        }

        public bool IsDataLoading
        {
            get
            {
                return this.isDataLoading;
            }

            set
            {
                SetProperty(ref this.isDataLoading, value);
            }
        }
    }
}
